using AutoMapper;
using Commands;
using Domains.DBModels;
using MediatR;
using Services.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class DeletePropertyDocumentHandler : AsyncRequestHandler<DeletePropertyDocumentCommand>
    {
        private IBaseRepository<PropertyDocument> _propertyDocumentBaseRepository;

        public DeletePropertyDocumentHandler(IMapper mapper
            , IBaseRepository<PropertyDocument> _propertyDocumentBaseRepository
        )
        {
            this._propertyDocumentBaseRepository = _propertyDocumentBaseRepository;
        }

        protected override async Task Handle(DeletePropertyDocumentCommand request, CancellationToken cancellationToken)
        {
            await _propertyDocumentBaseRepository.Delete(request.DocumentId);
        }
    }
}
