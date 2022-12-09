using AutoMapper;

using Commands;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class UpdateRoleCommandHandler : AsyncRequestHandler<UpdateRoleCommand>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Role> _baseRepositoryRole;
        private readonly IBaseRepository<RolePermissionMapping> _baseRepositoryRolePermissionMapping;

        public UpdateRoleCommandHandler(IMapper mapper
            , IBaseRepository<Role> baseRepositoryRole
            , IBaseRepository<RolePermissionMapping> baseRepositoryRolePermissionMapping
        )
        {
            _mapper = mapper;
            _baseRepositoryRole = baseRepositoryRole;
            _baseRepositoryRolePermissionMapping = baseRepositoryRolePermissionMapping;
        }

        protected override async Task Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            await _baseRepositoryRolePermissionMapping.DeleteAllAsync(it => it.RoleId == request.Id && it.OrganizationId == request.OrganizationId);

            var role = _mapper.Map<UpdateRoleCommand, Role>(request);
            await _baseRepositoryRole.UpdateAsync(role);
            foreach (var requestPermission in request.Permissions)
            {
                var teamUserMapping = new RolePermissionMapping()
                {
                    Id = Guid.NewGuid().ToString(),
                    OrganizationId = request.OrganizationId,
                    PermissionId = requestPermission,
                    RoleId = request.Id
                };
                await _baseRepositoryRolePermissionMapping.Create(teamUserMapping);
            }
        }
    }
}
