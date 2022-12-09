
using Commands;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler
{
    public class RemoveUserCommandHandler : AsyncRequestHandler<RemoveUserCommand>
    {
        private readonly IBaseRepository<UserRoleMapping> _baseRepositoryUserRoleMapping;
        private readonly IBaseRepository<UserOrganizationMapping> _baseRepositoryUserOrgMapping;

        public RemoveUserCommandHandler(IBaseRepository<UserRoleMapping> baseRepositoryUserRoleMapping
            , IBaseRepository<UserOrganizationMapping> baseRepositoryUserOrgMapping
            )
        {
            _baseRepositoryUserRoleMapping = baseRepositoryUserRoleMapping;
            _baseRepositoryUserOrgMapping = baseRepositoryUserOrgMapping;

        }

        protected override async Task<bool> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            await _baseRepositoryUserOrgMapping.DeleteAllAsync(x =>
                x.OrganizationId == request.OrgId && x.UserId == request.UserId);

            await _baseRepositoryUserRoleMapping.DeleteAllAsync(x =>
                x.OrganizationId == request.OrgId && x.UserId == request.UserId);

            return true;
        }
    }
}
