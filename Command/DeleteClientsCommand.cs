using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands
{
    public class DeleteClientsCommand : IRequest<bool>
    {
        public string Id { get; set; }
    }
}
