using System;
using System.Collections.Generic;

namespace Domains.Dtos
{
    public class TeamForUi
    {
        public string Id { get; set; }
        public string TeamName { get; set; }
        public string OrganizationId { get; set; }
        public List<UserForUi> Users { get; set; }
        public string Role { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
