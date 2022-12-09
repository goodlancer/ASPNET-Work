using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domains.DBModels
{
    public class UserRoleMapping : BaseEntity
    {
        public string RoleId { get; set; }
        public string UserId { get; set; }
        public string OrganizationId { get; set; }
    }
}
