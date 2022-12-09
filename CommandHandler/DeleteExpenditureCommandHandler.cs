using AutoMapper;

using Commands;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class DeleteExpenditureCommandHandler : IRequestHandler<DeleteExpenditureCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Expenditure> _baseRepositoryExpenditure;

        public DeleteExpenditureCommandHandler(IMapper mapper, IBaseRepository<Expenditure> baseRepositoryExpenditure)
        {
            _mapper = mapper;
            _baseRepositoryExpenditure = baseRepositoryExpenditure;
        }

        public async Task<bool> Handle(DeleteExpenditureCommand request, CancellationToken cancellationToken)
        {
            await _baseRepositoryExpenditure.Delete(request.ExpenditureId);
            return true;
        }

    }
}
