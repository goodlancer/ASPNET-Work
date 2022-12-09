using Commands.Query;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    public class GetAllTemplateHandler : IRequestHandler<GetAllTemplateQuery, List<DocumentTemplate>>
    {
        private readonly IBaseRepository<DocumentTemplate> _documentListBaseRepository;
        private readonly IBaseRepository<User> _userListBaseRepository;
        public GetAllTemplateHandler(IBaseRepository<DocumentTemplate> templateListBaseRepository
            , IBaseRepository<User> userListBaseRepository)
        {
            this._documentListBaseRepository = templateListBaseRepository;
            _userListBaseRepository = userListBaseRepository;
        }

        public async Task<List<DocumentTemplate>> Handle(GetAllTemplateQuery request, CancellationToken cancellationToken)
        {
            var documentForList = new List<DocumentTemplate>();
            var documents = await _documentListBaseRepository.GetAllWithPagingAsync(x => x.OrgId == request.OrganizationId, request.PageNumber, request.PageSize);
            var allowed = new List<bool>();
            foreach (DocumentTemplate template in documents)
            {
                allowed.Add(true);
            }

            if (request.SearchKey != null && request.SearchKey.Length > 0)
            {
                int j = 0;
                foreach (DocumentTemplate template in documents)
                {
                    if (template.TemplateName.Contains(request.SearchKey) == false)
                        allowed[j] = false;
                    j++;
                }
            }

            int w = 0;
            if (request.FilterObj != null)
            {
                foreach (DocumentTemplate template in documents)
                {
                    if (request.FilterObj[0] != null && request.FilterObj[0].Length > 0 && template.TemplateType != request.FilterObj[0])
                        allowed[w] = false;
                    w++;
                }
            }

            w = 0;
            foreach (DocumentTemplate template in documents)
            {
                if (allowed[w])
                {
                    var user = await _userListBaseRepository.GetByIdAsync(template.CreatedBy);
                    template.CreatedBy = user.DisplayName;
                    documentForList.Add(template);
                }
                w++;
            }

            return documentForList;
        }
    }
}
