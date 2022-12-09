using Domains.DBModels;
using MediatR;

namespace Commands.Query
{
    public class GetPropertyDocumentByIdQuery : IRequest<PropertyDocument>
    {
        public string OrgId { get; set; }
        public string DocumentId { get; set; }
    }
}
