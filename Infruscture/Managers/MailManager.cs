
using Microsoft.Extensions.Configuration;

using ScolptioCRMCoreService.IManagers;

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
namespace ScolptioCRMCoreService.Managers
{
    public class MailManager : IMailManager
    {

        public IConfiguration _configuration { get; set; }
        public MailManager(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public Task<bool> SendEmail(string[] to, string[] cc, string[] bcc, string subject, string message)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                mailMessage.From = new MailAddress(_configuration["EmailConfig:SenderEmail"]);

                SetEmail(mailMessage.To, to);

                if (cc != null)
                {
                    SetEmail(mailMessage.CC, cc);
                }

                if (bcc != null)
                {
                    SetEmail(mailMessage.Bcc, bcc);
                }
                else
                {
                    SetEmail(mailMessage.Bcc, new string[] { _configuration["EmailConfig:SenderEmail"] });
                }

                mailMessage.Subject = subject;
                mailMessage.IsBodyHtml = true; //to make message body as html  
                mailMessage.Body = message;
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                smtp.Port = Convert.ToInt32(_configuration["EmailConfig:Port"]);
                smtp.Host = _configuration["EmailConfig:Host"];
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(_configuration["EmailConfig:SenderEmail"], _configuration["EmailConfig:SenderCredential"]);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(mailMessage);
            }
            catch (Exception ex)
            {

            }
            return Task.FromResult(true);
        }

        private void SetEmail(MailAddressCollection mailAddresses, string[] mails)
        {
            foreach (string mail in mails)
            {
                mailAddresses.Add(new MailAddress(mail));
            }
        }

        public string EmailTemplate(string template, Dictionary<string, string> payloads)
        {
            foreach (KeyValuePair<string, string> kvp in payloads)
            {
                template = template.Replace(kvp.Key, kvp.Value);
            }
            return template;
        }
    }

}
