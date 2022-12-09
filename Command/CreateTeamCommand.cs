using MediatR;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Commands
{
    public class CreateTeamCommand : IRequest
    {
        public string TeamName { get; set; }

        [JsonIgnore]
        public string OrganizationId { get; set; }
        public string Role { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
