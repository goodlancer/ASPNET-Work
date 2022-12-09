namespace Domains.DBModels
{
    public class TeamUserMapping : BaseEntity
    {
        public string TeamId { get; set; }
        public string UserId { get; set; }
        public string OrganizationId { get; set; }
    }
}
