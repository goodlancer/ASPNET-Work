using Domains.DBModels;
using Domains.Dtos;

using MediatR;

using System.Collections.Generic;

namespace Commands.Query
{
    public class GetAllTemplateQuery : Pagination, IRequest<List<DocumentTemplate>>
    {
        public string OrganizationId { get; set; }
        public string SearchKey { get; set; }
        public string[] FilterObj { get; set; }
    }
}
