using AutoMapper;
using Commands.Query;
using Domains.DBModels;
using Domains.Dtos;
using MediatR;
using Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    public class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, List<ClientsUI>>
    {

        private readonly IBaseRepository<Clients> _baseRepositoryClients;
        private readonly IBaseRepository<Team> _baseRepositoryTeams;
        private readonly IMapper _mapper;


        public GetAllClientsQueryHandler(IBaseRepository<Team> baseRepositoryTeams,IMapper mapper, IBaseRepository<Clients> baseRepositoryClients)
        {
            _mapper = mapper;
            _baseRepositoryClients = baseRepositoryClients;
            _baseRepositoryTeams = baseRepositoryTeams;
        }

        public async Task<List<ClientsUI>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var ClientListUI = new List<ClientsUI>();
            var clients = new List<Clients>();
            if (request.PageNumber > 0)
            {
                var clientList = await _baseRepositoryClients.GetAllWithPagingAsync(x => x.OrgId == request.OrgId, request.PageNumber, request.PageSize);
                if (!string.IsNullOrEmpty(request.TeamId))
                    clientList = clientList.Where(m => m.TeamId == request.TeamId);
                if (request.SearchKey != null)
                    clientList = clientList.Where(m => m.FirstName.Contains(request.SearchKey)).ToList();
                clients.AddRange(clientList.ToList());
            }
            else
            {
                var clientList = await _baseRepositoryClients.GetAllAsync(x => x.OrgId == request.OrgId);
                clients.AddRange(clientList.ToList());

            }

            foreach (var item in clients)
            {
                var clientUI = _mapper.Map<Clients, ClientsUI>(item);

                var team = await _baseRepositoryTeams.GetByIdAsync(item.TeamId);
                if(team!=null)
                clientUI.TeamName = team.TeamName;

                ClientListUI.Add(clientUI);
            }

            return ClientListUI.ToList();
        }
    }
}
