using System;

namespace Domains.DBModels
{
    public class DocumentTemplate : BaseEntity
    {
        public string TemplateName { get; set; }
        public string TemplateData { get; set; }

        public string OrgId { get; set; }

        public string TemplateType { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Status { get; set; }
    }
}
