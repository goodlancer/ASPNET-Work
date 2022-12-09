using MediatR;
using System;
using System.Collections.Generic;

namespace Commands
{
    public class CreatePropertyDocumentCommand : IRequest
    {
        public string UserId { get; set; }
        public List<CreatePropertyDocument> List { get; set; }
    }

    public class CreatePropertyDocument
    {
        public string Name { get; set; }
        public string DocumentType { get; set; }
        public string TemplateData { get; set; }
        public string PropertyId { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public DateTime AddedDate { get; set; }
        public string OrganizationId { get; set; }
    }
}
