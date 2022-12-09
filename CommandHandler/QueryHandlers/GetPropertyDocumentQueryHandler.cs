using Commands.Query;
using Domains.DBModels;
using MediatR;
using Services.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace CommandHandlers.QueryHandlers
{
    public class GetPropertyDocumentQueryHandler : IRequestHandler<GetPropertyDocumentQuery, List<PropertyDocument>>
    {
        private readonly IBaseRepository<PropertyDocument> _propertyDocumentBaseRepository;
        public GetPropertyDocumentQueryHandler(IBaseRepository<PropertyDocument> _propertyDocumentBaseRepository)
        {
            this._propertyDocumentBaseRepository = _propertyDocumentBaseRepository;
        }

        public async Task<List<PropertyDocument>> Handle(GetPropertyDocumentQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<PropertyDocument> propertyDocument;
            if (request.OrgId == null)
                propertyDocument = await _propertyDocumentBaseRepository.GetAllAsync(x => true);
            else
                propertyDocument = await _propertyDocumentBaseRepository.GetAllAsync(x => x.OrganizationId == request.OrgId);
            return propertyDocument?.ToList();
        }
    }
}
