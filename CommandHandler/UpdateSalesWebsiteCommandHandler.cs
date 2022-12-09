using AutoMapper;

using Commands;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class UpdateSalesWebsiteCommandHandler : AsyncRequestHandler<UpdateSalesWebsiteCommand>
    {
        private readonly IMapper _mapper;
        private IBaseRepository<SalesWebsite> _baseRepositorySalesWebsite;

        public UpdateSalesWebsiteCommandHandler(IMapper mapper
            , IBaseRepository<SalesWebsite> _baseRepositorySalesWebsite
        )
        {
            _mapper = mapper;
            this._baseRepositorySalesWebsite = _baseRepositorySalesWebsite;
        }

        protected override async Task Handle(UpdateSalesWebsiteCommand request, CancellationToken cancellationToken)
        {
            if(request.Status == Domains.Enum.Enums.WebsiteStatus.Published)
            {
                var getWebsite = await _baseRepositorySalesWebsite.GetAllAsync(m => m.Status == Domains.Enum.Enums.WebsiteStatus.Published && m.OrganizationId == request.OrganizationId && m.Id != request.Id);
                foreach(var item in getWebsite)
                {
                    item.Status = Domains.Enum.Enums.WebsiteStatus.InActive;
                    await _baseRepositorySalesWebsite.UpdateAsync(item);
                }
            }
            var salesWebsiteObject = await _baseRepositorySalesWebsite.GetByIdAsync(request.Id);
            var saleswebsite = _mapper.Map<UpdateSalesWebsiteCommand, SalesWebsite>(request, salesWebsiteObject);
            await _baseRepositorySalesWebsite.UpdateAsync(saleswebsite);
        }

    }
}
