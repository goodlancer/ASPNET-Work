using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.DBModels
{
    public class PropertyDocument : BaseEntity
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
