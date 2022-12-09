using Commands;
using Domains.DBModels.Questionnaire;
using Domains.Dtos;
using MediatR;
using Services.Repository;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class SaveRoomCommandHandler : IRequestHandler<SaveRoomCommand, RoomForUi<Question>>
    {
        private IBaseRepository<Question> _questionRepo { get; set; }
        private IBaseRepository<Room> _roomRepo { get; set; }
        private IMediator _mediator { get; set; }
        public SaveRoomCommandHandler(IBaseRepository<Question> questionRepo, IBaseRepository<Room> roomRepo, IMediator mediator)
        {
            _questionRepo = questionRepo;
            _roomRepo = roomRepo;
            _mediator = mediator;
        }
        public async Task<RoomForUi<Question>> Handle(SaveRoomCommand request, CancellationToken cancellationToken)
        {
            foreach (var question in request.RoomForUi.Questions)
            {
                if(string.IsNullOrEmpty(question.Id))
                    question.Id = await _questionRepo.Create(question);
                else
                    await _questionRepo.UpdateAsync(question);
            }

            var room = new Room()
            {
                Id = request.RoomForUi.Id,
                ProjectId = request.ProjectId,
                Name = request.RoomForUi.Name,
                IsFloorPlan = request.RoomForUi.IsFloorPlan,
                QuestionIds = request.RoomForUi.Questions.Select(x => x.Id).ToList(),
                ImageS3Keys = request.RoomForUi.Images.Select(x => x.S3Key).ToList()
            };

            if (string.IsNullOrEmpty(room.Id))
            {
                room.Id = null;
                room.Id = await _roomRepo.Create(room);
            }
            else
            {
                var existingRoom = await _roomRepo.GetByIdAsync(request.RoomForUi.Id);
#pragma warning disable CS4014 // These are optional cleanup operations and need not be awaited
                CleanUpDeletedQuestionsAsync(existingRoom, room);
                CleanUpDeletedFilesAsync(existingRoom, room);
#pragma warning restore CS4014
                await _roomRepo.UpdateAsync(room);
            }
                
            request.RoomForUi.Id = room.Id;
            return request.RoomForUi;
        }

        private async Task CleanUpDeletedFilesAsync(Room existingRoom, Room newRoom)
        {
            if (existingRoom == null)
                return;
            var keysToDelete = existingRoom.QuestionIds.Where(x => !newRoom.ImageS3Keys.Contains(x)).ToList();
            await _mediator.Send(new DeleteFilesFromS3Command()
            {
                KeysToDelete = keysToDelete
            });
        }

        private async Task CleanUpDeletedQuestionsAsync(Room existingRoom, Room newRoom)
        {
            if (existingRoom == null)
                return;
            var questionIdsToDelete = existingRoom.QuestionIds.Where(x => !newRoom.QuestionIds.Contains(x));
            if(questionIdsToDelete.Any())
                await _questionRepo.DeleteAllAsync(x => questionIdsToDelete.Contains(x.Id));
        }
    }
}
