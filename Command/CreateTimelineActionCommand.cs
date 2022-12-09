using Domains.DBModels;
using MediatR;
using static Domains.Enum.Enums;

namespace Commands
{
    public class CreateTimelineActionCommand : IRequest
    {
        public TimelineAction Action { get; set; }
        public PropertyTimelineAction NewEntry { get; set; }

    }
}
