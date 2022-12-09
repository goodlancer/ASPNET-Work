using AutoMapper;
using Commands;
using Domains.DBModels;
using MediatR;
using Services.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class UpdatePropertyDocumentCommandHandler : AsyncRequestHandler<UpdatePropertyDocumentCommand>
    {
        private readonly IMapper _mapper;
        private IBaseRepository<PropertyDocument> _baseRepositoryCampaign;

        public UpdatePropertyDocumentCommandHandler(IMapper mapper
            , IBaseRepository<PropertyDocument> _baseRepositoryCampaign
        )
        {
            _mapper = mapper;
            this._baseRepositoryCampaign = _baseRepositoryCampaign;
        }

        protected override async Task Handle(UpdatePropertyDocumentCommand request, CancellationToken cancellationToken)
        {
            var pd = _mapper.Map<UpdatePropertyDocumentCommand, PropertyDocument>(request);
            await _baseRepositoryCampaign.UpdateAsync(pd);
        }

    }
}
