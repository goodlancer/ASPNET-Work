using AutoMapper;

using Commands.Query;

using Domains.DBModels;
using Domains.Dtos;

using MediatR;

using Services.Repository;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    public class GetAllExpenditureQueryHandler : IRequestHandler<GetAllExpenditureQuery, List<ExpenditureForUi>>
    {

        private readonly IBaseRepository<Expenditure> _baseRepositoryExpenditure;
        private readonly IMapper _mapper;


        public GetAllExpenditureQueryHandler(IMapper mapper, IBaseRepository<Expenditure> baseRepositoryExpenditure)
        {
            _mapper = mapper;
            _baseRepositoryExpenditure = baseRepositoryExpenditure;
        }

        public async Task<List<ExpenditureForUi>> Handle(GetAllExpenditureQuery request, CancellationToken cancellationToken)
        {
            var expenditureForList = new List<ExpenditureForUi>();
            var expenditureList = await _baseRepositoryExpenditure.GetAllWithPagingAsync(x => x.OrgId == request.OrgId, request.PageNumber, request.PageSize);

            var allowed = new List<bool>();
            foreach (var expenditure in expenditureList.ToList())
            {
                allowed.Add(true);
            }

            if (request.SearchKey != null && request.SearchKey.Length > 0)
            {
                int j = 0;
                foreach (var expenditure in expenditureList.ToList())
                {
                    var expenditureForUi = new ExpenditureForUi()
                    {
                        Id = expenditure.Id,
                        OrgId = expenditure.OrgId,
                        Description = expenditure.Description,
                        Type = expenditure.Type,
                        Amount = expenditure.Amount,
                        Status = expenditure.Status,
                        CreatedDate = expenditure.CreatedDate
                    };
                    if (expenditureForUi.Description.Contains(request.SearchKey) == false)
                        allowed[j] = false;
                    j++;
                }
            }

            int w = 0;
            if (request.FilterObj != null)
            {
                foreach (var expenditure in expenditureList.ToList())
                {
                    var expenditureForUi = new ExpenditureForUi()
                    {
                        Id = expenditure.Id,
                        OrgId = expenditure.OrgId,
                        Description = expenditure.Description,
                        Type = expenditure.Type,
                        Amount = expenditure.Amount,
                        Status = expenditure.Status,
                        CreatedDate = expenditure.CreatedDate
                    };
                    if (request.FilterObj[0] != null && request.FilterObj[0].Length > 0 && expenditureForUi.Type != request.FilterObj[0])
                        allowed[w] = false;
                    w++;
                }
            }

            w = 0;
            foreach (var expenditure in expenditureList.ToList())
            {
                if (allowed[w])
                {
                    var expenditureForUi = new ExpenditureForUi()
                    {
                        Id = expenditure.Id,
                        OrgId = expenditure.OrgId,
                        Description = expenditure.Description,
                        Type = expenditure.Type,
                        Amount = expenditure.Amount,
                        Status = expenditure.Status,
                        CreatedDate = expenditure.CreatedDate
                    };
                    expenditureForList.Add(expenditureForUi);
                }
                w++;
            }

            return expenditureForList;
        }
    }
}
