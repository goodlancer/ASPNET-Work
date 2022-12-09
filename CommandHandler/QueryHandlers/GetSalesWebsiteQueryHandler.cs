using Commands.Query;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    public class GetSalesWebsiteQueryHandler : IRequestHandler<GetSalesWebsiteQuery, SalesWebsite>
    {
        private readonly IBaseRepository<SalesWebsite> _saleswebsiteBaseRepository;
        public GetSalesWebsiteQueryHandler(IBaseRepository<SalesWebsite> _saleswebsiteBaseRepository)
        {
            this._saleswebsiteBaseRepository = _saleswebsiteBaseRepository;
        }
        public async Task<SalesWebsite> Handle(GetSalesWebsiteQuery request, CancellationToken cancellationToken)
        {
            var saleswebsites = await _saleswebsiteBaseRepository.GetSingleAsync(x => x.Id == request.SaleswebsiteId);
            return saleswebsites;
        }
    }
}
