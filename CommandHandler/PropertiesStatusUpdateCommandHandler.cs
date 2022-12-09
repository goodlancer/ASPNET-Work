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
    public class PropertiesResourceUpdateCommandHandler : IRequestHandler<PropertiesResourceUpdateCommand, bool>
    {
        private readonly IBaseRepository<Properties> _baseRepositoryProperties;
        private readonly IMediator _mediator;
        public PropertiesResourceUpdateCommandHandler(IBaseRepository<Properties> baseRepositoryProperties, IMediator mediator)
        {
            _baseRepositoryProperties = baseRepositoryProperties;
            _mediator = mediator;
        }

        public async Task<bool> Handle(PropertiesResourceUpdateCommand request, CancellationToken cancellationToken)
        {
            var property = await _baseRepositoryProperties.GetByIdAsync(request.PropertiesId);

            if (property != null)
            {
                if (request.ResourceType.ToLower() == "images")
                {
                    var action = TimelineAction.ImageAdd;
                    if (request.Keys.Length < property.Images?.Length)
                        action = TimelineAction.ImageDelete;
                    await ExecuteCreateTimelineActionCommand(property.Id, request.UserId, action); 
                    property.Images = request.Keys;
                }
                else if (request.ResourceType.ToLower() == "documents")
                {
                    var action = TimelineAction.DocumentAdd;
                    if (request.Keys.Length < property.Documents?.Length)
                        action = TimelineAction.DocumentDelete;
                    await ExecuteCreateTimelineActionCommand(property.Id, request.UserId, action);
                    property.Documents = request.Keys;
                }
                else if (request.ResourceType.ToLower() == "listing")
                {
                    property.ListingId = request.Keys[0];
                }

                await _baseRepositoryProperties.UpdateAsync(property);
                return true;
            }
            return false;
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
    }
}
