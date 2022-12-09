using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Commands
{
    public class ListingResourceUpdateCommand : IRequest<bool>
    {
        public string OrgId { get; set; }
        public string Id { get; set; }
        public string ResourceType { get; set; }
        public string[] Keys { get; set; }
    }
}
