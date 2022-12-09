using MediatR;

namespace Commands
{
    public class DeleteTeamCommand : IRequest<bool>
    {
        public string TeamId { get; set; }
    }
}
