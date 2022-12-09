using Domains.DBModels;
using Domains.Dtos;
using MediatR;

using System.Collections.Generic;

namespace Commands.Query
{
    public class GetAllSalesWebsiteQuery : Pagination , IRequest<List<SalesWebsite>>
    {
        public string OrgId { get; set; }
        public string SearchKey { get; set; }
        public string[] FilterObj { get; set; }
    }
}
