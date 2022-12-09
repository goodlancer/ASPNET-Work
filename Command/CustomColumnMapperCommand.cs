using MediatR;

using System.Collections.Generic;

namespace Commands
{
    public class CustomColumnMapperCommand : IRequest<bool>
    {
        public string FileId { get; set; }

        public Dictionary<string, string> ColumnMaps { get; set; }
    }
}
