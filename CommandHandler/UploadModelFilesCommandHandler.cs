using AutoMapper;

using Commands;

using Domains.DBModels;

using MediatR;

using Services.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class UploadModelFilesCommandHandler : IRequestHandler<UploadModelFilesCommand, string>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<ModelFile> _baseRepositoryModelFiles;

        public UploadModelFilesCommandHandler(IMapper mapper
            , IBaseRepository<ModelFile> baseRepositoryModelFiles
        )
        {
            _mapper = mapper;
            _baseRepositoryModelFiles = baseRepositoryModelFiles;
        }

        public async Task<string> Handle(UploadModelFilesCommand request, CancellationToken cancellationToken)
        {
            var file = new ModelFile()
            {
                ProjectId = request.ProjectId,
                FileName = request.FileName,
                S3FileName = request.S3FileName,
                FolderName = request.FolderName,
                UploadDateTime = DateTime.UtcNow
            };
            file.Id = Guid.NewGuid().ToString();
            var result = await _baseRepositoryModelFiles.Create(file);

            return result;
        }
    }
}
