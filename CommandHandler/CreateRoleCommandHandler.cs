using AutoMapper;

using Commands;

using Domains.DBModels;

using MediatR;

using ScolptioCRMCoreService.IManagers;

using Services.Repository;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class CreateRoleCommandHandler : AsyncRequestHandler<CreateRoleCommand>
    {
        private readonly IMapper _mapper;

        private IRoleManager _roleManager;
        private IMappingService _mappingService;
        private IBaseRepository<Permission> _baseRepositoryPermission;
        private IBaseRepository<RolePermissionMappingTemplate> _baseRepositoryRolePermissionTemplate;
        public CreateRoleCommandHandler(IRoleManager roleManager
            , IMappingService mappingService
            , IMapper mapper
            , IBaseRepository<Permission> baseRepositoryPermission,
            IBaseRepository<RolePermissionMappingTemplate> baseRepositoryRolePermissionTemplate
                                                )
        {

            _roleManager = roleManager;
            _mappingService = mappingService;
            _mapper = mapper;
            _baseRepositoryPermission = baseRepositoryPermission;
            _baseRepositoryRolePermissionTemplate = baseRepositoryRolePermissionTemplate;
        }

        protected override async Task<Task> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = _mapper.Map<CreateRoleCommand, Role>(request);
            var roleId = Guid.NewGuid().ToString();
            role.Id = roleId;
            role.OrganizationId = request.OrgId;
            role.IsActive = true;
            role.IsShownInUi = true;
            role.Title = request.RoleName;

            await _roleManager.CreateRole(role);
            var ListPermission = new List<Permission>();
            foreach (string permissionId in request.Permissions)
            {
                var permission = await _baseRepositoryPermission.GetByIdAsync(permissionId);

                if (permission != null)
                {
                    var rolePermissionMapping = new RolePermissionMapping()
                    {
                        Id = Guid.NewGuid().ToString(),
                        OrganizationId = request.OrgId,
                        PermissionId = permissionId,
                        RoleId = roleId,
                        PermissionKey = permission.Key
                    };

                    ListPermission.Add(permission);

                    await _roleManager.CreateRolePermissionMapping(rolePermissionMapping);
                }
            }

            var rolePermissionTemplate = new RolePermissionMappingTemplate();
            rolePermissionTemplate.Id = roleId;
            rolePermissionTemplate.IsActive = true;
            rolePermissionTemplate.Title = request.RoleName;
            rolePermissionTemplate.Permissions.AddRange(ListPermission);

            await _baseRepositoryRolePermissionTemplate.Create(rolePermissionTemplate);
            return Task.CompletedTask;
        }
    }
}
