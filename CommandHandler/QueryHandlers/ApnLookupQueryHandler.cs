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
    public class ApnLookupQueryHandler : IRequestHandler<ApnLookupQuery, List<Properties>>
    {
        private readonly IBaseRepository<Properties> _propertyRepo;
        public ApnLookupQueryHandler(IBaseRepository<Properties> propertyRepo)
        {
            this._propertyRepo = propertyRepo;
        }
        public async Task<List<Properties>> Handle(ApnLookupQuery request, CancellationToken cancellationToken)
        {
            var qry = await _propertyRepo.GetAllAsync(x => (x.APN == request.Apn || x.APNUnformatted == request.Apn) && x.OrgId == request.OrgId);
            var list = qry.ToList();
            return list;
        }
    }
}
