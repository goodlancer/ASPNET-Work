using Commands.Query;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    public class GetAllListingQueryHandler : IRequestHandler<GetAllListingQuery, List<Listing>>
    {
        private readonly IBaseRepository<Listing> _listingBaseRepository;
        public GetAllListingQueryHandler(IBaseRepository<Listing> _listingBaseRepository)
        {
            this._listingBaseRepository = _listingBaseRepository;
        }
        public async Task<List<Listing>> Handle(GetAllListingQuery request, CancellationToken cancellationToken)
        {
            // var listings = await _listingBaseRepository.GetAllAsync(x => x.OrganizationId == request.OrgId && x.IsMarketingSelected);
            var listings = await _listingBaseRepository.GetAllWithPagingAsync(x => x.OrganizationId == request.OrgId, request.pageNumber, request.pageSize);

            var allowed = new List<bool>();
            foreach (var income in listings.ToList())
            {
                allowed.Add(true);
            }

            if (request.SearchKey != null && request.SearchKey.Length > 0)
            {
                int j = 0;
                foreach (var income in listings.ToList())
                {
                    if (income.Address.Contains(request.SearchKey) == false)
                        allowed[j] = false;
                    j++;
                }
            }

            int w = 0;
            if (request.FilterObj != null)
            {
                foreach (var income in listings.ToList())
                {
                    if (request.FilterObj[0] != null && request.FilterObj[0].Length > 0 && income.AccessType != request.FilterObj[0])
                        allowed[w] = false;
                    w++;
                }
            }

            w = 0;
            var listListing = listings.ToList();
            foreach (var income in listings.ToList())
            {
                if (!allowed[w])
                {
                    listListing.Remove(income);
                }
                w++;
            }

            return listListing;
        }
    }
}
