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
    public class CreateProjectCommandHandler : AsyncRequestHandler<CreateProjectsCommand>
    {
        private readonly IMapper _mapper;
        private IBaseRepository<Projects> _baseRepositoryProject;
        private IBaseRepository<Clients> _baseRepositoryClients;

        public CreateProjectCommandHandler(IMapper mapper, IBaseRepository<Projects> baseRepositoryProject, IBaseRepository<Clients> baseRepositoryClients)
        {
            _mapper = mapper;
            _baseRepositoryProject = baseRepositoryProject;
            _baseRepositoryClients = baseRepositoryClients;
        }

        protected override async Task Handle(CreateProjectsCommand request, CancellationToken cancellationToken)
        {
            var client = await _baseRepositoryClients.GetByIdAsync(request.ClientId);
            var project = new Projects()
            {
                OrgId = request.OrgId,
                ProjectName = request.ProjectName,
                ClientId = request.ClientId,
                Cost = request.Cost,
                Deadline = request.Deadline,
                StartDate = request.StartDate,
                Status = request.Status,
                CreatedDate = request.CreatedDate,
                TeamId = client != null ? client.TeamId : string.Empty,
                DesignerIds = request.DesignerIds
            };
            project.Id = Guid.NewGuid().ToString();
            await _baseRepositoryProject.Create(project);
        }
    }
}
