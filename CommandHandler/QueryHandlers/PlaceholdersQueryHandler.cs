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
    public class PlaceholdersQueryHandler : IRequestHandler<PlaceholdersQuery, List<Placeholders>>
    {
        private readonly IBaseRepository<Placeholders> _placeHoldersRepository;
        public PlaceholdersQueryHandler(IBaseRepository<Placeholders> placeHoldersRepository)
        {
            _placeHoldersRepository = placeHoldersRepository;
        }
        public async Task<List<Placeholders>> Handle(PlaceholdersQuery request, CancellationToken cancellationToken)
        {
            var entries = await _placeHoldersRepository.GetAllAsync(x => true);
            return entries.ToList();
        }
    }
}
