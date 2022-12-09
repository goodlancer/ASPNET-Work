using MediatR;
using Newtonsoft.Json;

namespace Commands
{
    public class DeleteTemplateCommand : IRequest
    {
        public string TemplateId { get; set; }
    }
}
