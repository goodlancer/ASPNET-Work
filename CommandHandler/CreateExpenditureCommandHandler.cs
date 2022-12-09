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
    public class CreateExpenditureCommandHandler : AsyncRequestHandler<CreateExpenditureCommand>
    {
        private readonly IMapper _mapper;
        private IBaseRepository<Expenditure> _baseRepositoryExpenditure;

        public CreateExpenditureCommandHandler(IMapper mapper, IBaseRepository<Expenditure> baseRepositoryExpenditure)
        {
            _mapper = mapper;
            _baseRepositoryExpenditure = baseRepositoryExpenditure;
        }

        protected override async Task Handle(CreateExpenditureCommand request, CancellationToken cancellationToken)
        {
            var expenditure = new Expenditure()
            {
                OrgId = request.OrgId,
                Description = request.Description,
                Type = request.Type,
                Amount = request.Amount,
                Status = request.Status,
                CreatedDate = request.CreatedDate
            };
            expenditure.Id = Guid.NewGuid().ToString();
            await _baseRepositoryExpenditure.Create(expenditure);
        }
    }
}
