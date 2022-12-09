
using Commands;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class DeleteFileCommandHandler : AsyncRequestHandler<DeleteFileCommand>
    {
        private readonly IBaseRepository<PhFile> _baseRepositoryPhFile;

        public DeleteFileCommandHandler(IBaseRepository<PhFile> baseRepositoryPhFile)
        {
            this._baseRepositoryPhFile = baseRepositoryPhFile;
        }

        protected override async Task Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            await _baseRepositoryPhFile.Delete(request.FileId);
        }

    }
}
