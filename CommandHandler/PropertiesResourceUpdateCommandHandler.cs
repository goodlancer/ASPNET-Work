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
    public class PropertiesStatusUpdateCommandHandler : IRequestHandler<PropertiesStatusUpdateCommand, bool>
    {
        private readonly IBaseRepository<Properties> _baseRepositoryProperties;
        private readonly IBaseRepository<Listing> _baseRepositoryListing;
        private IMediator _mediator;
        public PropertiesStatusUpdateCommandHandler(IBaseRepository<Properties> baseRepositoryProperties, IBaseRepository<Listing> baseRepositoryListing, IMediator mediator)
        {
            _baseRepositoryProperties = baseRepositoryProperties;
            _baseRepositoryListing = baseRepositoryListing;
            _mediator = mediator;
        }

        public async Task<bool> Handle(PropertiesStatusUpdateCommand request, CancellationToken cancellationToken)
        {
            var property = await _baseRepositoryProperties.GetByIdAsync(request.PropertiesId);

            await ExecuteCreateTimelineActionCommand(property.PropertyStatus, request.ResourceStatus, property.Id, request.UserId);
            if (property != null)
            {
                var listing = await _baseRepositoryListing.GetByIdAsync(property.ListingId);
                property.PropertyStatus = request.ResourceStatus.ToLower();
                if (request.ResourceStatus.ToLower() == "marketing")
                {
                    if (listing != null)
                    {
                        listing.IsMarketingSelected = true;
                        await _baseRepositoryListing.UpdateAsync(listing);
                    }
                }
                else
                {
                    if (listing is { IsMarketingSelected: true })
                    {
                        listing.IsMarketingSelected = false;
                        await _baseRepositoryListing.UpdateAsync(listing);
                    }
                }
                await _baseRepositoryProperties.UpdateAsync(property);
                return true;
            }
            return false;
        }
        private async Task ExecuteCreateTimelineActionCommand(string statusOld, string statusNew, string id, string userId)
        {
            CreateTimelineActionCommand cmd = new();
            cmd.Action = TimelineAction.StatusChange;
            cmd.NewEntry = new PropertyTimelineAction()
            {
                PropertyId = id,
                UserId = userId,
                Timestamp = DateTime.UtcNow,
                StatusBefore = statusOld,
                StatusAfter = statusNew
            };
            await _mediator.Send(cmd);
        }
    }
}
