using System;

namespace Domains.DBModels
{
    public class Team : BaseEntity
    {
        public string TeamName { get; set; }
        public string OrganizationId { get; set; }
        public string Role { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
