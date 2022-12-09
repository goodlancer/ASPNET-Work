using MediatR;

namespace Commands
{
    public class DeleteTagCommand : IRequest<bool>
    {
        public string Id { get; set; }
        public string OrgId { get; set; }
    }
}
