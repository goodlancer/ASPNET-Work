using Commands;

using Domains.DBModels;

using MediatR;

using ScolptioCRMCoreService.IManagers;

using Services.Repository;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class AssignRoleCommandHandler : AsyncRequestHandler<AssignRoleCommand>
    {
        private readonly IRoleManager _roleManager;
        private IBaseRepository<UserRoleMapping> _baseRepositoryUserRoleMapping;

        public AssignRoleCommandHandler(IBaseRepository<UserRoleMapping> _baseRepositoryUserRoleMapping)
        {
            this._baseRepositoryUserRoleMapping = _baseRepositoryUserRoleMapping;
        }

        protected override async Task Handle(AssignRoleCommand request, CancellationToken cancellationToken)
        {
            foreach (string roleId in request.RoleIds)
            {
                await _baseRepositoryUserRoleMapping.DeleteAllAsync(x => x.RoleId == roleId && x.OrganizationId == request.OrgId);
                var userRoleMappingObj = new UserRoleMapping
                {
                    Id = Guid.NewGuid().ToString(),
                    RoleId = roleId,
                    OrganizationId = request.OrgId,
                    UserId = request.UserId
                };
                await _baseRepositoryUserRoleMapping.Create(userRoleMappingObj);
            }
        }
    }
}
