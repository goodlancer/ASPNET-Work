using AutoMapper;

using Commands;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class DeleteListingCommandHandler : AsyncRequestHandler<DeleteListingCommand>
    {
        private readonly IMapper _mapper;
        private IBaseRepository<Listing> _baseRepositoryListing;

        public DeleteListingCommandHandler(IMapper mapper
            , IBaseRepository<Listing> _baseRepositoryListing
        )
        {
            _mapper = mapper;
            this._baseRepositoryListing = _baseRepositoryListing;
        }

        protected override async Task Handle(DeleteListingCommand request, CancellationToken cancellationToken)
        {
            await _baseRepositoryListing.Delete(request.ListingId);
        }

    }
}
