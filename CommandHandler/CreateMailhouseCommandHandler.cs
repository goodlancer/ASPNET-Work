using AutoMapper;

using Commands;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class CreateMailhouseCommandHandler : AsyncRequestHandler<CreateMailhouseCommand>
    {
        private readonly IMapper _mapper;
        private IBaseRepository<Mailhouse> _baseRepositoryMailhouse;

        public CreateMailhouseCommandHandler(IMapper mapper
            , IBaseRepository<Mailhouse> _baseRepositoryMailhouse
        )
        {
            _mapper = mapper;
            this._baseRepositoryMailhouse = _baseRepositoryMailhouse;
        }

        protected override async Task Handle(CreateMailhouseCommand request, CancellationToken cancellationToken)
        {
            var mailhouse = _mapper.Map<CreateMailhouseCommand, Mailhouse>(request);
            mailhouse.Id = Guid.NewGuid().ToString();
            await _baseRepositoryMailhouse.Create(mailhouse);
        }

    }
}
