using Domains.DBModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands
{
    public class UploadImageCommand : BaseEntity,IRequest
    {
        public string Property { get; set; }
        public string ImageKey { get; set; }
    }
}
