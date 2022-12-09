using AutoMapper;

using Commands;

using Domains.DBModels;
using Domains.Dtos;

using MediatR;

using Services.Repository;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserForUi>
    {

        private readonly IBaseRepository<User> _userBaseRepository;
        private readonly IBaseRepository<UserRoleMapping> _userRoleMappingBaseRepository;
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Team> _teamBaseRepository;
        public GetUserQueryHandler(IBaseRepository<User> userBaseRepository
            , IBaseRepository<UserRoleMapping> userRoleMappingBaseRepository
            , IMapper mapper
            , IBaseRepository<Team> teamBaseRepository)
        {
            _userBaseRepository = userBaseRepository;
            _userRoleMappingBaseRepository = userRoleMappingBaseRepository;
            _mapper = mapper;
            _teamBaseRepository = teamBaseRepository;
        }

        public async Task<UserForUi> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userBaseRepository.GetByIdAsync(request.UserId);
            if (request.OrgId != null)
            {
                var rolesMapping = await _userRoleMappingBaseRepository.GetAllAsync(x =>
                    x.OrganizationId == request.OrgId && x.UserId == request.UserId);
                List<string> rolesId = new List<string>();
                foreach (UserRoleMapping rolePermissionMapping in rolesMapping)
                {
                    rolesId.Add(rolePermissionMapping.Id);
                }

                user.Roles = rolesId;
            }

            var uiUser = _mapper.Map<User, UserForUi>(user);
            var team = await _teamBaseRepository.GetSingleAsync(x => x.Id == user.TeamId);
            if (team != null)
                uiUser.TeamName = team.TeamName;
            else
                uiUser.TeamName = "Admin";
            return uiUser;
        }

    }
}
