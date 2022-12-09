using System.Collections.Generic;

namespace Domains.DBModels
{
    public class RolePermissionMappingTemplate : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool IsActive { get; set; }
        public bool IsShownInUi { get; set; }
        public List<Permission> Permissions { get; set; }

        public RolePermissionMappingTemplate()
        {
            Permissions = new List<Permission>();
        }
    }
}
