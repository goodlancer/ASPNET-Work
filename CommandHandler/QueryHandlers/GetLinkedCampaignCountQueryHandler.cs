using Commands.Query;
using Domains.DBModels;
using Domains.Dtos;
using MediatR;
using Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    public class GetLinkedCampaignCountQueryHandler : IRequestHandler<GetLinkedCampaignCountQuery,bool>
    {
        private readonly IBaseRepository<Campaign> _campaignRepository;
        public GetLinkedCampaignCountQueryHandler(IBaseRepository<Campaign> campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }
        public async Task<bool> Handle(GetLinkedCampaignCountQuery request, CancellationToken cancellationToken)
        {
            var affectedCampaigns = await _campaignRepository.GetAllAsync(x => x.Properties.Any(y => request.PropertyIds.Contains(y.Id)));
            return affectedCampaigns.Any();
        }
    }
}
