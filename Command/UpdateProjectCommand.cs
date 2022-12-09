using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Commands
{
    public class UpdateProjectCommand : IRequest
    {
        public string Id { get; set; }
        public string ProjectName { get; set; }
        public string ClientId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Deadline { get; set; }
        public Decimal Cost { get; set; }
        public string Status { get; set; }
        [JsonIgnore]
        public string OrgId { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<string> DesignerIds { get; set; }
    }
}
