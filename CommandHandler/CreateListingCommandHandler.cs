using AutoMapper;

using Commands;
using Commands.Query;
using Domains.DBModels;

using MediatR;

using Services.Repository;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Domains.Enum.Enums;

namespace CommandHandlers
{
    public class CreateListingCommandHandler : IRequestHandler<CreateListingCommand, string>
    {
        private readonly IMapper _mapper;
        private IBaseRepository<Listing> _baseRepositoryListing;
        private IBaseRepository<Properties> _baseRepositoryProperty;
        private IMediator _mediator;

        public CreateListingCommandHandler(IMapper mapper,
            IBaseRepository<Listing> baseRepositoryListing,
            IBaseRepository<Properties> baseRepositoryProperty,
            IMediator mediator
        )
        {
            _mapper = mapper;
            _baseRepositoryListing = baseRepositoryListing;
            _baseRepositoryProperty = baseRepositoryProperty;
            _mediator = mediator;
        }

        public async Task<string> Handle(CreateListingCommand request, CancellationToken cancellationToken)
        {
            var listing = _mapper.Map<CreateListingCommand, Listing>(request);
            listing.Id = Guid.NewGuid().ToString();
            var propertiesFound = await _mediator.Send(new ApnLookupQuery()
            {
                OrgId = request.OrganizationId,
                Apn = request.APN
            });
            if (string.IsNullOrEmpty(request.PropertyId))
            {
                if (propertiesFound.Any())
                    return "";
                else
                    await _baseRepositoryListing.Create(listing);
            }
            else
            {
                if(propertiesFound.Count == 1)
                {
                    listing.PropertyId = request.PropertyId;
                    await _baseRepositoryListing.Create(listing);
                    var property = propertiesFound.First();
                    property.ListingId = listing.Id;
                    await _baseRepositoryProperty.UpdateAsync(property);
                    await ExecuteCreateTimelineActionCommand(listing.Id, listing.PropertyId, request.UserId, request.IsFromListingModule);
                }
                //else handle?
            }
            return listing.Id;
        }
        private async Task ExecuteCreateTimelineActionCommand(string listingId, string propertyId, string userId, bool isFromListingModule)
        {
            CreateTimelineActionCommand cmd = new();
            cmd.Action = isFromListingModule ? TimelineAction.Listing : TimelineAction.MarketingFilled;
            cmd.NewEntry = new PropertyTimelineAction()
            {
                PropertyId = propertyId,
                UserId = userId,
                Timestamp = DateTime.UtcNow,
                ListingId = listingId
            };
            await _mediator.Send(cmd);
        }
    }
}
