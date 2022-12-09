using Commands.Query;
using Domains.DBModels.Questionnaire;
using Domains.Dtos;
using MediatR;
using Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    public class RoomDetailsByIdQueryHandler : IRequestHandler<RoomDetailsByIdQuery, RoomForUi<QuestionForUi>>
    {
        private IBaseRepository<Room> _roomRepo { get; set; }
        private IBaseRepository<Question> _questionRepo { get; set; }
        private IMediator _mediator { get; set; }
        public RoomDetailsByIdQueryHandler(IBaseRepository<Room> roomRepo, IBaseRepository<Question> questionRepo, IMediator mediator)
        {
            _roomRepo = roomRepo;
            _questionRepo = questionRepo;
            _mediator = mediator;
        }
        public async Task<RoomForUi<QuestionForUi>> Handle(RoomDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var room = await _roomRepo.GetByIdAsync(request.Id);
            var roomForUi = new RoomForUi<QuestionForUi>()
            {
                Id = room.Id,
                Name = room.Name,
                IsFloorPlan = room.IsFloorPlan,
                Images = room.ImageS3Keys.Select(x => new S3File() 
                { 
                    S3Key = x,
                    SafeUrl = _mediator.Send(new GetS3ObjectUrlQuery() { Key = x }).Result
                }).ToList(),
                Questions = new List<QuestionForUi>()
            };
            foreach (var id in room.QuestionIds)
            {
                var question = await _questionRepo.GetByIdAsync(id);
                var questionForUi = new QuestionForUi()
                {
                    Id = question.Id,
                    Interrogative = question.Interrogative,
                    Answer = question.Answer,
                    Attachments = question.Attachments?.Select(x => new S3File()
                    {
                        S3Key = x,
                        SafeUrl = _mediator.Send(new GetS3ObjectUrlQuery() { Key = x }).Result
                    }).ToList()
                };
                roomForUi.Questions.Add(questionForUi);
            }
            return roomForUi;
        }
    }
}
