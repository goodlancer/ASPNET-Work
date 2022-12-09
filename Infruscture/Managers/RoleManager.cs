using Domains.DBModels;

using ScolptioCRMCoreService.IManagers;

using Services.Repository;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScolptioCRMCoreService.Managers
{
    public class RoleManager : IRoleManager
    {
        private readonly IBaseRepository<Role> _roleBaseRepository;
        private readonly IBaseRepository<RolePermissionMapping> _rolePermissionMappingBaseRepository;
        private readonly IBaseRepository<UserRoleMapping> _userRoleMappingBaseRepository;

        public RoleManager(IBaseRepository<Role> roleBaseRepository
            , IBaseRepository<RolePermissionMapping> rolePermissionMappingBaseRepository
            , IBaseRepository<UserRoleMapping> userRoleMappingBaseRepository)
        {
            this._roleBaseRepository = roleBaseRepository;
            this._rolePermissionMappingBaseRepository = rolePermissionMappingBaseRepository;
            this._userRoleMappingBaseRepository = userRoleMappingBaseRepository;
        }


        public async Task CreateRole(Role role)
        {
            await _roleBaseRepository.Create(role);
        }

        public async Task CreateRolePermissionMapping(RolePermissionMapping rolePermissionMapping)
        {
            await _rolePermissionMappingBaseRepository.Create(rolePermissionMapping);
        }

        public List<Role> GetRoleByUserByOrgAsync(string userId, string orgId)
        {
            return null;
        }
    }
}
