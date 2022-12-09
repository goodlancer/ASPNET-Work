using Commands;

using Domains.DBModels;

using Infrastructure;

using MediatR;

using ScolptioCRMCoreService.IManagers;

using Services.IManagers;
using Services.Repository;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class SendInvitationCommandHandler : AsyncRequestHandler<SendInvitationCommand>
    {
        private IBaseRepository<Invitation> _baseRepositoryInvitation;
        private IBaseRepository<TeamUserMapping> _baseRepositoryTeamUserMapping;
        private IBaseRepository<EmailTemplate> _baseRepositoryEmailTemplate;
        private IBaseRepository<Team> _baseRepositoryTeams;
        private IMailManager _mailManager;
        private IMappingService _mappingService;
        private readonly IBaseUserManager _userManager;
        public SendInvitationCommandHandler(IBaseRepository<Invitation> _baseRepositoryInvitation
            , IMailManager mailManager
            , IBaseRepository<EmailTemplate> _baseRepositoryEmailTemplate
            , IMappingService mappingService
            , IBaseUserManager userManager
            , IBaseRepository<TeamUserMapping> baseRepositoryTeamUserMapping
            , IBaseRepository<Team> baseRepositoryTeams
            )
        {
            this._baseRepositoryInvitation = _baseRepositoryInvitation;
            this._mailManager = mailManager;
            this._baseRepositoryEmailTemplate = _baseRepositoryEmailTemplate;
            this._mappingService = mappingService;
            _userManager = userManager;
            _baseRepositoryTeamUserMapping = baseRepositoryTeamUserMapping;
            _baseRepositoryTeams = baseRepositoryTeams;
        }

        protected override async Task Handle(SendInvitationCommand request, CancellationToken cancellationToken)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("{@orgName}", request.OrgName);
            keyValuePairs.Add("{@senderName}", request.Name);
            keyValuePairs.Add("{@senderEmail}", request.InvitationEmail);

            var template = await _baseRepositoryEmailTemplate.GetSingleAsync(x => x.TemplateName == Const.EMAIL_TEMPLATE_INVITATION_SEND);
            string emailTemplate = _mailManager.EmailTemplate(template.TemplateBody, keyValuePairs);

            var invitation = new Invitation
            {
                Id = Guid.NewGuid().ToString(),
                InvitedUserEmail = request.InvitationEmail,
                OrgId = request.OrgId,
                SenderId = request.UserId,
                Address = request.Address,
                Phone = request.Phone,
                TeamId = request.TeamId,
                Name = request.Name,
                Website = request.Website,
                CompanyAdress = request.CompanyAdress,
                Logo = request.Logo,
                SenderEmail = request.InvitationEmail,
                PhoneNumber = request.PhoneNumber,
                CompanyName = request.CompanyName,
                IsInvited = request.IsInvited,
                UserType = request.UserType,
                InvitedBy = request.InvitedBy
            };

            await _baseRepositoryInvitation.Create(invitation);
            if (request.IsInvited)
            {
                await _mailManager.SendEmail(new string[] { request.InvitationEmail }, null, null, template.Subject, emailTemplate);
            }

            var applicationUser = await _userManager.FindByNameAsync(request.InvitationEmail);

            if (applicationUser != null)
            {
                var dbinvitation = await _baseRepositoryInvitation.GetSingleAsync(x => x.InvitedUserEmail == request.InvitationEmail);


                if (dbinvitation != null)
                {
                    var role = await _baseRepositoryTeams.GetSingleAsync(m => m.Id == request.TeamId);

                    await _mappingService.MapUserOrgRole(role.Id, applicationUser.Id, dbinvitation.OrgId);

                    var rolePermissionMappingTemplate = await _mappingService.GetRolePermissionMappingTemplateById(role.Role);
                    foreach (Permission permission in rolePermissionMappingTemplate.Permissions)
                    {
                        await _mappingService.MapRolePermissionByOrg(role.Id, permission, dbinvitation.OrgId);
                    }
                    await _mappingService.MapOrgUser(applicationUser.Id, dbinvitation.OrgId);


                    if (dbinvitation.TeamId != null)
                    {
                        var teamUserMapping = new TeamUserMapping()
                        {
                            Id = Guid.NewGuid().ToString(),
                            TeamId = dbinvitation.TeamId,
                            OrganizationId = request.OrgId,
                            UserId = applicationUser.Id
                        };
                        await _baseRepositoryTeamUserMapping.Create(teamUserMapping);
                    }
                }
            }
        }
    }
}
