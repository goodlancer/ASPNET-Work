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
    public class GetAllSalesWebsiteQueryHandler : IRequestHandler<GetAllSalesWebsiteQuery, List<SalesWebsite>>
    {
        private readonly IBaseRepository<SalesWebsite> _saleswebsiteBaseRepository;
        public GetAllSalesWebsiteQueryHandler(IBaseRepository<SalesWebsite> _saleswebsiteBaseRepository)
        {
            this._saleswebsiteBaseRepository = _saleswebsiteBaseRepository;
        }
        public async Task<List<SalesWebsite>> Handle(GetAllSalesWebsiteQuery request, CancellationToken cancellationToken)
        {
            var saleswebsitesForList = new List<SalesWebsite>();
            var saleswebsites = await _saleswebsiteBaseRepository.GetAllWithPagingAsync(x => x.OrganizationId == request.OrgId, request.PageNumber, request.PageSize);

            var allowed = new List<bool>();
            foreach (var saleswebsite in saleswebsites)
            {
                allowed.Add(true);
            }

            if (request.SearchKey != null && request.SearchKey.Length > 0)
            {
                int j = 0;
                foreach (var saleswebsite in saleswebsites)
                {
                    if (saleswebsite.WebAddress.Contains(request.SearchKey) == false)
                        allowed[j] = false;
                    j++;
                }
            }

            int w = 0;
            if (request.FilterObj != null)
            {
                foreach (var saleswebsite in saleswebsites)
                {
                    if (request.FilterObj[0] != null && request.FilterObj[0].Length > 0 && saleswebsite.Status.ToString() != request.FilterObj[0])
                        allowed[w] = false;
                    w++;
                }
            }

            w = 0;
            foreach (var saleswebsite in saleswebsites)
            {
                if (allowed[w])
                {
                    saleswebsitesForList.Add(saleswebsite);
                }
                w++;
            }

            return saleswebsitesForList;
        }
    }
}
