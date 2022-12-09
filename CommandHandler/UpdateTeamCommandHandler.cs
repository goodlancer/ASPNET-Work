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
    public class UpdateTeamCommandHandler : AsyncRequestHandler<UpdateTeamCommand>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Team> _baseRepositoryTeam;
        private readonly IBaseRepository<TeamUserMapping> _baseRepositoryTeamUserMapping;

        public UpdateTeamCommandHandler(IMapper mapper
            , IBaseRepository<Team> baseRepositoryTeam
            , IBaseRepository<TeamUserMapping> baseRepositoryTeamUserMapping
        )
        {
            _mapper = mapper;
            _baseRepositoryTeam = baseRepositoryTeam;
            _baseRepositoryTeamUserMapping = baseRepositoryTeamUserMapping;
        }

        protected override async Task Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
        {
            await _baseRepositoryTeamUserMapping.DeleteAllAsync(it => it.TeamId == request.Id);

            var teamDb = await _baseRepositoryTeam.GetByIdAsync(request.Id);

            if (teamDb == null)
                return;

            teamDb.TeamName = request.TeamName;
            teamDb.Role = request.Role;
            await _baseRepositoryTeam.UpdateAsync(teamDb);
        }
    }
}
