using MediatR;

using System.Collections.Generic;

namespace Commands
{
    public class UpdateExpenditureCommand : IRequest
    {
        public string Id { get; set; }
        public string OrgId { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }

    }
}
