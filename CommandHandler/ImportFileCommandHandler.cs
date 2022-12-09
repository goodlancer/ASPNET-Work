using Commands;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class ImportFileCommandHandler : IRequestHandler<ImportFileCommand, string>
    {
        private IBaseRepository<PropertiesFileImport> _baseRepositoryPropertiesFileImport;
        public ImportFileCommandHandler(IBaseRepository<PropertiesFileImport> baseRepositoryPropertiesFileImport)
        {
            _baseRepositoryPropertiesFileImport = baseRepositoryPropertiesFileImport;
        }
        public async Task<string> Handle(ImportFileCommand request, CancellationToken cancellationToken)
        {
            var importFile = new PropertiesFileImport
            {
                FileContent = request.FileContent,
                FileName = request.FileName,
                Extension = request.Extension,
                OrgId = request.OrgId,
                UploadDate = DateTime.UtcNow,
                UserId = request.UserId,
                UserName = request.UserName,
                Id = Guid.NewGuid().ToString(),
                Status = "imported"
            };
            await _baseRepositoryPropertiesFileImport.Create(importFile);
            return importFile.Id;
        }
    }
}
