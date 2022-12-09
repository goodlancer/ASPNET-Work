
using AutoMapper;

using Commands;

using Domains.DBModels;

using MediatR;

using Services.IManagers;
using Services.Repository;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, bool>
    {
        private readonly IBaseUserManager _userManager;
        private readonly IMapper _mapper;
        private IBaseRepository<ForgotPasswordRequest> _baseRepositoryForgotPassword;

        public ResetPasswordCommandHandler(IBaseUserManager userManager
        , IMapper mapper
        , IBaseRepository<ForgotPasswordRequest> baseRepositoryForgotPassword)
        {
            _userManager = userManager;
            _mapper = mapper;
            _baseRepositoryForgotPassword = baseRepositoryForgotPassword;
        }

        public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var forgetRequest = await _baseRepositoryForgotPassword.GetByIdAsync(request.Slug);
            DateTime expiredDateTime = forgetRequest.RequestedDateTime.AddMinutes(30);

            if (forgetRequest.Code == request.Code && forgetRequest.Email == request.Email &&
                expiredDateTime >= DateTime.UtcNow && forgetRequest.IsUsed == false)
            {
                var dbUser = await _userManager.GetUserByEmail(request.Email);
                var user = _mapper.Map<User, ApplicationUser>(dbUser);
                var result = await _userManager.ResetUserPasswordAsync(user, request.NewPassword);

                if (result)
                {
                    forgetRequest.IsUsed = true;
                    result = await _baseRepositoryForgotPassword.UpdateAsync(forgetRequest);
                }

                return result;
            }

            return false;
        }

    }
}
