using AutoMapper;

using Commands;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Role> _baseRepositoryRole;
        private readonly IBaseRepository<RolePermissionMapping> _baseRepositoryRolePermissionMapping;
        private readonly IBaseRepository<UserRoleMapping> _baseRepositoryUserRoleMapping;
        private readonly IBaseRepository<Team> _baseRepositoryTeam;

        public DeleteRoleCommandHandler(IMapper mapper
            , IBaseRepository<Role> baseRepositoryRole
            , IBaseRepository<RolePermissionMapping> baseRepositoryRolePermissionMapping
            , IBaseRepository<Team> baseRepositoryTeam
            , IBaseRepository<UserRoleMapping> baseRepositoryUserRoleMapping
        )
        {
            _mapper = mapper;
            _baseRepositoryRole = baseRepositoryRole;
            _baseRepositoryRolePermissionMapping = baseRepositoryRolePermissionMapping;
            _baseRepositoryTeam = baseRepositoryTeam;
            _baseRepositoryUserRoleMapping = baseRepositoryUserRoleMapping;
        }

        public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var teamList = await _baseRepositoryTeam.GetSingleAsync(x => x.Role == request.RoleId);
            var userList = await _baseRepositoryUserRoleMapping.GetSingleAsync(x => x.RoleId == request.RoleId);

            if (teamList != null && userList != null)
            {
                return false;
            }

            await _baseRepositoryRole.Delete(request.RoleId);
            await _baseRepositoryRolePermissionMapping.DeleteAllAsync(x => x.RoleId == request.RoleId);
            return true;
        }

    }
}
