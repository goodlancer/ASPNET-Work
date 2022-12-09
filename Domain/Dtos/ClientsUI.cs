using Domains.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Dtos
{
    public class ClientsUI : Clients
    {
        public string TeamName { get; set; }
    }
}
