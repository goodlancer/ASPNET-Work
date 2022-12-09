using MediatR;

namespace Commands
{
    public class RemoveUserCommand : IRequest
    {
        public string UserId { get; set; }
        public string OrgId { get; set; }
    }
}
