using Domains.Dtos;

using MediatR;

namespace Commands.Query
{
    public class GetTeamQuery : IRequest<TeamForUi>
    {
        public string OrgId { get; set; }
        public string TeamId { get; set; }

    }
}
