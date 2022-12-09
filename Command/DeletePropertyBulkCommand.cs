using MediatR;
using System.Collections.Generic;

namespace Commands
{
    public class DeletePropertyBulkCommand : IRequest
    {
        public List<string> PropertyIds { get; set; }
    }
}
