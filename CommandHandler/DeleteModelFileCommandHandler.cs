using AutoMapper;
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
    public class DeleteModelFileCommandHandler : IRequestHandler<DeleteModelFileCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<ModelFile> _baseRepositoryModelFile;

        public DeleteModelFileCommandHandler(IMapper mapper, IBaseRepository<ModelFile> baseRepositoryModelFile)
        {
            _mapper = mapper;
            _baseRepositoryModelFile = baseRepositoryModelFile;
        }

        public async Task<bool> Handle(DeleteModelFileCommand request, CancellationToken cancellationToken)
        {
            await _baseRepositoryModelFile.Delete(request.FileId);
            return true;
        }

    }
}
