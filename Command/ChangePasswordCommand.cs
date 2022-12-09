using MediatR;

using System.Text.Json.Serialization;

namespace Commands
{
    public class ChangePasswordCommand : IRequest<bool>
    {
        [JsonIgnore]
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
