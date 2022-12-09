using Domains.DBModels;
using MediatR;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Commands.Query
{
    public class ApnLookupQuery : IRequest<List<Properties>>
    {
        [JsonIgnore]
        public string OrgId { get; set; }
        public string Apn { get; set; }
    }
}
