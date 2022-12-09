using MediatR;

namespace Commands
{
    public class UpdateUserRoleCommand : IRequest
    {
        public string OrgId { get; set; }
        public string UserId { get; set; }
        public string[] Roles { get; set; }
    }
}
