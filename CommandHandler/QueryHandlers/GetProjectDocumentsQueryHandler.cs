using Commands;
using Domains.DBModels;
using Domains.Dtos;
using MediatR;
using Services.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class GetProjectDocumentsQueryHandler : IRequestHandler<GetProjectDocumentsQuery, List<DocumentForUi>>
    {
        private IBaseRepository<DocumentTypeMaster> _documentTypeMaster { get; set; }
        private IBaseRepository<Document> _documentRepo { get; set; }
        private IBaseRepository<Tag> _tagRepo;
        public GetProjectDocumentsQueryHandler(IBaseRepository<Document> documentRepo, IBaseRepository<DocumentTypeMaster> documentTypeMaster, IBaseRepository<Tag> tagRepo)
        {
            _documentRepo = documentRepo;
            _documentTypeMaster = documentTypeMaster;
            _tagRepo = tagRepo;
        }

        public async Task<List<DocumentForUi>> Handle(GetProjectDocumentsQuery request, CancellationToken cancellationToken)
        {
            var documentsForUi = new List<DocumentForUi>();
            var documentType = await _documentTypeMaster.GetAllAsync(x => x.Key == request.DocumentType);
            var id = documentType.FirstOrDefault().Id;
            var documents = await _documentRepo.GetAllAsync(x => x.ProjectId == request.ProjectId && x.DocumentTypeId == id);
            foreach (var document in documents)
            {
                var tagsQuery = await _tagRepo.GetAllAsync(x => document.TagsIds.Contains(x.Id));
                documentsForUi.Add(new DocumentForUi()
                {
                    Id = document.Id,
                    ProjectId = document.ProjectId,
                    DocumentTypeId = document.DocumentTypeId,
                    S3Key = document.S3Key,
                    FileName = document.FileName,
                    UploadDateTimeUtc = document.UploadDateTimeUtc,
                    Tags = tagsQuery.ToList()
                });
            }
            return documentsForUi;
        }
    }
}
