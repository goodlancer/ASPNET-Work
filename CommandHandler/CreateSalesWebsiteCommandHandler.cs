using AutoMapper;

using Commands;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System;
using System.Threading;
using System.Threading.Tasks;
using static Domains.Enum.Enums;

namespace CommandHandlers
{
    public class CreateSalesWebsiteCommandHandler : IRequestHandler<CreateSalesWebsiteCommand,string>
    {
        private readonly IMapper _mapper;
        private IBaseRepository<SalesWebsite> _baseRepositorySalesWebsite;

        public CreateSalesWebsiteCommandHandler(IMapper mapper
            , IBaseRepository<SalesWebsite> _baseRepositorySalesWebsite
        )
        {
            _mapper = mapper;
            this._baseRepositorySalesWebsite = _baseRepositorySalesWebsite;
        }

        public async Task<string> Handle(CreateSalesWebsiteCommand request, CancellationToken cancellationToken)
        {
            if (request.Status == Domains.Enum.Enums.WebsiteStatus.Published)
            {
                var getWebsite = await _baseRepositorySalesWebsite.GetAllAsync(m => m.Status == Domains.Enum.Enums.WebsiteStatus.Published && m.OrganizationId == request.OrganizationId);
                foreach (var item in getWebsite)
                {
                    item.Status = Domains.Enum.Enums.WebsiteStatus.InActive;
                    await _baseRepositorySalesWebsite.UpdateAsync(item);
                }
            }
            var saleswebsite = _mapper.Map<CreateSalesWebsiteCommand, SalesWebsite>(request);
            saleswebsite.Id = Guid.NewGuid().ToString();
            saleswebsite.Status = request.Status;
            saleswebsite.CreatedOn = DateTime.Now;
            saleswebsite.CreatedBy = request.CreatedBy;
            await _baseRepositorySalesWebsite.Create(saleswebsite);
            return saleswebsite.Id;
        }

    }
}
