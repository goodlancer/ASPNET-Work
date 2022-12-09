using Domains.DBModels.Questionnaire;
using Domains.Dtos;
using MediatR;
using System.Collections.Generic;

namespace Commands.Query
{
    public class RoomListQuery : IRequest<List<RoomForUi<Question>>>
    {
        public string ProjectId { get; set; }
    }
}
