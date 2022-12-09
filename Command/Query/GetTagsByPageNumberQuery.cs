using Domains.Dtos;
using MediatR;

namespace Commands.Query
{
    public class GetTagsByPageNumberQuery : Pagination, IRequest<TagsForUi>
    {
        public string SearchKey { get; set; }
        public string OrgId { get; set; }
    }
}
