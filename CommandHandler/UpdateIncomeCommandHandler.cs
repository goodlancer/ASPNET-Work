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
    public class UpdateIncomeCommandHandler : AsyncRequestHandler<UpdateIncomeCommand>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Income> _baseRepositoryIncome;

        public UpdateIncomeCommandHandler(IMapper mapper, IBaseRepository<Income> baseRepositoryIncome)
        {
            _mapper = mapper;
            _baseRepositoryIncome = baseRepositoryIncome;
        }

        protected override async Task Handle(UpdateIncomeCommand request, CancellationToken cancellationToken)
        {
            var incomeDb = await _baseRepositoryIncome.GetByIdAsync(request.Id);

            if (incomeDb == null)
                return;

            incomeDb.Description = request.Description;
            incomeDb.Type = request.Type;
            incomeDb.Amount = request.Amount;
            incomeDb.Status = request.Status;
            await _baseRepositoryIncome.UpdateAsync(incomeDb);
        }
    }
}
