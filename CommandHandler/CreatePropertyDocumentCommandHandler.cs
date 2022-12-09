using AutoMapper;
using Commands;
using Domains.DBModels;
using MediatR;
using Services.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;
using static Domains.Enum.Enums;

namespace CommandHandlers
{
    public class CreatePropertyDocumentCommandHandler : AsyncRequestHandler<CreatePropertyDocumentCommand>
    {
        private readonly IMapper _mapper;
        private IBaseRepository<PropertyDocument> _baseRepositoryCampaign;
        private IMediator _mediator;

        public CreatePropertyDocumentCommandHandler(IMapper mapper,
            IBaseRepository<PropertyDocument> baseRepositoryCampaign,
            IMediator mediator
        )
        {
            _mapper = mapper;
            _baseRepositoryCampaign = baseRepositoryCampaign;
            _mediator = mediator;
        }

        protected override async Task Handle(CreatePropertyDocumentCommand request, CancellationToken cancellationToken)
        {
            foreach(var entry in request.List)
            {
                await ExecuteCreateTimelineActionCommand(entry, request.UserId);
                var pd = _mapper.Map<CreatePropertyDocument, PropertyDocument>(entry);
                await _baseRepositoryCampaign.Create(pd);
            }
        }

        private async Task ExecuteCreateTimelineActionCommand(CreatePropertyDocument entry, string userId)
        {
            CreateTimelineActionCommand cmd = new();
            cmd.Action = entry.DocumentType == "1" ? TimelineAction.OfferGenerated : TimelineAction.OtherDocGenerated;
            cmd.NewEntry = new PropertyTimelineAction()
            {
                PropertyId = entry.PropertyId,
                UserId = userId,
                Timestamp = DateTime.UtcNow,
                DocumentTemplateType = entry.DocumentType
            };
            await _mediator.Send(cmd);
        }
    }
}
