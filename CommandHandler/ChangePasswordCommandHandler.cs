

using AutoMapper;
using Commands;

using Domains.DBModels;

using MediatR;
using Services.IManagers;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, bool>
    {
        private readonly IBaseUserManager _userManager;
        private readonly IMapper _mapper;

        public ChangePasswordCommandHandler(IBaseUserManager userManager
        , IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var dbUser = await _userManager.GetUserByEmail(request.Email);
            var user = _mapper.Map<User, ApplicationUser>(dbUser);

            var result = await _userManager.ChangeUserPasswordAsync(user, request.OldPassword, request.NewPassword);
            return result;
        }


    }
}
