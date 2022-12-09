using Commands;
using Domains.DBModels;
using MediatR;
using Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class DeletePropertyBulkCommandHandler : AsyncRequestHandler<DeletePropertyBulkCommand>
    {
        private IBaseRepository<Properties> _propertyRepository;
        private IBaseRepository<Campaign> _campaignRepository;
        public DeletePropertyBulkCommandHandler(IBaseRepository<Properties> propertyRepository, IBaseRepository<Campaign> campaignRepository)
        {
            _propertyRepository = propertyRepository;
            _campaignRepository = campaignRepository;
        }
        protected async override Task Handle(DeletePropertyBulkCommand request, CancellationToken cancellationToken)
        {
            var affectedCampaigns = await _campaignRepository.GetAllAsync(x => x.Properties.Any(y => request.PropertyIds.Contains(y.Id)));
            foreach(var campaign in affectedCampaigns)
            {
                campaign.Properties = campaign.Properties.Where(x => !request.PropertyIds.Contains(x.Id)).ToList();
                await _campaignRepository.UpdateAsync(campaign);
            }
            await _propertyRepository.DeleteAllAsync(x => request.PropertyIds.Contains(x.Id));
        }
    }
}
