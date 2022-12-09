namespace Domains.DBModels
{
    public class EmailTemplate : BaseEntity
    {
        public string TemplateName { get; set; }
        public string Subject { get; set; }
        public string TemplateBody { get; set; }
    }
}
