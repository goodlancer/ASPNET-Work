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
    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectsCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Projects> _baseRepositoryProject;

        public DeleteProjectCommandHandler(IMapper mapper, IBaseRepository<Projects> baseRepositoryProject)
        {
            _mapper = mapper;
            _baseRepositoryProject = baseRepositoryProject;
        }

        public async Task<bool> Handle(DeleteProjectsCommand request, CancellationToken cancellationToken)
        {
            await _baseRepositoryProject.Delete(request.Id);
            return true;
        }

    }
}
