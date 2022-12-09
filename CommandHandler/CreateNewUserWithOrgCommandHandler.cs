using AutoMapper;

using Commands;

using Domains.DBModels;

using Infrastructure;

using MediatR;

using Services.IManagers;
using Services.Repository;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class CreateNewUserWithOrgCommandHandler : AsyncRequestHandler<CreateNewUserWithOrgCommand>
    {
        private readonly IMapper _mapper;
        private IBaseUserManager _usermanager;
        private IOrganizationManager _organizationManager;
        private IMappingService _mappingService;
        public CreateNewUserWithOrgCommandHandler(IBaseUserManager userManager,
                                                IOrganizationManager organizationManager,
                                                IMappingService mappingService,
                                                IMapper mapper)
        {
            _usermanager = userManager;
            _organizationManager = organizationManager;
            _mappingService = mappingService;
            _mapper = mapper;
        }
        protected override async Task Handle(CreateNewUserWithOrgCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<CreateNewUserWithOrgCommand, User>(request);
            var userId = Guid.NewGuid().ToString();
            var orgId = Guid.NewGuid().ToString();
            user.Id = userId;
            user.OrganizationId = orgId;
            user.Status = "Active";
            user.Signature = string.Empty;
            var organization = new Organization
            {
                CreatedBy = userId,
                Id = orgId,
                Title = request.OrgTitle,
                Address = request.Address,
                Description = request.Description,
                ImageId = request.ImageId

            };

            await _organizationManager.CreateOrganizationAsync(organization);
            await _usermanager.CreateUser(user);
            foreach (var roleId in user.Roles)
            {
                await _mappingService.MapUserOrgRole(roleId, user.Id, organization.Id);
            }
            var rolePermissionMappingTemplate = await _mappingService.GetRolePermissionMappingTemplateById(Const.DEFAULT_ADMIN_ROLE_ID);
            foreach (Permission permission in rolePermissionMappingTemplate.Permissions)
            {
                await _mappingService.MapRolePermissionByOrg(Const.DEFAULT_ADMIN_ROLE_ID, permission, orgId);
            }

            await _mappingService.MapOrgUser(userId, orgId);
        }
    }
}
