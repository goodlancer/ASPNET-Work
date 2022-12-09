using Domains.DBModels.Questionnaire;
using Domains.Dtos;
using MediatR;

namespace Commands
{
    public class SaveRoomCommand : IRequest<RoomForUi<Question>>
    {
        public string ProjectId { get; set; }
        public RoomForUi<Question> RoomForUi { get; set; }
    }
}
