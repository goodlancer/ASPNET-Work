using System;

namespace Domains.DBModels
{
    public class Mailhouse : BaseEntity
    {
        public string Name { get; set; }
        public string OrganizationId { get; set; }
        public string APIKey { get; set; }
        public string ContactEmail { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ContactName { get; set; }
        public string Status { get; set; }

    }
}
