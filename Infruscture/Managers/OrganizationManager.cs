
using Domains.DBModels;

using Services.IManagers;
using Services.Repository;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Managers
{
    public class OrganizationManager : IOrganizationManager
    {
        //private readonly IBaseRepository<> _userBaseRepository;
        private readonly IBaseRepository<Organization> _organizationBaseRepository;

        public OrganizationManager(IBaseRepository<Organization> organizationBaseRepository)
        {
            _organizationBaseRepository = organizationBaseRepository;
        }

        public async Task CreateOrganizationAsync(Organization organization)
        {
            await _organizationBaseRepository.Create(organization);
        }

        public async Task<Organization> GetSingleOrganizationByCreatorAsync(string createdBy)
        {
            return await _organizationBaseRepository.GetSingleAsync(it => it.CreatedBy == createdBy);
        }

        public async Task<Organization> GetSingleOrganizationByIdAsync(string orgId)
        {
            return await _organizationBaseRepository.GetByIdAsync(orgId);
        }

        public Task UserRoleOrgMapsDetails(List<UserRoleMapping> userRoleMappings)
        {
            throw new System.NotImplementedException();
        }
    }
}
