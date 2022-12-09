using Domains.DBModels;
using Domains.Dtos;

using MediatR;

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Commands.Query
{
    public class GetFilesQuery : Pagination, IRequest<List<PhFile>>
    {
        [JsonIgnore]
        public string OrgId { get; set; }

        public string[] FileIds { get; set; }

    }
}
