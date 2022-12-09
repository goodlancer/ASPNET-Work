using Domains.DBModels;
using Domains.Dtos;

using MediatR;

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Commands
{
    public class GetUserSpecificOrgQuery : Pagination, IRequest<List<Organization>>
    {
        [JsonIgnore]
        public string UserId { get; set; }
        public string SearchKey { get; set; }
        public string[] FilterObj { get; set; }

    }
}
