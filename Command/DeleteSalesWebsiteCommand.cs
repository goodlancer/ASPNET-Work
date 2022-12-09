using MediatR;

namespace Commands
{
    public class DeleteSalesWebsiteCommand : IRequest
    {
        public string SaleswebsiteId { get; set; }
    }
}
