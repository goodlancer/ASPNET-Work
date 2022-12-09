using AutoMapper;

using Commands;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class DeleteMailhouseCommandHandler : AsyncRequestHandler<DeleteMailhouseCommand>
    {
        private readonly IMapper _mapper;
        private IBaseRepository<Mailhouse> _baseRepositoryMailhouse;

        public DeleteMailhouseCommandHandler(IMapper mapper
            , IBaseRepository<Mailhouse> _baseRepositoryMailhouse
        )
        {
            _mapper = mapper;
            this._baseRepositoryMailhouse = _baseRepositoryMailhouse;
        }

        protected override async Task Handle(DeleteMailhouseCommand request, CancellationToken cancellationToken)
        {
            await _baseRepositoryMailhouse.Delete(request.MailhouseId);
        }

    }
}
