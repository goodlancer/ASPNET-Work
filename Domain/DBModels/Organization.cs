namespace Domains.DBModels
{
    public class Organization : BaseEntity
    {
        public string Title { get; set; }
        public string ImageId { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string CreatedBy { get; set; }
        public string Image { get; set; }
        public string TimeZone { get; set; }
        public string Status { get; set; }
        public string AdminName { get; set; }
    }
}
