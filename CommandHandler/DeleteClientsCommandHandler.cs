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
    public class DeleteClientsCommandHandler : IRequestHandler<DeleteClientsCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Clients> _baseRepositoryClients;
        private readonly IBaseRepository<Projects> _baseRepositoryProjects;

        public DeleteClientsCommandHandler(IMapper mapper, IBaseRepository<Clients> baseRepositoryClients, IBaseRepository<Projects> baseRepositoryProjects)
        {
            _mapper = mapper;
            _baseRepositoryClients = baseRepositoryClients;
            _baseRepositoryProjects = baseRepositoryProjects;
        }

        public async Task<bool> Handle(DeleteClientsCommand request, CancellationToken cancellationToken)
        {
            var getProject = _baseRepositoryProjects.GetTotalCount(m => m.ClientId == request.Id);
            if (getProject > 0)
                return false;
            await _baseRepositoryClients.Delete(request.Id);
            return true;
        }

    }
}
