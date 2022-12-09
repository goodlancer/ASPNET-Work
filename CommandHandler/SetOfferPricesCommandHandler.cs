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
    public class SetOfferPricesCommandHandler : AsyncRequestHandler<SetOfferPricesCommand>
    {
        private IBaseRepository<Properties> _propertiesRepository;
        private IMediator _mediator;
        public SetOfferPricesCommandHandler(IBaseRepository<Properties> propertiesRepository, IMediator mediator)
        {
            _propertiesRepository = propertiesRepository;
            _mediator = mediator;
        }
        protected override async Task Handle(SetOfferPricesCommand request, CancellationToken cancellationToken)
        {
            foreach (var entry in request.SetOfferPrices)
            {
                var property = await _propertiesRepository.GetByIdAsync(entry.PropertyId);
                await ExecuteCreateTimelineActionCommand(property.OfferPrice, entry, request.UserId);
                property.OfferPrice = entry.OfferPrice;
                await _propertiesRepository.UpdateAsync(property);
            }
        }
        private async Task ExecuteCreateTimelineActionCommand(string offerPrice, SetOfferPrice entry, string userId)
        {
            CreateTimelineActionCommand cmd = new();
            cmd.Action = TimelineAction.UpdateOfferPrice;
            cmd.NewEntry = new PropertyTimelineAction()
            {
                PropertyId = entry.PropertyId,
                UserId = userId,
                Timestamp = DateTime.Now,
                OfferPriceBefore = offerPrice,
                OfferPriceAfter = entry.OfferPrice
            };
            await _mediator.Send(cmd);
        }
    }
}
