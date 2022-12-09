using Domains.DBModels;

using MediatR;

namespace Commands.Query
{
    public class GetSalesWebsiteQuery : IRequest<SalesWebsite>
    {
        public string OrgId { get; set; }
        public string SaleswebsiteId { get; set; }

    }
}
