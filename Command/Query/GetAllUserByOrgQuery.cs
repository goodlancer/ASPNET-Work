using Domains.Dtos;

using MediatR;

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Commands.Query
{
    public class GetAllUserByOrgQuery : Pagination, IRequest<List<UserForUi>>
    {
        [JsonIgnore]
        public string OrgId { get; set; }
        public string SearchKey { get; set; }
        public string[] FilterObj { get; set; }
        public string TeamId { get; set; }
        public string LoogedInUserId { get; set; }
        public List<string> RoleIds { get; set; }

    }
}
