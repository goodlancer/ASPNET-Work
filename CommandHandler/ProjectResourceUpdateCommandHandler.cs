using Commands;
using Domains.DBModels;
using MediatR;
using Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class ProjectResourceUpdateCommandHandler : IRequestHandler<ProjectResourceUpdateCommand, bool>
    {
        private IBaseRepository<DocumentTypeMaster> _documentTypeMaster { get; set; }
        private IBaseRepository<Document> _documentRepo { get; set; }
        private IMediator _mediator { get; set; }
        public ProjectResourceUpdateCommandHandler(IBaseRepository<Document> documentRepo, IBaseRepository<DocumentTypeMaster> documentTypeMaster, IMediator mediator)
        {
            _documentRepo = documentRepo;
            _documentTypeMaster = documentTypeMaster;
            _mediator = mediator;
        }
        public async Task<bool> Handle(ProjectResourceUpdateCommand request, CancellationToken cancellationToken)
        {
            var documentType = await _documentTypeMaster.GetAllAsync(x => x.Key == request.ResourceType);
            var id = documentType.FirstOrDefault().Id;
            var currentDocumentsQuery = await _documentRepo.GetAllAsync(x => x.ProjectId == request.ProjectId && x.DocumentTypeId == id);
            var currentS3Keys = currentDocumentsQuery?.Select(x => x.S3Key)?.ToList();
            var keysToDelete = currentS3Keys?.Where(x => !request.Files.Select(x => x.S3Key).Contains(x)).ToList();
            if(keysToDelete != null && keysToDelete.Any())
            {
#pragma warning disable CS4014 // Optional cleanup
                _mediator.Send(new DeleteFilesFromS3Command() { KeysToDelete = keysToDelete });
#pragma warning restore CS4014
                await _documentRepo.DeleteAllAsync(x => keysToDelete.Contains(x.S3Key));
            }
            foreach(var file in request.Files)
            {
                if (currentS3Keys.Contains(file.S3Key))
                    continue;
                var document = new Document()
                {
                    ProjectId = request.ProjectId,
                    DocumentTypeId = id,
                    S3Key = file.S3Key,
                    FileName = file.FileName,
                    UploadDateTimeUtc = DateTime.UtcNow,
                    TagsIds = file.Tags.Select(x => x.Id).ToList()
                };
                await _documentRepo.Create(document);
            }
            return true;
        }
    }
}
