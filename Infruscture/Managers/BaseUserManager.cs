
using AutoMapper;

using Domains.DBModels;

using Microsoft.AspNetCore.Identity;

using Services.IManagers;
using Services.Repository;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Managers
{
    public class BaseUserManager : IBaseUserManager
    {
        private readonly IBaseRepository<User> _userBaseRepository;
        private readonly IBaseRepository<UserRoleMapping> _userRoleMappingBaseRepository;
        private readonly IBaseRepository<RolePermissionMapping> _rolePermissionMappingBaseRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly IMapper _mapper;
        public BaseUserManager(IBaseRepository<User> userBaseRepository
            , IMapper mapper
            , UserManager<ApplicationUser> userManager
             , SignInManager<ApplicationUser> signInManager
            , IBaseRepository<UserRoleMapping> userRoleMappingBaseRepository
            , IBaseRepository<RolePermissionMapping> rolePermissionMappingBaseRepository)
        {
            _userBaseRepository = userBaseRepository;
            _mapper = mapper;
            _userRoleMappingBaseRepository = userRoleMappingBaseRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _rolePermissionMappingBaseRepository = rolePermissionMappingBaseRepository;
        }

        public async Task<bool> RegisterUserAsync(ApplicationUser user, string password = "")
        {
            var absence = await _userManager.FindByEmailAsync(user.Email);
            if (absence != null)
            {
                return false;
            }
            var result = await _userManager.CreateAsync(user, password);

            return result.Succeeded;
        }
        public async Task<bool> Login(string email, string password = "")
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);
            return result.Succeeded;
        }

        public async Task CreateUser(User user)
        {
            await _userBaseRepository.Create(user);
        }

        public async Task<bool> VerifyEmail(string code, string email)
        {
            var result = await _userBaseRepository.GetSingleAsync(x => x.Email == email);
            if (result != null && result.Id == code)
            {
                result.EmailConfirmed = true;
                await _userBaseRepository.UpdateAsync(result);
                return true;
            }
            return false;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _userBaseRepository.GetSingleAsync(x => x.Email == email);
            return user;
        }
        public void UpdateUserRoleOrgMaps(List<UserRoleMapping> userRoleMappings)
        {
            foreach (UserRoleMapping userRoleMapping in userRoleMappings)
            {
                _userRoleMappingBaseRepository.Create(userRoleMapping);
            }
        }

        public async Task<ApplicationUser> FindByNameAsync(string email)
        {
            return await _userManager.FindByNameAsync(email);
        }

        public async Task<List<UserRoleMapping>> FindRolesByUserIdByOrgIdAsync(string userId, string orgId)
        {
            var data = await _userRoleMappingBaseRepository.GetAllAsync(it => it.OrganizationId == orgId && it.UserId == userId);
            return data.ToList();
        }
        public async Task<List<RolePermissionMapping>> FindRolesPermissionMappingByUserIdByOrgIdAsync(string roleId, string orgId)
        {
            var data = await _rolePermissionMappingBaseRepository.GetAllAsync(it => (it.OrganizationId == orgId || it.OrganizationId == null) && it.RoleId == roleId);
            return data.ToList();
        }

        public async Task<bool> ChangeUserPasswordAsync(ApplicationUser user, string oldPassword = "", string newPassword = "")
        {
            var identityResult = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            return identityResult.Succeeded;
        }

        public async Task<bool> ResetUserPasswordAsync(ApplicationUser user, string newPassword = "")
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var identityResult = await _userManager.ResetPasswordAsync(user, token, newPassword);
            return identityResult.Succeeded;
        }

        public async Task UpdateUser(User user)
        {
            await _userBaseRepository.UpdateAsync(user);
        }
    }
}
