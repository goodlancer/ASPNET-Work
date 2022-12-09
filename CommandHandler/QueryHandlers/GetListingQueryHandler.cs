using Commands.Query;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    public class GetListingQueryHandler : IRequestHandler<GetListingQuery, Listing>
    {
        private readonly IBaseRepository<Listing> _listingBaseRepository;
        public GetListingQueryHandler(IBaseRepository<Listing> _listingBaseRepository)
        {
            this._listingBaseRepository = _listingBaseRepository;
        }
        public async Task<Listing> Handle(GetListingQuery request, CancellationToken cancellationToken)
        {
            var listings = await _listingBaseRepository.GetSingleAsync(x => x.Id == request.ListingId);
            return listings;
        }
    }
}
