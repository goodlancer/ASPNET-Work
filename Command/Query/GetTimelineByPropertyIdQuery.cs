using Domains.Dtos;
using MediatR;

namespace Commands.Query
{
    public class GetTimelineByPropertyIdQuery : IRequest<PropertyTimelineForUi>
    {
        public string PropertyId { get; set; }
    }
}