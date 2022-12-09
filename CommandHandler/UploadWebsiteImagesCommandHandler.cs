using AutoMapper;
using Commands;
using Domains.DBModels;
using MediatR;
using Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class UploadWebsiteImagesCommandHandler : AsyncRequestHandler<UploadImageCommand>
    {

        private readonly IMapper _mapper;
        private readonly IBaseRepository<SalesWebsite> _baseRepositoryRole;

        public UploadWebsiteImagesCommandHandler(IMapper mapper
            , IBaseRepository<SalesWebsite> baseRepositoryRole
        )
        {
            _mapper = mapper;
            _baseRepositoryRole = baseRepositoryRole;
        }

        protected override async Task Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            var salesWebsite = await _baseRepositoryRole.GetSingleAsync(m => m.Id == request.Id);

            if (request.Property == "WebsiteLogo")
                salesWebsite.WebsiteLogo = request.ImageKey;
            else if (request.Property == "InventoryHeaderPhoto")
                salesWebsite.InventoryHeaderPhoto = request.ImageKey;
            else if (request.Property == "AboutPagePhoto")
                salesWebsite.AboutPagePhoto = request.ImageKey;

            await _baseRepositoryRole.UpdateAsync(salesWebsite);
        }
    }
}
