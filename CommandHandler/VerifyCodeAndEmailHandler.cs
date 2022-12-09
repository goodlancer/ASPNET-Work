using Commands;

using Domains.DBModels;

using Infrastructure;

using MediatR;

using ScolptioCRMCoreService.IManagers;

using Services.IManagers;
using Services.Repository;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class VerifyCodeAndEmailHandler : IRequestHandler<VerifyCodeAndEmail, bool>
    {
        private IBaseUserManager _userManager;
        private IMailManager _mailManager;
        private IBaseRepository<EmailTemplate> _baseRepositoryEmailTemplate;
        public VerifyCodeAndEmailHandler(IBaseUserManager _baseUserManager
            , IBaseRepository<EmailTemplate> _baseRepositoryEmailTemplate
            , IMailManager _mailManager)
        {
            this._userManager = _baseUserManager;
            this._mailManager = _mailManager;
            this._baseRepositoryEmailTemplate = _baseRepositoryEmailTemplate;
        }

        public async Task<bool> Handle(VerifyCodeAndEmail request, CancellationToken cancellationToken)
        {
            var result = await _userManager.VerifyEmail(request.Code, request.Email);

            if (result)
            {
                var user = await _userManager.GetUserByEmail(request.Email);

                Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
                keyValuePairs.Add("{@senderName}", user.DisplayName);
                keyValuePairs.Add("{@userName}", user.Email);

                var template = await _baseRepositoryEmailTemplate.GetSingleAsync(x => x.TemplateName == Const.EMAIL_TEMPLATE_ACCOUNT_CREATION);
                string emailTemplate = _mailManager.EmailTemplate(template.TemplateBody, keyValuePairs);
                await _mailManager.SendEmail(new string[] { request.Email }, null, null, template.Subject, emailTemplate);

            }

            return result;
        }
    }
}
