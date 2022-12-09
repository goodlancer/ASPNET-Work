using MediatR;

using System.Text.Json.Serialization;

namespace Commands
{
    public class InitiateFileImportCommand : IRequest<string>
    {
        public string FileId { get; set; }
        [JsonIgnore]
        public string OrgId { get; set; }
    }
}
