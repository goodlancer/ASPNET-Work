using Commands.Query;
using Domains.DBModels;
using MediatR;
using Services.Repository;
using System.Threading;
using System.Threading.Tasks;
namespace CommandHandlers.QueryHandlers
{
    public class GetPropertyDocumentByIdQueryHandler : IRequestHandler<GetPropertyDocumentByIdQuery, PropertyDocument>
    {
        private readonly IBaseRepository<PropertyDocument> _propertyDocumentBaseRepository;
        public GetPropertyDocumentByIdQueryHandler(IBaseRepository<PropertyDocument> _propertyDocumentBaseRepository)
        {
            this._propertyDocumentBaseRepository = _propertyDocumentBaseRepository;
        }

        public async Task<PropertyDocument> Handle(GetPropertyDocumentByIdQuery request, CancellationToken cancellationToken)
        {
            var propertyDocument = await _propertyDocumentBaseRepository.GetByIdAsync(request.DocumentId);
            return propertyDocument;
        }
    }
}
