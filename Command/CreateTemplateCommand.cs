using MediatR;

using Newtonsoft.Json;

namespace Commands
{
    public class CreateTemplateCommand : IRequest
    {
        [JsonIgnore]
        public string OrganizationId { get; set; }
        public string TemplateName { get; set; }
        public string TemplateData { get; set; }
        public string TemplateType { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedAt { get; set; }
        public string Status { get; set; }

    }
}
