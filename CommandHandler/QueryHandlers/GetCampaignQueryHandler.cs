using Commands.Query;
using Domains.DBModels;
using MediatR;
using Services.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    public class GetCampaignQueryHandler : IRequestHandler<GetCampaignQuery, List<Campaign>>
    {
        private readonly IBaseRepository<Campaign> _campaignBaseRepository;
        public GetCampaignQueryHandler(IBaseRepository<Campaign> _campaignBaseRepository)
        {
            this._campaignBaseRepository = _campaignBaseRepository;
        }

        public async Task<List<Campaign>> Handle(GetCampaignQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Campaign> campaign;
            if (request.OrgId == null)
                campaign = await _campaignBaseRepository.GetAllAsync(x => true);
            else
                campaign = await _campaignBaseRepository.GetAllAsync(x => x.OrganizationId == request.OrgId);
            return campaign?.ToList();
        }
    }
}
