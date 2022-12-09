using AutoMapper;

using Commands;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class UpdateMailhouseCommandHandler : AsyncRequestHandler<UpdateMailhouseCommand>
    {
        private readonly IMapper _mapper;
        private IBaseRepository<Mailhouse> _baseRepositoryMailhouse;

        public UpdateMailhouseCommandHandler(IMapper mapper
            , IBaseRepository<Mailhouse> _baseRepositoryMailhouse
        )
        {
            _mapper = mapper;
            this._baseRepositoryMailhouse = _baseRepositoryMailhouse;
        }

        protected override async Task Handle(UpdateMailhouseCommand request, CancellationToken cancellationToken)
        {
            var mailhouse = _mapper.Map<UpdateMailhouseCommand, Mailhouse>(request);
            await _baseRepositoryMailhouse.UpdateAsync(mailhouse);
        }

    }
}
