using MediatR;

namespace Commands
{
    public class DeleteListingCommand : IRequest
    {
        public string ListingId { get; set; }
    }
}
