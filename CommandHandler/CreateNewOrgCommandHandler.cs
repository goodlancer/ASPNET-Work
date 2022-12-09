
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
    public class CreateNewOrgCommandHandler : AsyncRequestHandler<CreateNewOrgCommand>
    {
        private IOrganizationManager _organizationManager;
        private IMappingService _mappingService;
        public CreateNewOrgCommandHandler(IOrganizationManager organizationManager
            , IMappingService mappingService)
        {
            _organizationManager = organizationManager;
            _mappingService = mappingService;
        }
        protected override async Task Handle(CreateNewOrgCommand request, CancellationToken cancellationToken)
        {
            var userId = request.UserId;
            var orgId = Guid.NewGuid().ToString();
            var organization = new Organization
            {
                CreatedBy = userId,
                Id = orgId,
                Title = request.OrgTitle,
                Address = request.Address,
                Image = request.Image,
                Description = request.Description,
                ImageId = request.ImageId,
                Status = request.Status,
                TimeZone = request.TimeZone,
                Email = request.Email,
                Phone = request.Phone,
                Fax = request.Fax,
            };
            await _organizationManager.CreateOrganizationAsync(organization);
            await _mappingService.MapUserOrgRole(Const.DEFAULT_ADMIN_ROLE_ID, request.UserId, organization.Id);
            var rolePermissionMappingTemplate = await _mappingService.GetRolePermissionMappingTemplateById(Const.DEFAULT_ADMIN_ROLE_ID);
            foreach (Permission permission in rolePermissionMappingTemplate.Permissions)
            {
                await _mappingService.MapRolePermissionByOrg(Const.DEFAULT_ADMIN_ROLE_ID, permission, orgId);
            }
            await _mappingService.MapOrgUser(userId, orgId);
        }
    }
}
