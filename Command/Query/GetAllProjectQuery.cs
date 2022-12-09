using Domains.DBModels;
using Domains.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Commands.Query
{
    public class GetAllProjectQuery : Pagination, IRequest<ProjectsForUi>
    {
        [JsonIgnore]
        public string OrgId { get; set; }
        public string SearchKey { get; set; }
        public List<string> SearchStatuses { get; set; }
        public string TeamId { get; set; }
        public string UserId { get; set; }
        public List<string> RoleIds { get; set; }
    }
}
