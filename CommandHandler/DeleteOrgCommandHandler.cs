
using Commands;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class DeleteOrgCommandHandler : AsyncRequestHandler<DeleteOrgCommand>
    {

        private readonly IBaseRepository<Organization> _baseRepositoryOrganization;
        private readonly IBaseRepository<UserOrganizationMapping> _baseRepositoryUserOrganization;

        public DeleteOrgCommandHandler(IBaseRepository<Organization> baseRepositoryOrganization
            , IBaseRepository<UserOrganizationMapping> baseRepositoryUserOrganization
        )
        {
            _baseRepositoryOrganization = baseRepositoryOrganization;
            _baseRepositoryUserOrganization = baseRepositoryUserOrganization;
        }

        protected override async Task Handle(DeleteOrgCommand request, CancellationToken cancellationToken)
        {
            await _baseRepositoryOrganization.Delete(request.OrgId);
            await _baseRepositoryUserOrganization.DeleteAllAsync(x => x.OrganizationId == request.OrgId);
        }

    }
}
