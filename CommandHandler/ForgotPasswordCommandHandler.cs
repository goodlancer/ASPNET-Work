
using Commands;

using Domains.DBModels;

using Infrastructure;

using MediatR;

using Microsoft.Extensions.Configuration;

using ScolptioCRMCoreService.IManagers;

using Services.IManagers;
using Services.Repository;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, bool>
    {
        private readonly IBaseUserManager _usermanager;
        private readonly IBaseRepository<ForgotPasswordRequest> _baseRepositoryForgotPasswordRequest;
        private IBaseRepository<EmailTemplate> _baseRepositoryEmailTemplate;
        private readonly IBaseRepository<User> _baseRepositoryUser;
        private IMailManager _mailManager;
        public IConfiguration _configuration { get; set; }

        public ForgotPasswordCommandHandler(IBaseUserManager userManager,
            IBaseRepository<ForgotPasswordRequest> baseRepositoryForgotPasswordRequest,
            IMailManager mailManager,
            IConfiguration configuration,
            IBaseRepository<User> baseRepositoryUser,
            IBaseRepository<EmailTemplate> baseRepositoryEmailTemplate)
        {
            _usermanager = userManager;
            _baseRepositoryForgotPasswordRequest = baseRepositoryForgotPasswordRequest;
            _mailManager = mailManager;
            _configuration = configuration;
            _baseRepositoryUser = baseRepositoryUser;
            _baseRepositoryEmailTemplate = baseRepositoryEmailTemplate;
        }
        public async Task<bool> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {

            var user = await _baseRepositoryUser.GetSingleAsync(x => x.Email == request.Email);
            if (user != null)
            {
                Random generator = new Random();
                String random = generator.Next(0, 1000000).ToString("D6");

                var forgotPasswordRequest = new ForgotPasswordRequest()
                {
                    Id = Guid.NewGuid().ToString(),
                    Code = random,
                    Email = request.Email,
                    IsUsed = false,
                    RequestedDateTime = DateTime.UtcNow
                };

                await _baseRepositoryForgotPasswordRequest.Create(forgotPasswordRequest);

                var confirmationLink = _configuration["ForgetPasswordLink"];
                confirmationLink = confirmationLink + "?code=" + random + "&email=" + request.Email + "&slug=" + forgotPasswordRequest.Id;

                Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
                keyValuePairs.Add("{@userName}", user.DisplayName);
                keyValuePairs.Add("{@resetLink}", confirmationLink);

                var template = await _baseRepositoryEmailTemplate.GetSingleAsync(x => x.TemplateName == Const.EMAIL_TEMPLATE_PASSWORD_FORGOT);
                string emailTemplate = _mailManager.EmailTemplate(template.TemplateBody, keyValuePairs);
                await _mailManager.SendEmail(new string[] { request.Email }, null, null, template.Subject, emailTemplate);


            }

            return true;
        }
    }
}
