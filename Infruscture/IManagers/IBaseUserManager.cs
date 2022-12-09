using Domains.DBModels;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.IManagers
{
    public interface IBaseUserManager
    {
        Task CreateUser(User user);
        Task<User> GetUserByEmail(string email);
        Task<bool> VerifyEmail(string code, string email);
        void UpdateUserRoleOrgMaps(List<UserRoleMapping> userRoleMappings);
        Task<bool> Login(string email, string password = "");
        Task<bool> RegisterUserAsync(ApplicationUser user, string password = "");
        Task<bool> ChangeUserPasswordAsync(ApplicationUser user, string oldPassword = "", string newPassword = "");
        Task<ApplicationUser> FindByNameAsync(string email);
        Task<List<UserRoleMapping>> FindRolesByUserIdByOrgIdAsync(string userId, string orgId);
        Task<List<RolePermissionMapping>> FindRolesPermissionMappingByUserIdByOrgIdAsync(string roleId, string orgId);
        Task<bool> ResetUserPasswordAsync(ApplicationUser user, string newPassword = "");
        Task UpdateUser(User user);
    }
}
