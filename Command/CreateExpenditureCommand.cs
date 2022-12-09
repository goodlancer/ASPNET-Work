using MediatR;

using System;
using System.Text.Json.Serialization;

namespace Commands
{
    public class CreateExpenditureCommand : IRequest
    {
        public string Description { get; set; }
        [JsonIgnore]
        public string OrgId { get; set; }
        public string Type { get; set; }
        public Decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
