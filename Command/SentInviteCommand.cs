using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Commands
{
    public class SentInviteCommand : IRequest<bool>
    {
        public string Id { get; set; }
        [JsonIgnore]
        public string OrgName { get; set; }
    }
}
