using Domains.DBModels;

using MediatR;

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Commands.Query
{
    public class GetAllListingQuery : IRequest<List<Listing>>
    {
        public string OrgId { get; set; }
        public string SearchKey { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public string[] FilterObj { get; set; }
    }
}
