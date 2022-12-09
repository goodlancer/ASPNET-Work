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
    public class GetAllIncomeQueryHandler : IRequestHandler<GetAllIncomeQuery, List<IncomeForUi>>
    {

        private readonly IBaseRepository<Income> _baseRepositoryIncome;
        private readonly IMapper _mapper;


        public GetAllIncomeQueryHandler(IMapper mapper, IBaseRepository<Income> baseRepositoryIncome)
        {
            _mapper = mapper;
            _baseRepositoryIncome = baseRepositoryIncome;
        }

        public async Task<List<IncomeForUi>> Handle(GetAllIncomeQuery request, CancellationToken cancellationToken)
        {
            var incomeForList = new List<IncomeForUi>();
            var incomeList = await _baseRepositoryIncome.GetAllWithPagingAsync(x => x.OrgId == request.OrgId, request.PageNumber, request.PageSize);

            var allowed = new List<bool>();
            foreach (var income in incomeList.ToList())
            {
                allowed.Add(true);
            }

            if (request.SearchKey != null && request.SearchKey.Length > 0)
            {
                int j = 0;
                foreach (var income in incomeList.ToList())
                {
                    var incomeForUi = new IncomeForUi()
                    {
                        Id = income.Id,
                        OrgId = income.OrgId,
                        Description = income.Description,
                        Type = income.Type,
                        Amount = income.Amount,
                        Status = income.Status,
                        CreatedDate = income.CreatedDate
                    };
                    if (incomeForUi.Description.Contains(request.SearchKey) == false)
                        allowed[j] = false;
                    j++;
                }
            }

            int w = 0;
            if (request.FilterObj != null)
            {
                foreach (var income in incomeList.ToList())
                {
                    var incomeForUi = new IncomeForUi()
                    {
                        Id = income.Id,
                        OrgId = income.OrgId,
                        Description = income.Description,
                        Type = income.Type,
                        Amount = income.Amount,
                        Status = income.Status,
                        CreatedDate = income.CreatedDate
                    };
                    if (request.FilterObj[0] != null && request.FilterObj[0].Length > 0 && incomeForUi.Type != request.FilterObj[0])
                        allowed[w] = false;
                    w++;
                }
            }

            w = 0;
            foreach (var income in incomeList.ToList())
            {
                if (allowed[w])
                {
                    var incomeForUi = new IncomeForUi()
                    {
                        Id = income.Id,
                        OrgId = income.OrgId,
                        Description = income.Description,
                        Type = income.Type,
                        Amount = income.Amount,
                        Status = income.Status,
                        CreatedDate = income.CreatedDate
                    };
                    incomeForList.Add(incomeForUi);
                }
                w++;
            }

            return incomeForList;
        }
    }
}
