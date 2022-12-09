using AutoMapper;

using Commands;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class CreateIncomeCommandHandler : AsyncRequestHandler<CreateIncomeCommand>
    {
        private readonly IMapper _mapper;
        private IBaseRepository<Income> _baseRepositoryIncome;

        public CreateIncomeCommandHandler(IMapper mapper, IBaseRepository<Income> baseRepositoryIncome)
        {
            _mapper = mapper;
            _baseRepositoryIncome = baseRepositoryIncome;
        }

        protected override async Task Handle(CreateIncomeCommand request, CancellationToken cancellationToken)
        {
            var income = new Income()
            {
                OrgId = request.OrgId,
                Description = request.Description,
                Type = request.Type,
                Amount = request.Amount,
                Status = request.Status,
                CreatedDate = request.CreatedDate
            };
            income.Id = Guid.NewGuid().ToString();
            await _baseRepositoryIncome.Create(income);
        }
    }
}
