using MediatR;

using Newtonsoft.Json;

namespace Commands
{
    public class UpdateTemplateCommand : IRequest
    {
        [JsonIgnore]
        public string OrganizationId { get; set; }
        public string TemplateId { get; set; }
        public string TemplateName { get; set; }
        public string TemplateData { get; set; }
        public string TemplateType { get; set; }
        public string Status { get; set; }

    }
}
