using Commands;
using Domains.DBModels;
using MediatR;
using Services.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class DeleteDocumentCommandHandler : AsyncRequestHandler<DeleteDocumentCommand>
    {
        private IBaseRepository<Document> _documentRepo { get; set; }
        public DeleteDocumentCommandHandler(IBaseRepository<Document> documentRepo)
        {
            _documentRepo = documentRepo;
        }

        protected async override Task Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
        {
            await this._documentRepo.DeleteAllAsync(x => x.S3Key == request.S3FileName);
        }
    }
}
