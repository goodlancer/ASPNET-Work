using AutoMapper;

using Commands;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class DeleteIncomeCommandHandler : IRequestHandler<DeleteIncomeCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Income> _baseRepositoryIncome;

        public DeleteIncomeCommandHandler(IMapper mapper, IBaseRepository<Income> baseRepositoryIncome)
        {
            _mapper = mapper;
            _baseRepositoryIncome = baseRepositoryIncome;
        }

        public async Task<bool> Handle(DeleteIncomeCommand request, CancellationToken cancellationToken)
        {
            await _baseRepositoryIncome.Delete(request.IncomeId);
            return true;
        }

    }
}
