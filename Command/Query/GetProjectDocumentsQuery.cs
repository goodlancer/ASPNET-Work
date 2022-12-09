using Domains.Dtos;
using MediatR;
using System.Collections.Generic;

namespace Commands
{
    public class GetProjectDocumentsQuery : IRequest<List<DocumentForUi>>
    {
        public string ProjectId { get; set; }
        public string DocumentType { get; set; }
    }
}
