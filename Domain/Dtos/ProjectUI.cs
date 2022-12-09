using Domains.DBModels;
using System;
using System.Collections.Generic;

namespace Domains.Dtos
{
    public class ProjectUI : BaseEntity
    {
        public string ProjectName { get; set; }
        public string ClientName { get; set; }
        public string ClientId { get; set; }
        public string ClientLogo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Deadline { get; set; }
        public Decimal Cost { get; set; }
        public string Status { get; set; }
        public string OrgId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string TeamName { get; set; }
        public List<string> DesignerIds { get; set; }
    }
}
