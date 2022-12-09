using AutoMapper;
using Commands;
using Domains.DBModels;
using Domains.Dtos;
using MediatR;
using Services.Repository;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static Domains.Enum.Enums;

namespace CommandHandlers
{
    public class CreateCampaignCommandHandler : AsyncRequestHandler<CreateCampaignCommand>
    {
        private readonly IMapper _mapper;
        private IBaseRepository<Campaign> _baseRepositoryCampaign;
        private IMediator _mediator;

        public CreateCampaignCommandHandler(IMapper mapper,
            IBaseRepository<Campaign> baseRepositoryCampaign,
            IMediator mediator
        )
        {
            _mapper = mapper;
            _baseRepositoryCampaign = baseRepositoryCampaign;
            _mediator = mediator;
        }

        protected override async Task Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
        {
            var campaign = _mapper.Map<CreateCampaignCommand, Campaign>(request);
            campaign.Id = Guid.NewGuid().ToString();
            await this.ExecuteCreateTimelineActionCommand(request.Properties, request.UserId, campaign.Id);
            await _baseRepositoryCampaign.Create(campaign);
        }

        private async Task ExecuteCreateTimelineActionCommand(List<PropertyForList> properties, string userId, string campaignId)
        {
            foreach(var property in properties)
            {
                CreateTimelineActionCommand cmd = new();
                cmd.Action = TimelineAction.CampaignAdd;
                cmd.NewEntry = new PropertyTimelineAction()
                {
                    PropertyId = property.Id,
                    UserId = userId,
                    Timestamp = DateTime.UtcNow,
                    CampaignId = campaignId
                };
                await _mediator.Send(cmd);
            }
        }
    }
}
