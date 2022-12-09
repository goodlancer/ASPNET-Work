using MediatR;

namespace Commands
{
    public class EditTagCommand : IRequest<bool>
    {
        public string Id { get; set; }
        public string OrgId { get; set; }
        public string Name { get; set; }
    }
}
