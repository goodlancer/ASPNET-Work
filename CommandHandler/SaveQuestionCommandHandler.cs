using Commands;
using Domains.DBModels.Questionnaire;
using MediatR;
using Services.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class SaveQuestionCommandHandler : IRequestHandler<SaveQuestionCommand, bool>
    {
        private IBaseRepository<Question> _questionRepo { get; set; }
        private IMediator _mediator { get; set; }
        public SaveQuestionCommandHandler(IBaseRepository<Question> questionRepo, IMediator mediator)
        {
            _questionRepo = questionRepo;
            _mediator = mediator;
        }

        public async Task<bool> Handle(SaveQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await _questionRepo.GetByIdAsync(request.Question.Id);
            var newAttachments = request.Question.Attachments?.Select(x => x.S3Key).ToList();
            question.Answer = request.Question.Answer;
#pragma warning disable CS4014 // Optional cleanup operation and need not be awaited
            CleanUpDeletedAttachmentsAsync(question.Attachments, newAttachments);
#pragma warning restore CS4014 
            question.Attachments = newAttachments;
            return await _questionRepo.UpdateAsync(question);
        }
        private async Task CleanUpDeletedAttachmentsAsync(List<string> existingAttachments, List<string> newAttachments)
        {
            if (existingAttachments == null)
                return;
            await _mediator.Send(new DeleteFilesFromS3Command()
            {
                KeysToDelete = existingAttachments.Where(x => !newAttachments.Contains(x)).ToList()
            });
        }
    }
}
