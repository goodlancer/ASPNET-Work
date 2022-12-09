namespace Domains.DBModels
{
    public class Permission : BaseEntity
    {
        public string Description { get; set; }
        public string Title { get; set; }
        public string Key { get; set; }
        public string Category { get; set; }
        public bool IsActive { get; set; }
        public bool IsShownInUi { get; set; }
        public string Group { get; set; }
    }
}
