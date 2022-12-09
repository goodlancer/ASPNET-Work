using Commands;
using Domains.DBModels;
using MediatR;
using Services.Repository;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class UpdateDocumentTagsCommandHandler : IRequestHandler<UpdateDocumentTagsCommand, bool>
    {
        private IBaseRepository<Document> _documentRepo { get; set; }
        public UpdateDocumentTagsCommandHandler(IBaseRepository<Document> documentRepo)
        {
            _documentRepo = documentRepo;
        }

        public async Task<bool> Handle(UpdateDocumentTagsCommand request, CancellationToken cancellationToken)
        {
            var documentQuery = await _documentRepo.GetAllAsync(x => x.S3Key == request.S3Key);
            var document = documentQuery.FirstOrDefault();
            document.TagsIds = request.Tags.Select(x => x.Id).ToList(); // Validated during query, no need to validate here
            await _documentRepo.UpdateAsync(document);
            return true;
        }
    }
}
