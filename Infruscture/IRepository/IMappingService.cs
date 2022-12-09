using Domains.DBModels;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Repository
{
    public interface IMappingService
    {
        Task MapUserOrgRole(string roleId, string userId, string organizationId);
        Task MapRolePermissionByOrg(string roleId, Permission permission, string organizationId);
        Task MapOrgUser(string userId, string organizationId);
        Task<RolePermissionMappingTemplate> GetRolePermissionMappingTemplateById(string defaultRoleId);
        Task<List<RolePermissionMapping>> GetRolePermissionMappingByRoleId(string defaultRoleId);
    }
}
