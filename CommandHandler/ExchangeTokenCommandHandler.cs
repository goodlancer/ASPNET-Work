using Commands;

using MediatR;

using Services.IManagers;
using Services.IServices;

using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class ExchangeTokenCommandHandler : IRequestHandler<ExchangeTokenCommand, string>
    {
        private IBaseUserManager _userManager;
        private ITokenService _tokenService;
        public ExchangeTokenCommandHandler(IBaseUserManager userManager
            , ITokenService tokenService
            )
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }
        public async Task<string> Handle(ExchangeTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            return await _tokenService.ExchangeTokenAsync(user, request.OrgId);
        }
    }
}
