using MediatR;

using System.Text.Json.Serialization;

namespace Commands
{
    public class PropertiesResourceUpdateCommand : IRequest<bool>
    {
        [JsonIgnore]
        public string OrgId { get; set; }
        public string PropertiesId { get; set; }
        public string ResourceType { get; set; }
        public string[] Keys { get; set; }
        public string UserId { get; set; }
    }
}
