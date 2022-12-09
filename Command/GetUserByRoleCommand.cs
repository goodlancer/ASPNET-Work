using Domains.DBModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands
{
    public class GetUserByRoleCommand : IRequest<List<GetUsers>>
    {
        public string Role { get; set; }
        public string OrgId { get; set; }
    }

    public class GetUsers
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
