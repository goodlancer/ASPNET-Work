using MediatR;

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Commands
{
    public class CreateRoleCommand : IRequest
    {
        public string RoleName { get; set; }

        [JsonIgnore]
        public string OrgId { get; set; }
        public List<string> Permissions { get; set; }

        public CreateRoleCommand()
        {
            Permissions = new List<string>();
        }
    }
}
