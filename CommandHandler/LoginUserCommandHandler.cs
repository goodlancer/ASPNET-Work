using Commands;

using MediatR;


using Services.IManagers;
using Services.IServices;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
    {
        private IBaseUserManager _userManager;
        private ITokenService _tokenService;
        public LoginUserCommandHandler(IBaseUserManager userManager
            , ITokenService tokenService
            )
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }
        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {

            var userExist = await _userManager.GetUserByEmail(request.Email);
            if (userExist != null && userExist.EmailConfirmed)
            {
                var result = await _userManager.Login(request.Email, request.Password);
                if (result)
                {
                    var user = await _userManager.FindByNameAsync(request.Email);
                    user.LastLoginUtc = DateTime.UtcNow;
                    userExist.LastLoginUtc = user.LastLoginUtc;
                    await _userManager.UpdateUser(userExist);
                    return await _tokenService.CreateTokenAsync(user);
                }
            }
            return string.Empty;
        }
    }
}
