using AutoMapper;

using Commands;

using Domains.DBModels;

using Infrastructure;

using MediatR;

using Microsoft.Extensions.Configuration;

using ScolptioCRMCoreService.IManagers;

using Services.IManagers;
using Services.Repository;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler
{
    public class CreateCLientUserCommandHandler : AsyncRequestHandler<CreateClientUserCommand>
    {
        private readonly IBaseUserManager _usermanager;
        private readonly IMapper _mapper;
        private readonly IOrganizationManager _organizationManager;
        private readonly IMappingService _mappingService;
        private readonly IBaseRepository<Invitation> _baseRepositoryInvitation;
        private IBaseRepository<EmailTemplate> _baseRepositoryEmailTemplate;
        private IBaseRepository<Organization> _baseRepositoryOrganization;
        private IBaseRepository<TeamUserMapping> _baseRepositoryTeamUserMapping;
        private IMailManager _mailManager;
        public IConfiguration _configuration { get; set; }

        public CreateCLientUserCommandHandler(IBaseUserManager userManager
            , IMapper mapper
            , IOrganizationManager organizationManager
            , IMappingService mappingService
            , IBaseRepository<Invitation> _baseRepositoryInvitation
            , IBaseRepository<EmailTemplate> _baseRepositoryEmailTemplate
            , IMailManager _mailManager
            , IBaseRepository<Organization> _baseRepositoryOrganization
            , IConfiguration _configuration
            , IBaseRepository<TeamUserMapping> baseRepositoryTeamUserMapping
            )
        {
            _usermanager = userManager;
            _mapper = mapper;
            _organizationManager = organizationManager;
            _mappingService = mappingService;
            this._baseRepositoryInvitation = _baseRepositoryInvitation;
            this._baseRepositoryEmailTemplate = _baseRepositoryEmailTemplate;
            this._mailManager = _mailManager;
            this._baseRepositoryOrganization = _baseRepositoryOrganization;
            this._configuration = _configuration;
            this._baseRepositoryTeamUserMapping = baseRepositoryTeamUserMapping;
        }

        protected override async Task<bool> Handle(CreateClientUserCommand request, CancellationToken cancellationToken)
        {
            var userId = Guid.NewGuid().ToString();
            var user = _mapper.Map<CreateClientUserCommand, ApplicationUser>(request);
            var orgId = request.OrgId;
            user.Id = userId;
            user.EmailConfirmed = true;

            var result = await _usermanager.RegisterUserAsync(user, request.Password);
            if (result)
            {
                await _mappingService.MapUserOrgRole(Const.DEFAULT_CLIENT_ROLE_ID, user.Id, orgId);
                var teamUserMapping = new TeamUserMapping()
                {
                    Id = Guid.NewGuid().ToString(),
                    OrganizationId = orgId,
                    TeamId = request.TeamId,
                    UserId = userId
                };
                await _baseRepositoryTeamUserMapping.Create(teamUserMapping);

                var rolePermissionMappingTemplate = await _mappingService.GetRolePermissionMappingTemplateById(Const.DEFAULT_CLIENT_ROLE_ID);
                foreach (Permission permission in rolePermissionMappingTemplate.Permissions)
                {
                    await _mappingService.MapRolePermissionByOrg(Const.DEFAULT_CLIENT_ROLE_ID, permission, orgId);
                }
                await _mappingService.MapOrgUser(userId, orgId);
            }

            var org = await _baseRepositoryOrganization.GetSingleAsync(x => x.Id == orgId);

            var confirmationLink = _configuration["ConfirmationLink"];
            confirmationLink = confirmationLink + "?code=" + userId + "&email=" + request.Email;

            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("{@orgName}", org.Title);
            keyValuePairs.Add("{@senderName}", request.DisplayName);
            keyValuePairs.Add("{@ClientCompany}", request.CompanyName);
            keyValuePairs.Add("{@UserId}", request.Email);
            keyValuePairs.Add("{@Password}", request.Password);

            var template = await _baseRepositoryEmailTemplate.GetSingleAsync(x => x.TemplateName == Const.EMAIL_TEMPLATE_ACCOUNT_CLIENT_CONFIRMATION);
            string emailTemplate = _mailManager.EmailTemplate(template.TemplateBody, keyValuePairs);
            await _mailManager.SendEmail(new string[] { request.Email }, null, null, template.Subject, emailTemplate);

            return result;
        }
    }
}
