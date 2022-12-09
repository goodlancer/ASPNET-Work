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
    public class UpdateExpenditureCommandHandler : AsyncRequestHandler<UpdateExpenditureCommand>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Expenditure> _baseRepositoryExpenditure;

        public UpdateExpenditureCommandHandler(IMapper mapper, IBaseRepository<Expenditure> baseRepositoryExpenditure)
        {
            _mapper = mapper;
            _baseRepositoryExpenditure = baseRepositoryExpenditure;
        }

        protected override async Task Handle(UpdateExpenditureCommand request, CancellationToken cancellationToken)
        {
            var expenditureDb = await _baseRepositoryExpenditure.GetByIdAsync(request.Id);

            if (expenditureDb == null)
                return;

            expenditureDb.Description = request.Description;
            expenditureDb.Type = request.Type;
            expenditureDb.Amount = request.Amount;
            expenditureDb.Status = request.Status;
            await _baseRepositoryExpenditure.UpdateAsync(expenditureDb);
        }
    }
}
