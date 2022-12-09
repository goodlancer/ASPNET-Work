using AutoMapper;

using Commands;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Team> _baseRepositoryTeam;
        private readonly IBaseRepository<TeamUserMapping> _baseRepositoryTeamUserMapping;

        public DeleteTeamCommandHandler(IMapper mapper
            , IBaseRepository<Team> baseRepositoryTeam
            , IBaseRepository<TeamUserMapping> baseRepositoryTeamUserMapping
        )
        {
            _mapper = mapper;
            _baseRepositoryTeam = baseRepositoryTeam;
            _baseRepositoryTeamUserMapping = baseRepositoryTeamUserMapping;
        }

        public async Task<bool> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
        {
            await _baseRepositoryTeam.Delete(request.TeamId);
            await _baseRepositoryTeamUserMapping.DeleteAllAsync(x => x.TeamId == request.TeamId);
            return true;
        }

    }
}
