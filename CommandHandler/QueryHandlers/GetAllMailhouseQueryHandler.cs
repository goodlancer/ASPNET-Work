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
    public class GetAllMailhouseQueryHandler : IRequestHandler<GetAllMailhouseQuery, List<Mailhouse>>
    {
        private readonly IBaseRepository<Mailhouse> _mailhouseBaseRepository;
        public GetAllMailhouseQueryHandler(IBaseRepository<Mailhouse> _mailhouseBaseRepository)
        {
            this._mailhouseBaseRepository = _mailhouseBaseRepository;
        }
        public async Task<List<Mailhouse>> Handle(GetAllMailhouseQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Mailhouse> mailHouses;
            if (request.OrgId == null)
            {
                mailHouses = await _mailhouseBaseRepository.GetAllAsync(x => true);
            }
            else
            {
                mailHouses = await _mailhouseBaseRepository.GetAllAsync(x => x.OrganizationId == request.OrgId);
            }
            return mailHouses?.ToList();
        }
    }
}
