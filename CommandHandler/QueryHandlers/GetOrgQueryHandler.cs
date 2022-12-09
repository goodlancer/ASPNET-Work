using Commands;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    public class GetOrgQueryHandler : IRequestHandler<GetOrgQuery, Organization>
    {

        private readonly IBaseRepository<Organization> _organizationBaseRepository;

        public GetOrgQueryHandler(IBaseRepository<Organization> _organizationBaseRepository
           )
        {
            this._organizationBaseRepository = _organizationBaseRepository;
        }

        public async Task<Organization> Handle(GetOrgQuery request, CancellationToken cancellationToken)
        {
            var organization = await _organizationBaseRepository.GetByIdAsync(request.OrgId);
            return organization;
        }

    }
}
