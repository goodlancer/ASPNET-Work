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
    public class PropertiesUpdateCommandHandler : IRequestHandler<PropertyUpdateCommand, bool>
    {
        private readonly IBaseRepository<Properties> _baseRepositoryProperties;
        private IMediator _mediator;
        public PropertiesUpdateCommandHandler(IBaseRepository<Properties> baseRepositoryProperties, IMediator mediator)
        {
            _baseRepositoryProperties = baseRepositoryProperties;
            _mediator = mediator;
        }

        public async Task<bool> Handle(PropertyUpdateCommand request, CancellationToken cancellationToken)
        {
            var property = await _baseRepositoryProperties.GetByIdAsync(request.PropertiesId);

            if (request.IsDueDiligenceTransaction)
            {
                var action = TimelineAction.DueDiligenceComplete;
                if (!string.IsNullOrEmpty(property.Zoning))
                    action = TimelineAction.DueDiligenceEdit;
                await ExecuteCreateTimelineActionCommand(property.Id, request.UserId, action);
            }
            else if (property.TotalAssessedValue != request.totalAssessedValue)
                await ExecuteCreateTimelineActionCommand(property.TotalAssessedValue, request.totalAssessedValue, property.Id, request.UserId);
            property.TotalAssessedValue = request.totalAssessedValue;
            property.propertyDimension = request.propertyDimension;
            property.hoaRestriction = request.hoaRestriction;
            property.zoningRestriction = request.zoningRestriction;
            property.accessType = request.accessType;
            property.visualAccess = request.visualAccess;
            property.topography = request.topography;
            property.powerAvailable = request.powerAvailable;
            property.gasAvailable = request.gasAvailable;
            property.pertTest = request.pertTest;
            property.floodZone = request.floodZone;
            property.survey = request.survey;
            property.Zoning= request.zoning;
            property.utilities = request.utilities;

            await _baseRepositoryProperties.UpdateAsync(property);

            return true;
        }

        private async Task ExecuteCreateTimelineActionCommand(string propertyId, string userId, TimelineAction action)
        {
            CreateTimelineActionCommand cmd = new();
            cmd.Action = action;
            cmd.NewEntry = new PropertyTimelineAction()
            {
                PropertyId = propertyId,
                UserId = userId,
                Timestamp = DateTime.UtcNow,
            };
            await _mediator.Send(cmd);
        }

        private async Task ExecuteCreateTimelineActionCommand(string marketValueBefore, string marketValueAfter, string propertyId, string userId)
        {
            CreateTimelineActionCommand cmd = new();
            cmd.Action = TimelineAction.MarketValueUpdated;
            cmd.NewEntry = new PropertyTimelineAction()
            {
                PropertyId = propertyId,
                UserId = userId,
                Timestamp = DateTime.UtcNow,
                MarketValueBefore = marketValueBefore,
                MarketValueAfter = marketValueAfter
            };
            await _mediator.Send(cmd);
        }
    }
}
