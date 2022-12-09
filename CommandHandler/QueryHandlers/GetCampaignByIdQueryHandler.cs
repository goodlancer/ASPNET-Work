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
    public class GetCampaignByIdQueryHandler : IRequestHandler<GetCampaignByIdQuery, Campaign>
    {
        private readonly IBaseRepository<Campaign> _campaignBaseRepository;
        public GetCampaignByIdQueryHandler(IBaseRepository<Campaign> _campaignBaseRepository)
        {
            this._campaignBaseRepository = _campaignBaseRepository;
        }

        public async Task<Campaign> Handle(GetCampaignByIdQuery request, CancellationToken cancellationToken)
        {
            var campaign = await _campaignBaseRepository.GetByIdAsync(request.CampaignId);
            return campaign;
        }
    }
}
