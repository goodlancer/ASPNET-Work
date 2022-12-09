using MediatR;

namespace Commands
{
    public class AddTagCommand : IRequest<string>
    {
        public string OrgId { get; set; }
        public string Name { get; set; }
    }
}
