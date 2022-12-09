using Commands;
using Domains.DBModels.Questionnaire;
using MediatR;
using Services.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, bool>
    {
        private IBaseRepository<Question> _questionRepo { get; set; }
        private IBaseRepository<Room> _roomRepo { get; set; }
        public DeleteRoomCommandHandler(IBaseRepository<Question> questionRepo, IBaseRepository<Room> roomRepo)
        {
            _questionRepo = questionRepo;
            _roomRepo = roomRepo;
        }

        public async Task<bool> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            var room = await _roomRepo.GetByIdAsync(request.Id);
            var deleteQuestionsTask = _questionRepo.DeleteAllAsync(x => room.QuestionIds.Contains(x.Id));
            var deleteRoomTask = _roomRepo.Delete(request.Id);
            await Task.WhenAll(deleteQuestionsTask, deleteQuestionsTask);
            return true;
        }
    }
}
