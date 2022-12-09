using Commands;
using Domains.DBModels;
using MediatR;
using MongoDB.Driver;
using Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class GetUserByRoleCommandHandler : IRequestHandler<GetUserByRoleCommand, List<GetUsers>>
    {
        private readonly IMongoScolptioDBContext _mongoContext;
        private readonly IMongoCollection<User> _dbUserCollection;
        private readonly IMongoCollection<Role> _dbRoleCollection;
        private readonly IMongoCollection<Team> _dbTeamCollection;

        public GetUserByRoleCommandHandler(IMongoScolptioDBContext context)
        {
            _mongoContext = context;
            _dbUserCollection = _mongoContext.GetCollection<User>("User");
            _dbRoleCollection = _mongoContext.GetCollection<Role>("Role");
            _dbTeamCollection = _mongoContext.GetCollection<Team>("Team");
        }

        public Task<List<GetUsers>> Handle(GetUserByRoleCommand request, CancellationToken cancellationToken)
        {
            var user = _dbUserCollection.AsQueryable()
                .Join(_dbTeamCollection.AsQueryable(), o => o.TeamId, m => m.Id, (x, y) => new { user = x, team = y })
                .Join(_dbRoleCollection.AsQueryable(),t=>t.team.Role , r => r.Id , (u,v)=> new { user = u, role = v })
                .Where(x => x.user.user.OrganizationId == request.OrgId && x.role.Title == request.Role)
                .Select(x => new GetUsers { Id = x.user.user.Id, Name = x.user.user.FirstName.ToString() + " " + x.user.user.LastName.ToString()  });
            var l =  user.ToList();
            return Task.FromResult(l);
        }

    }
}

