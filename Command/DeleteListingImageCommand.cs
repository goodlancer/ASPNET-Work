using Domains.DBModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands
{
    public class DeleteListingImageCommand : BaseEntity, IRequest
    {

        public string Image { get; set; }
        public string OrgId { get; set; }
    }

    public class MainImageCommand : DeleteListingImageCommand
    {

    }
}
