using Domains.DBModels;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScolptioCRMCoreService.IManagers
{
    public interface IRoleManager
    {
        Task CreateRole(Role role);
        Task CreateRolePermissionMapping(RolePermissionMapping rolePermissionMapping);
        List<Role> GetRoleByUserByOrgAsync(string userId, string orgId);
    }
}
