using System;
using System.Collections.Generic;

namespace Domains.Dtos
{
    public class IncomeForUi
    {
        public string Id { get; set; }
        public string OrgId { get; set; }
        public string Description{ get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
