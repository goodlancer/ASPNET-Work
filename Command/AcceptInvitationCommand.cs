using MediatR;

using System.Text.Json.Serialization;

namespace Commands
{
    public class AcceptInvitationCommand : IRequest
    {
        [JsonIgnore]
        public string UserName { get; set; }
        public string OrgId { get; set; }
    }
}
