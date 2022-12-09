using AutoMapper;
using Commands.Query;
using Domains.DBModels;
using Domains.Dtos;
using MediatR;
using Services.Repository;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    public class GetAllProjectQueryHandler : IRequestHandler<GetAllProjectQuery, ProjectsForUi>
    {

        private readonly IBaseRepository<Projects> _baseRepositoryProject;
        private readonly IBaseRepository<User> _baseRepositoryUser;
        private readonly IBaseRepository<Team> _baseRepositoryTeam;
        private readonly IBaseRepository<Role> _roleBaseRepository;

        public GetAllProjectQueryHandler(IBaseRepository<User> baseRepositoryUser, IBaseRepository<Role> roleBaseRepository, IBaseRepository<Team> baseRepositoryTeam, IBaseRepository<Projects> baseRepositoryProject)
        {
            _baseRepositoryProject = baseRepositoryProject;
            _baseRepositoryTeam = baseRepositoryTeam;
            _baseRepositoryUser = baseRepositoryUser;
            _roleBaseRepository = roleBaseRepository;
        }

        public async Task<ProjectsForUi> Handle(GetAllProjectQuery request, CancellationToken cancellationToken)
        {
            var projectUIList = new List<ProjectUI>();

            var roleList = await _roleBaseRepository.GetAllAsync(m => request.RoleIds.Contains(m.Id));
            var isAdmin = false;
            foreach (var item in roleList)
            {
                if (item.Title == "Admin")
                {
                    isAdmin = true;
                }
            }
            Expression<Func<Projects, bool>> condition = null;
            var clientIds = new List<string>();
            /*
             * For better performance the idea is to NOT query all projects, and then apply filters, but to apply filters during query. 
             * (It does depend on the implementation of GetAllWithPagingAsync also which is expected to do the same but with row count)
             * However to apply filter for client, client Ids must be queried first.
             */
            if (!string.IsNullOrEmpty(request.SearchKey))
            {
                var clientIdQuery = await _baseRepositoryUser.GetAllAsync(x =>
                    x.FirstName.Contains(request.SearchKey) ||
                    x.LastName.Contains(request.SearchKey) ||
                    x.DisplayName.Contains(request.SearchKey));
                clientIds = clientIdQuery.Select(x => x.Id).ToList();
            }
            else // Because clause after OR is evaluated by Linq even if first returns true
                request.SearchKey = "";
            if (!string.IsNullOrEmpty(request.UserId) && !isAdmin)
                condition = m =>
                            m.OrgId == request.OrgId &&
                            (m.ClientId == request.UserId || m.DesignerIds.Contains(request.UserId)) &&
                            (request.SearchKey == "" || (m.ProjectName.Contains(request.SearchKey)) || clientIds.Contains(m.ClientId)) &&
                            (!request.SearchStatuses.Any() || request.SearchStatuses.Contains(m.Status));
            else
                condition = m =>
                            m.OrgId == request.OrgId &&
                            (request.SearchKey == "" || (m.ProjectName.Contains(request.SearchKey)) || clientIds.Contains(m.ClientId)) &&
                            (!request.SearchStatuses.Any() || request.SearchStatuses.Contains(m.Status));

            var projectList = await _baseRepositoryProject.GetAllWithPagingAsync(condition, request.PageNumber, request.PageSize);
            var count = _baseRepositoryProject.GetTotalCount(condition);
            foreach (var item in projectList)
            {
                var clientName = await _baseRepositoryUser.GetSingleAsync(m => m.Id == item.ClientId);
                var team = await _baseRepositoryTeam.GetByIdAsync(item.TeamId);
                var projectUI = new ProjectUI()
                {
                    Id = item.Id,
                    ClientName = clientName!=null ? clientName.FirstName + " " + clientName.LastName : "",
                    ClientId = item.ClientId,
                    ClientLogo = clientName!=null ?  clientName.Logo : "",
                    ProjectName = item.ProjectName,
                    Cost = item.Cost,
                    StartDate = item.StartDate,
                    CreatedDate = item.CreatedDate,
                    Deadline = item.Deadline,
                    OrgId = item.OrgId,
                    Status = item.Status,
                    TeamName = team != null ? team.TeamName : "",
                    DesignerIds = item.DesignerIds
                };
                projectUIList.Add(projectUI);
            }
            return new ProjectsForUi()
            {
                Data = projectUIList,
                Count = count
            };
        }
    }
}
