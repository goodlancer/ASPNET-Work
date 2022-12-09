namespace Domains.DBModels
{
    public class RolePermissionMapping : BaseEntity
    {
        public string RoleId { get; set; }
        public string PermissionId { get; set; }
        public string PermissionKey { get; set; }
        public string OrganizationId { get; set; }
    }
}
