using Domains.DBModels;
using MediatR;
using System.Collections.Generic;

namespace Commands.Query
{
    public class GetPropertyDocumentQuery : IRequest<List<PropertyDocument>>
    {
        public string OrgId { get; set; }
    }
}
