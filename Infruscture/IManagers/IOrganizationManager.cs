using Domains.DBModels;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.IManagers
{
    public interface IOrganizationManager
    {
        Task UserRoleOrgMapsDetails(List<UserRoleMapping> userRoleMappings);
        Task CreateOrganizationAsync(Organization organization);
        Task<Organization> GetSingleOrganizationByCreatorAsync(string createdBy);
        Task<Organization> GetSingleOrganizationByIdAsync(string orgId);

    }

}
