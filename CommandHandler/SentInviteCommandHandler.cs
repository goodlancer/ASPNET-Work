using Commands;
using Domains.DBModels;
using Infrastructure;
using MediatR;
using ScolptioCRMCoreService.IManagers;
using Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class SentInviteCommandHandler : IRequestHandler<SentInviteCommand, bool>
    {
        private IBaseRepository<EmailTemplate> _baseRepositoryEmailTemplate;
        private IMailManager _mailManager;
        private IBaseRepository<Invitation> _baseRepositoryInvitation;
        public SentInviteCommandHandler(IBaseRepository<EmailTemplate> _baseRepositoryEmailTemplate, IMailManager mailManager, IBaseRepository<Invitation> baseRepositoryInvitation)
        {
            this._baseRepositoryEmailTemplate = _baseRepositoryEmailTemplate;
            this._mailManager = mailManager;
            this._baseRepositoryInvitation = baseRepositoryInvitation;
        }

        public async Task<bool> Handle(SentInviteCommand request, CancellationToken cancellationToken)
        {
            var invitation = await _baseRepositoryInvitation.GetByIdAsync(request.Id);

            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("{@orgName}", request.OrgName);
            keyValuePairs.Add("{@senderName}", invitation.Name);
            keyValuePairs.Add("{@senderEmail}", invitation.InvitedUserEmail);

            var template = await _baseRepositoryEmailTemplate.GetSingleAsync(x => x.TemplateName == Const.EMAIL_TEMPLATE_INVITATION_SEND);
            string emailTemplate = _mailManager.EmailTemplate(template.TemplateBody, keyValuePairs);

            var isSent = await _mailManager.SendEmail(new string[] { invitation.InvitedUserEmail }, null, null, template.Subject, emailTemplate);
            invitation.IsInvited = true;
            await _baseRepositoryInvitation.UpdateAsync(invitation);
            return isSent;
        }
    }
}
