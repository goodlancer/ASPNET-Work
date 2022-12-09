using Domains.DBModels;

using MediatR;

using System.Text.Json.Serialization;

namespace Commands.Query
{
    public class GetPropertiesQuery : IRequest<Properties>
    {
        [JsonIgnore]
        public string OrgId { get; set; }

        public string PropertiesId { get; set; }
    }
}
