using AutoMapper;

using Commands;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class CreateFileCommandHandler : IRequestHandler<CreateFileCommand, string>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<PhFile> _baseRepositoryPhFiles;

        public CreateFileCommandHandler(IMapper mapper
            , IBaseRepository<PhFile> baseRepositoryPhFiles
        )
        {
            _mapper = mapper;
            _baseRepositoryPhFiles = baseRepositoryPhFiles;
        }

        public async Task<string> Handle(CreateFileCommand request, CancellationToken cancellationToken)
        {
            var file = _mapper.Map<CreateFileCommand, PhFile>(request);
            file.Id = file.FileKey;
            file.UploadDate = System.DateTime.UtcNow;
            await _baseRepositoryPhFiles.Create(file);

            return file.Id;
        }

    }
}
