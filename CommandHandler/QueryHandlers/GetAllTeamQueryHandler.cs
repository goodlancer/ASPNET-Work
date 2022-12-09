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
    public class GetAllTeamQueryHandler : IRequestHandler<GetAllTeamQuery, List<TeamForUi>>
    {

        private readonly IBaseRepository<Team> _baseRepositoryTeam;
        private readonly IBaseRepository<User> _baseRepositoryUser;
        private readonly IBaseRepository<Role> _baseRepositoryRole;
        private readonly IBaseRepository<TeamUserMapping> _baseRepositoryTeamUserMapping;
        private readonly IMapper _mapper;


        public GetAllTeamQueryHandler(IMapper mapper
             , IBaseRepository<Team> baseRepositoryTeam
             , IBaseRepository<TeamUserMapping> baseRepositoryTeamUserMapping
             , IBaseRepository<User> baseRepositoryUser
             , IBaseRepository<Role> baseRepositoryRole
           )
        {
            _mapper = mapper;
            _baseRepositoryTeamUserMapping = baseRepositoryTeamUserMapping;
            _baseRepositoryTeam = baseRepositoryTeam;
            _baseRepositoryUser = baseRepositoryUser;
            _baseRepositoryRole = baseRepositoryRole;
        }

        public async Task<List<TeamForUi>> Handle(GetAllTeamQuery request, CancellationToken cancellationToken)
        {
            var teamForList = new List<TeamForUi>();
            IEnumerable<Team> teamList;
            if(request.PageNumber>=0)
            teamList = await _baseRepositoryTeam.GetAllWithPagingAsync(x => x.OrganizationId == request.OrgId, request.PageNumber, request.PageSize);
            else
            teamList = await _baseRepositoryTeam.GetAllAsync(x => x.OrganizationId == request.OrgId);

            var allowed = new List<bool>();
            foreach (var team in teamList.ToList())
            {
                allowed.Add(true);
            }

            if (request.SearchKey != null && request.SearchKey.Length > 0)
            {
                int j = 0;
                foreach (var team in teamList.ToList())
                {
                    var teamForUi = _mapper.Map<Team, TeamForUi>(team);
                    var createdUser = await _baseRepositoryUser.GetByIdAsync(team.CreatedBy);
                    if (team.Role != null)
                    {
                        var role = await _baseRepositoryRole.GetByIdAsync(team.Role);
                        if (role != null)
                        {
                            teamForUi.Role = role.Title;
                        }
                        else
                        {
                            teamForUi.Role = "N/A";
                        }
                    }
                    else
                    {
                        teamForUi.Role = "N/A";
                    }
                    teamForUi.CreatedBy = createdUser?.DisplayName;
                    teamForUi.Users = new List<UserForUi>();
                    var teamUsers = await _baseRepositoryTeamUserMapping.GetAllAsync(x => x.TeamId == team.Id);
                    foreach (var teamUserMapping in teamUsers.ToList())
                    {
                        var user = await _baseRepositoryUser.GetByIdAsync(teamUserMapping.UserId);
                        var userForUi = _mapper.Map<User, UserForUi>(user);
                        teamForUi.Users.Add(userForUi);
                    }

                    if (teamForUi.TeamName.Contains(request.SearchKey) == false)
                        allowed[j] = false;
                    j++;
                }
            }

            int w = 0;
            if (request.FilterObj != null)
            {
                foreach (var team in teamList.ToList())
                {
                    var teamForUi = _mapper.Map<Team, TeamForUi>(team);
                    var createdUser = await _baseRepositoryUser.GetByIdAsync(team.CreatedBy);
                    if (team.Role != null)
                    {
                        var role = await _baseRepositoryRole.GetByIdAsync(team.Role);
                        if (role != null)
                        {
                            teamForUi.Role = role.Title;
                        }
                        else
                        {
                            teamForUi.Role = "N/A";
                        }
                    }
                    else
                    {
                        teamForUi.Role = "N/A";
                    }
                    teamForUi.CreatedBy = createdUser?.DisplayName;
                    teamForUi.Users = new List<UserForUi>();
                    var teamUsers = await _baseRepositoryTeamUserMapping.GetAllAsync(x => x.TeamId == team.Id);
                    foreach (var teamUserMapping in teamUsers.ToList())
                    {
                        var user = await _baseRepositoryUser.GetByIdAsync(teamUserMapping.UserId);
                        var userForUi = _mapper.Map<User, UserForUi>(user);
                        teamForUi.Users.Add(userForUi);
                    }

                    if (request.FilterObj[0] != null && request.FilterObj[0].Length > 0 && teamForUi.TeamName.ToLower().Contains(request.FilterObj[0].ToLower()) == false)
                        allowed[w] = false;
                    w++;
                }
            }

            w = 0;
            foreach (var team in teamList.ToList())
            {
                if (allowed[w])
                {
                    var teamForUi = _mapper.Map<Team, TeamForUi>(team);
                    var createdUser = await _baseRepositoryUser.GetByIdAsync(team.CreatedBy);
                    if (team.Role != null)
                    {
                        var role = await _baseRepositoryRole.GetByIdAsync(team.Role);
                        if (role != null)
                        {
                            teamForUi.Role = role.Title;
                        }
                        else
                        {
                            teamForUi.Role = "N/A";
                        }
                    }
                    else
                    {
                        teamForUi.Role = "N/A";
                    }
                    teamForUi.CreatedBy = createdUser?.DisplayName;
                    teamForUi.Users = new List<UserForUi>();
                    var teamUsers = await _baseRepositoryTeamUserMapping.GetAllAsync(x => x.TeamId == team.Id);
                    foreach (var teamUserMapping in teamUsers.ToList())
                    {
                        var user = await _baseRepositoryUser.GetByIdAsync(teamUserMapping.UserId);
                        var userForUi = _mapper.Map<User, UserForUi>(user);
                        teamForUi.Users.Add(userForUi);
                    }
                    teamForList.Add(teamForUi);
                }
                w++;
            }

            return teamForList;
        }

    }
}
