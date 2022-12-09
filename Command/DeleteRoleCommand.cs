using MediatR;

namespace Commands
{
    public class DeleteRoleCommand : IRequest<bool>
    {
        public string RoleId { get; set; }
    }
}
