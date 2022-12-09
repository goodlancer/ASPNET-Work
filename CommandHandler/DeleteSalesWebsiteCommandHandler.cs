using AutoMapper;

using Commands;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class DeleteSalesWebsiteCommandHandler : AsyncRequestHandler<DeleteSalesWebsiteCommand>
    {
        private readonly IMapper _mapper;
        private IBaseRepository<SalesWebsite> _baseRepositorySalesWebsite;

        public DeleteSalesWebsiteCommandHandler(IMapper mapper
            , IBaseRepository<SalesWebsite> _baseRepositorySalesWebsite
        )
        {
            _mapper = mapper;
            this._baseRepositorySalesWebsite = _baseRepositorySalesWebsite;
        }

        protected override async Task Handle(DeleteSalesWebsiteCommand request, CancellationToken cancellationToken)
        {
            await _baseRepositorySalesWebsite.Delete(request.SaleswebsiteId);
        }

    }
}
