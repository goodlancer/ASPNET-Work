using AutoMapper;

using Commands.Query;

using Domains.DBModels;
using Domains.Dtos;

using MediatR;

using Services.Repository;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    public class GetTeamQueryHandler : IRequestHandler<GetTeamQuery, TeamForUi>
    {

        private readonly IBaseRepository<Team> _baseRepositoryTeam;
        private readonly IBaseRepository<User> _baseRepositoryUser;
        private readonly IBaseRepository<TeamUserMapping> _baseRepositoryTeamUserMapping;
        private readonly IMapper _mapper;


        public GetTeamQueryHandler(IMapper mapper
             , IBaseRepository<Team> baseRepositoryTeam
             , IBaseRepository<TeamUserMapping> baseRepositoryTeamUserMapping
             , IBaseRepository<User> baseRepositoryUser
           )
        {
            _mapper = mapper;
            _baseRepositoryTeamUserMapping = baseRepositoryTeamUserMapping;
            _baseRepositoryTeam = baseRepositoryTeam;
            _baseRepositoryUser = baseRepositoryUser;
        }

        public async Task<TeamForUi> Handle(GetTeamQuery request, CancellationToken cancellationToken)
        {
            var teamList = await _baseRepositoryTeam.GetSingleAsync(x => x.OrganizationId == request.OrgId && x.Id == request.TeamId);
            var teamForUi = _mapper.Map<Team, TeamForUi>(teamList);
            teamForUi.Users = new List<UserForUi>();
            var teamUsers = await _baseRepositoryTeamUserMapping.GetAllAsync(x => x.TeamId == request.TeamId);
            foreach (var teamUserMapping in teamUsers.ToList())
            {
                var user = await _baseRepositoryUser.GetByIdAsync(teamUserMapping.UserId);
                var userForUi = _mapper.Map<User, UserForUi>(user);
                teamForUi.Users.Add(userForUi);
            }
            return teamForUi;
        }

    }
}
