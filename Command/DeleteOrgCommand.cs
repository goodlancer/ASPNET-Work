using MediatR;

namespace Commands
{
    public class DeleteOrgCommand : IRequest
    {
        public string OrgId { get; set; }
    }
}
