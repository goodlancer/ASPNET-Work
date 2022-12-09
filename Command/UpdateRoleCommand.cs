using MediatR;

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Commands
{
    public class UpdateRoleCommand : IRequest
    {
        public string Id { get; set; }

        [JsonIgnore]
        public string OrganizationId { get; set; }
        public List<string> Permissions { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool IsActive { get; set; }
        public bool IsShownInUi { get; set; }

        public UpdateRoleCommand()
        {
            Permissions = new List<string>();
        }
    }
}
