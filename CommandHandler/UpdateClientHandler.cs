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
    public class UpdateClientHandler : IRequestHandler<UpdateClientsCommand,User>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Clients> _baseRepositoryClients;
        private readonly IBaseRepository<Projects> _baseRepositoryProjects;
        private readonly IBaseRepository<User> _baseRepositoryUser;

        public UpdateClientHandler(IBaseRepository<User> baseRepositoryUser,IMapper mapper, IBaseRepository<Clients> baseRepositoryClients,IBaseRepository<Projects> baseRepositoryProjects)
        {
            _mapper = mapper;
            _baseRepositoryClients = baseRepositoryClients;
            _baseRepositoryProjects = baseRepositoryProjects;
            _baseRepositoryUser = baseRepositoryUser;
        }

        public async Task<User> Handle(UpdateClientsCommand request, CancellationToken cancellationToken)
        {
            var user = await _baseRepositoryUser.GetByIdAsync(request.Id);
            //var oldClient = (User)user.Shallowcopy();
            if (user == null)
                return null;


            //var projects = await _baseRepositoryProjects.GetAllAsync(m => m.TeamId == client.TeamId);
            //foreach (var item in projects)
            //{
            //    item.TeamId = request.TeamId;
            //    await _baseRepositoryProjects.UpdateAsync(item);
            //}

            //var users = await _baseRepositoryUser.GetAllAsync(m => m.TeamId == client.TeamId);
            //foreach (var item in users)
            //{
            //    item.TeamId = request.TeamId;
            //    await _baseRepositoryUser.UpdateAsync(item);
            //}

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Website = request.Website;
            user.CompanyAdress = request.CompanyAdress;
            user.Logo = request.Logo;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            user.CompanyName = request.CompanyName;
            user.IsInvited = request.IsInvited;

            //await _baseRepositoryClients.UpdateAsync(client);
            await _baseRepositoryUser.UpdateAsync(user);

            return user;



        }
    }
}
