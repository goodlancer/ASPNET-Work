using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.DBModels
{
    public class UserOrganizationMapping:BaseEntity
    {
        public string UserId { get; set; }
        public string OrganizationId { get; set; }
    }
}
