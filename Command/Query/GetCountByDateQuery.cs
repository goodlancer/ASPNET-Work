using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands.Query
{
    public class GetCountByDateQuery : IRequest<decimal>
    {

        public string OrganizationId { get; set; }
        public string UserId { get; set; }
        public Type EntityName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string StartDateString { get; set; }
        public string EndDateString { get; set; }
        public string SectionName { get; set; }
    }

    public class GetOrderCountByDateQuery : IRequest<object>
    {
        public string OrganizationId { get; set; }
        public string UserId { get; set; }
        public Type EntityName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string StartDateString { get; set; }
        public string EndDateString { get; set; }
        public string SectionName { get; set; }
    }
}
