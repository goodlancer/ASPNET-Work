using Commands;
using Domains.DBModels;
using MediatR;
using Services.IManagers;
using Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class DeleteListingImageCommandHandler : AsyncRequestHandler<DeleteListingImageCommand>
    {
        private readonly IBaseRepository<Listing> _baseRepository;
        private IMappingService _mappingService;
        private readonly IBaseUserManager _userManager;

        public DeleteListingImageCommandHandler(IBaseRepository<Listing> baseRepository
            , IMappingService _mappingService
            , IBaseUserManager userManager)
        {
            this._baseRepository = baseRepository;
            this._mappingService = _mappingService;
            this._userManager = userManager;
        }

        protected override async Task Handle(DeleteListingImageCommand request, CancellationToken cancellationToken)
        {
            var getListing = await _baseRepository.GetSingleAsync(m=>m.OrganizationId ==request.OrgId && m.Id == request.Id);
            if(getListing.Images.Count > 0)
            {
                //take image item
                var getImage = getListing.Images.FirstOrDefault(m => m.Image == request.Image);
                if (getImage != null)
                    getListing.Images.Remove(getImage);
            }
            await _baseRepository.UpdateAsync(getListing);
        }
    }
}
