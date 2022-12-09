using AutoMapper;

using Domains.DBModels;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class MappingService : IMappingService
    {
        private readonly IBaseRepository<UserRoleMapping> _userRoleMappingRepo;
        private readonly IBaseRepository<RolePermissionMapping> _rolePermissionMappingRepo;
        private readonly IBaseRepository<RolePermissionMappingTemplate> _rolePermissionMappingTemplateRepo;
        private readonly IBaseRepository<UserOrganizationMapping> _userOrganizationRepo;

        private readonly IMapper _mapper;

        public MappingService(IBaseRepository<UserRoleMapping> userRoleMappingRepo
            , IMapper mapper
            , IBaseRepository<RolePermissionMapping> rolePermissionMappingRepo
            , IBaseRepository<RolePermissionMappingTemplate> rolePermissionMappingTemplateRepo
            , IBaseRepository<UserOrganizationMapping> userOrganizationRepo
            )
        {
            _userRoleMappingRepo = userRoleMappingRepo;
            _mapper = mapper;
            _rolePermissionMappingRepo = rolePermissionMappingRepo;
            _rolePermissionMappingTemplateRepo = rolePermissionMappingTemplateRepo;
            _userOrganizationRepo = userOrganizationRepo;
        }

        public async Task MapUserOrgRole(string roleId, string userId, string organizationId)
        {

            var userRoleMap = new UserRoleMapping
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                RoleId = roleId,
                OrganizationId = organizationId
            };
            await _userRoleMappingRepo.Create(userRoleMap);
        }

        public async Task MapOrgUser(string userId, string organizationId)
        {
            var userRoleMap = new UserOrganizationMapping
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                OrganizationId = organizationId
            };
            await _userOrganizationRepo.Create(userRoleMap);
        }

        public async Task MapRolePermissionByOrg(string roleId, Permission permission, string organizationId)
        {
            var rolePermissionMap = new RolePermissionMapping
            {
                Id = Guid.NewGuid().ToString(),
                RoleId = roleId,
                OrganizationId = organizationId,
                PermissionId = permission.Id,
                PermissionKey = permission.Key
            };
            await _rolePermissionMappingRepo.Create(rolePermissionMap);
        }

        public async Task<RolePermissionMappingTemplate> GetRolePermissionMappingTemplateById(string id)
        {
            return await _rolePermissionMappingTemplateRepo.GetByIdAsync(id);
        }

        public async Task<List<RolePermissionMapping>> GetRolePermissionMappingByRoleId(string defaultRoleId)
        {
            return (List<RolePermissionMapping>)await _rolePermissionMappingRepo.GetAllAsync(x => x.RoleId == defaultRoleId);
        }
    }
}
