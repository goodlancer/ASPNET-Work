using Domains.DBModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands.Query
{
    public class GetClientsQuery : IRequest<Clients>
    {
        public string OrgId { get; set; }
        public string IncomeId { get; set; }
    }
}
