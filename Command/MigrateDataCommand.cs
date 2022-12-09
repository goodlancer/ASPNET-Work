using MediatR;

using System.Text.Json.Serialization;

namespace Commands
{
    public class MigrateDataCommand : IRequest<string>
    {
        [JsonIgnore]
        public string OrgId { get; set; }
        public string FileId { get; set; }
    }
}
