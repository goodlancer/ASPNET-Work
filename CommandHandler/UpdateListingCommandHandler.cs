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
    public class UpdateListingCommandHandler : AsyncRequestHandler<UpdateListingCommand>
    {
        private readonly IMapper _mapper;
        private IBaseRepository<Listing> _baseRepositoryListing;
        private IMediator _mediator;

        public UpdateListingCommandHandler(IMapper mapper
            , IBaseRepository<Listing> _baseRepositoryListing
            , IMediator _mediator
        )
        {
            _mapper = mapper;
            this._baseRepositoryListing = _baseRepositoryListing;
            this._mediator = _mediator;
        }

        protected override async Task Handle(UpdateListingCommand request, CancellationToken cancellationToken)
        {
            var listing = _mapper.Map<UpdateListingCommand, Listing>(request);
            listing.Images = new System.Collections.Generic.List<PropertyImage>()
            {
                new PropertyImage(){ Image = "http://property.myadvtcorner.com/images/pic1.jpg" , IsPrimary = true},
                new PropertyImage(){ Image = "https://images.pexels.com/photos/106399/pexels-photo-106399.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500" , IsPrimary = false}
            };
            await ExecuteCreateTimelineActionCommand(listing.PropertyId, request.UserId, request.IsFromListingModule);
            await _baseRepositoryListing.UpdateAsync(listing);
        }
        private async Task ExecuteCreateTimelineActionCommand(string propertyId, string userId, bool isFromListingModule)
        {
            if (isFromListingModule) //TO-DO: Implement for listing module?
                return;
            CreateTimelineActionCommand cmd = new();
            cmd.Action = TimelineAction.MarketingUpdated;
            cmd.NewEntry = new PropertyTimelineAction()
            {
                PropertyId = propertyId,
                UserId = userId,
                Timestamp = DateTime.UtcNow
            };
            await this._mediator.Send(cmd);
        }
    }
}
