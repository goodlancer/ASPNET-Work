
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScolptioCRMCoreService.IManagers
{
    public interface IMailManager
    {
        Task<bool> SendEmail(string[] to, string[] cc, string[] bcc, string sybject, string message);
        string EmailTemplate(string template, Dictionary<string, string> keyValuePairs);
    }
}
