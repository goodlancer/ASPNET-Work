using Domains.Dtos;
using MediatR;

namespace Commands.Query
{
    public class RoomDetailsByIdQuery : IRequest<RoomForUi<QuestionForUi>>
    {
        public string Id { get; set; }
    }
}
