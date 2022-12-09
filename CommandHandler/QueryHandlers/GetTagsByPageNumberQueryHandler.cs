using Commands.Query;
using Domains.DBModels;
using Domains.Dtos;
using MediatR;
using Services.Repository;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    public class GetTagsByPageNumberQueryHandler : IRequestHandler<GetTagsByPageNumberQuery, TagsForUi>
    {
        private IBaseRepository<Tag> _tagRepo;
        public GetTagsByPageNumberQueryHandler(IBaseRepository<Tag> tagRepo)
        {
            this._tagRepo = tagRepo;
        }
        public async Task<TagsForUi> Handle(GetTagsByPageNumberQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Tag, bool>> condition = null;
            if (string.IsNullOrEmpty(request.SearchKey))
                condition = x => (x.OrgId == request.OrgId || string.IsNullOrEmpty(x.OrgId));
            else
                condition = x => (x.OrgId == request.OrgId || string.IsNullOrEmpty(x.OrgId)) && x.Name.Contains(request.SearchKey);
            var tagsWithPagination = await _tagRepo.GetAllWithPagingAsync(condition, request.PageNumber, request.PageSize);
            var count = _tagRepo.GetTotalCount(x => true);
            var tagsForUi = new TagsForUi()
            {
                Count = count,
                Tags = tagsWithPagination.ToList()
            };
            return tagsForUi;
        }
    }
}
