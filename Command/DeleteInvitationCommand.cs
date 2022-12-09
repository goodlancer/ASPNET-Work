using MediatR;

using System.Text.Json.Serialization;

namespace Commands
{
    public class DeleteInvitationCommand : IRequest<bool>
    {
        [JsonIgnore]
        public string OrganizationId { get; set; }
        public string Email { get; set; }
    }
}
