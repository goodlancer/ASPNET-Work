using AutoMapper;

using Commands;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class CreateTeamCommandHandler : AsyncRequestHandler<CreateTeamCommand>
    {
        private readonly IMapper _mapper;
        private IBaseRepository<Team> _baseRepositoryTeam;
        private IBaseRepository<TeamUserMapping> _baseRepositoryTeamUserMapping;

        public CreateTeamCommandHandler(IMapper mapper
            , IBaseRepository<Team> baseRepositoryTeam
            , IBaseRepository<TeamUserMapping> baseRepositoryTeamUserMapping
        )
        {
            _mapper = mapper;
            _baseRepositoryTeam = baseRepositoryTeam;
            _baseRepositoryTeamUserMapping = baseRepositoryTeamUserMapping;
        }

        protected override async Task Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            var team = _mapper.Map<CreateTeamCommand, Team>(request);
            team.Id = Guid.NewGuid().ToString();
            await _baseRepositoryTeam.Create(team);
        }
    }
}
