using Domains.DBModels;

using MediatR;

namespace Commands.Query
{
    public class GetListingQuery : IRequest<Listing>
    {
        public string ListingId { get; set; }
    }
}
