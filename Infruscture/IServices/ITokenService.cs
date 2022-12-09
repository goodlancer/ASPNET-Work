
using Domains.DBModels;

using System.Threading.Tasks;

namespace Services.IServices
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(ApplicationUser appicationUser);
        Task<string> ExchangeTokenAsync(ApplicationUser appicationUser, string org);
    }
}
