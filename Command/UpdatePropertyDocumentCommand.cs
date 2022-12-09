using MediatR;
using System;

namespace Commands
{
    public class UpdatePropertyDocumentCommand : IRequest
    {
        public string Name { get; set; }
        public string DocumentType { get; set; }
        public string TemplateData { get; set; }
        public string PropertyId { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public DateTime AddedDate { get; set; }
        public string OrganizationId { get; set; }
        public string Id { get; set; }
    }
}
