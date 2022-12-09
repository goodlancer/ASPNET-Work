using Domains.Dtos;

using MediatR;

namespace Commands
{
    public class MapPropertiesColumnCommand : IRequest<ColumnMapResult>
    {
        public string FileId { get; set; }
        public string ListProvider { get; set; }
        public string PropertyType { get; set; }
    }
}
