using Commands.Query;
using Domains.DBModels.Questionnaire;
using Domains.Dtos;
using MediatR;
using Services.Repository;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class PrepareQuestionnaireQueryHandler : IRequestHandler<PrepareQuestionnaireQuery, List<RoomForUi<Question>>>
    {
        private IBaseRepository<Question> _questionRepo { get; set; }
        private IBaseRepository<Room> _roomRepo { get; set; }
        public PrepareQuestionnaireQueryHandler(IBaseRepository<Question> questionRepo, IBaseRepository<Room> roomRepo)
        {
            _questionRepo = questionRepo;
            _roomRepo = roomRepo;
        }

        public async Task<List<RoomForUi<Question>>> Handle(PrepareQuestionnaireQuery request, CancellationToken cancellationToken)
        {
            var result = new List<RoomForUi<Question>>();
            var roomsQuery = await _roomRepo.GetAllAsync(x => x.ProjectId == request.ProjectId);
            var rooms = roomsQuery?.ToList();
            if (rooms != null && !rooms.Any())
                return result;
            foreach(var room in rooms)
            {
                var questionsQuery = await _questionRepo.GetAllAsync(x => room.QuestionIds.Contains(x.Id));
                var images = room.ImageS3Keys.Select(x => new S3File() { S3Key = x });
                result.Add(new RoomForUi<Question>()
                {
                    Id = room.Id,
                    Name = room.Name,
                    IsFloorPlan = room.IsFloorPlan,
                    Images = images?.ToList(),
                    Questions = questionsQuery?.ToList()
                });
            }
            return result;
        }
    }
}
