
using Commands;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    public class GetUserSpecificOrgQueryHandler : IRequestHandler<GetUserSpecificOrgQuery, List<Organization>>
    {
        private readonly IBaseRepository<UserOrganizationMapping> _userOrganizationMappingRepository;
        private readonly IBaseRepository<Organization> _organizationRepository;
        private readonly IBaseRepository<User> _userRepository;

        public GetUserSpecificOrgQueryHandler(IBaseRepository<Organization> organizationRepository
            , IBaseRepository<UserOrganizationMapping> userOrganizationMappingRepository
            , IBaseRepository<User> userRepository
           )
        {
            _organizationRepository = organizationRepository;
            _userOrganizationMappingRepository = userOrganizationMappingRepository;
            _userRepository = userRepository;
        }

        public async Task<List<Organization>> Handle(GetUserSpecificOrgQuery request, CancellationToken cancellationToken)
        {
            var userOrganizationMappingList = await _userOrganizationMappingRepository.GetAllWithPagingAsync(x => x.UserId == request.UserId, request.PageNumber, request.PageSize);
            List<Organization> orgLIst = new List<Organization>();

            var allowed = new List<bool>();
            foreach (UserOrganizationMapping userOrganization in userOrganizationMappingList)
            {
                allowed.Add(true);
            }

            if (request.SearchKey != null && request.SearchKey.Length > 0)
            {
                int j = 0;
                foreach (UserOrganizationMapping userOrganization in userOrganizationMappingList)
                {
                    var org = await _organizationRepository.GetByIdAsync(userOrganization.OrganizationId);
                    if (org.Title.Contains(request.SearchKey) == false && org.Phone.Contains(request.SearchKey) == false)
                        allowed[j] = false;
                    j++;
                }
            }

            int w = 0;
            if (request.FilterObj != null)
            {
                foreach (UserOrganizationMapping userOrganization in userOrganizationMappingList)
                {
                    var org = await _organizationRepository.GetByIdAsync(userOrganization.OrganizationId);
                    if (request.FilterObj[0] != null && request.FilterObj[0].Length > 0 && org.Status != request.FilterObj[0])
                        allowed[w] = false;
                    if (request.FilterObj[1] != null && request.FilterObj[1].Length > 0 && org.TimeZone != request.FilterObj[1])
                        allowed[w] = false;
                    w++;
                }
            }

            w = 0;
            foreach (UserOrganizationMapping userOrganization in userOrganizationMappingList)
            {
                if (allowed[w])
                {
                    var org = await _organizationRepository.GetByIdAsync(userOrganization.OrganizationId);
                    var user = await _userRepository.GetByIdAsync(org.CreatedBy);
                    if (user != null)
                    {
                        org.AdminName = user.DisplayName;
                    }
                    orgLIst.Add(org);
                }
                w++;
            }

            return orgLIst;
        }

    }
}
