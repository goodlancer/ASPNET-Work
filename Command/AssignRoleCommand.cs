using MediatR;

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Commands
{
    public class AssignRoleCommand : IRequest
    {
        public string UserId { get; set; }
        [JsonIgnore]
        public string OrgId { get; set; }
        public List<string> RoleIds { get; set; }
    }
}
