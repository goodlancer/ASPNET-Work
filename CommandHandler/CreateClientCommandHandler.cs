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
    public class CreateClientCommandHandler : IRequestHandler<CreateClientsCommand, string>
    {
        private readonly IMapper _mapper;
        private IBaseRepository<Invitation> _baseRepositoryInvitation;

        public CreateClientCommandHandler(IMapper mapper, IBaseRepository<Invitation> baseRepositoryInvitation)
        {
            _mapper = mapper;
            _baseRepositoryInvitation = baseRepositoryInvitation;
        }

        public async Task<string> Handle(CreateClientsCommand request, CancellationToken cancellationToken)
        {
            var user = new Invitation()
            {
                OrgId = request.OrgId,
                Website = request.Website,
                CompanyAdress = request.CompanyAdress,
                Logo = request.Logo,
                SenderEmail = request.Email,
                PhoneNumber = request.PhoneNumber,
                CompanyName = request.CompanyName,
                TeamId = request.TeamId,
                IsInvited = request.IsInvited
            };
            user.Id = Guid.NewGuid().ToString();

            return await _baseRepositoryInvitation.Create(user);
        }
    }
}
