using Commands.Query;
using Domains.DBModels.Questionnaire;
using Domains.Dtos;
using MediatR;
using Services.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    public class RoomListQueryHandler : IRequestHandler<RoomListQuery, List<RoomForUi<Question>>>
    {
        private IBaseRepository<Room> _roomRepo { get; set; }
        private IBaseRepository<Question> _questionRepo { get; set; }
        public RoomListQueryHandler(IBaseRepository<Room> roomRepo, IBaseRepository<Question> questionRepo)
        {
            _roomRepo = roomRepo;
            _questionRepo = questionRepo;
        }
        public async Task<List<RoomForUi<Question>>> Handle(RoomListQuery request, CancellationToken cancellationToken)
        {
            var roomsQuery = await _roomRepo.GetAllAsync(x => x.ProjectId == request.ProjectId);
            var roomsList = roomsQuery.ToList();
            var result = new List<RoomForUi<Question>>();
            foreach(var room in roomsList)
            {
                var pendingQuestions = await _questionRepo.GetAllAsync(x => room.QuestionIds.Contains(x.Id) && string.IsNullOrEmpty(x.Answer));
                result.Add(new RoomForUi<Question>()
                {
                    Id = room.Id,
                    Name = room.Name,
                    IsFloorPlan = room.IsFloorPlan,
                    IsCompleted = !pendingQuestions.Any()
                    // Other properties in RoomForUi are not necessary in this query (for now)
                });
            }
            result.Sort(); // keep floor plan(s) at the top
            return result;
        }
    }
}
