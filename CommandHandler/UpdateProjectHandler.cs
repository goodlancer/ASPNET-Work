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
    public class UpdateProjectHandler : AsyncRequestHandler<UpdateProjectCommand>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Projects> _baseRepositoryProjects;
        private IBaseRepository<Clients> _baseRepositoryClients;
        public UpdateProjectHandler(IMapper mapper, IBaseRepository<Projects> baseRepositoryProjects, IBaseRepository<Clients> baseRepositoryClients)
        {
            _mapper = mapper;
            _baseRepositoryProjects = baseRepositoryProjects;
            _baseRepositoryClients = baseRepositoryClients;
        }

        protected override async Task Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var Project = await _baseRepositoryProjects.GetByIdAsync(request.Id);
            var client = await _baseRepositoryClients.GetByIdAsync(request.ClientId);
            if (Project == null)
                return;

            Project.ProjectName = request.ProjectName;
            Project.ClientId = request.ClientId;
            Project.Cost = request.Cost;
            Project.Deadline = request.Deadline;
            Project.StartDate = request.StartDate;
            Project.Status = request.Status;
            Project.TeamId = client != null ? client.TeamId : string.Empty;
            Project.DesignerIds = request.DesignerIds;
            await _baseRepositoryProjects.UpdateAsync(Project);
        }
    }
}
