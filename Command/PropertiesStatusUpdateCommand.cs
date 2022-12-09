using MediatR;

using System.Text.Json.Serialization;

namespace Commands
{
    public class PropertiesStatusUpdateCommand : IRequest<bool>
    {
        [JsonIgnore]
        public string OrgId { get; set; }
        public string PropertiesId { get; set; }
        public string ResourceStatus { get; set; }
        public string UserId { get; set; }
    }
}
