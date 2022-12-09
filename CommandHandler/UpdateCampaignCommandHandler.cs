using AutoMapper;
using Commands;
using Domains.DBModels;
using MediatR;
using Services.Repository;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Domains.Enum.Enums;

namespace CommandHandlers
{
    public class UpdateCampaignCommandHandler : AsyncRequestHandler<UpdateCampaignCommand>
    {
        private readonly IMapper _mapper;
        private IBaseRepository<Campaign> _baseRepositoryCampaign;
        private IMediator _mediator;
        public UpdateCampaignCommandHandler(IMapper mapper,
            IBaseRepository<Campaign> _baseRepositoryCampaign,
            IMediator mediator
        )
        {
            _mapper = mapper;
            this._baseRepositoryCampaign = _baseRepositoryCampaign;
            _mediator = mediator;
        }

        protected override async Task Handle(UpdateCampaignCommand request, CancellationToken cancellationToken)
        {
            var campaign = _mapper.Map<UpdateCampaignCommand, Campaign>(request);
            await this.ExecuteCreateTimelineActionCommand(request, request.Id);
            await _baseRepositoryCampaign.UpdateAsync(campaign);
        }
        private async Task ExecuteCreateTimelineActionCommand(UpdateCampaignCommand request, string campaignId)
        {
            var oldProperties = (await _baseRepositoryCampaign.GetByIdAsync(request.Id)).Properties.Select(x => x.Id);
            var addList = request.Properties.Where(x => !oldProperties.Contains(x.Id)).Select(x => x.Id).ToList();
            var removeList = oldProperties.Where(old => !request.Properties.Select(nC => nC.Id).Contains(old)).ToList();

            
            foreach (var propertyId in addList)
            {
                var cmd = new CreateTimelineActionCommand();
                cmd.NewEntry = new PropertyTimelineAction()
                {
                    UserId = request.UserId,
                    Timestamp = DateTime.UtcNow,
                    CampaignId = campaignId
                };
                cmd.Action = TimelineAction.CampaignAdd;
                cmd.NewEntry.PropertyId = propertyId;
                await _mediator.Send(cmd);
            }
            foreach (var propertyId in removeList)
            {
                var cmd = new CreateTimelineActionCommand();
                cmd.NewEntry = new PropertyTimelineAction()
                {
                    UserId = request.UserId,
                    Timestamp = DateTime.UtcNow,
                    CampaignId = campaignId
                };
                cmd.Action = TimelineAction.CampaignRemove;
                cmd.NewEntry.PropertyId = propertyId;
                await _mediator.Send(cmd);
            }
        }
    }
}
