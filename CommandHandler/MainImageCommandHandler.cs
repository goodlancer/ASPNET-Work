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
    public class MainImageCommandHandler : AsyncRequestHandler<MainImageCommand>
    {
        private readonly IBaseRepository<Listing> _baseRepository;
        private IMappingService _mappingService;
        private readonly IBaseUserManager _userManager;

        public MainImageCommandHandler(IBaseRepository<Listing> baseRepository
            , IMappingService _mappingService
            , IBaseUserManager userManager)
        {
            this._baseRepository = baseRepository;
            this._mappingService = _mappingService;
            this._userManager = userManager;
        }

        protected override async Task Handle(MainImageCommand request, CancellationToken cancellationToken)
        {
            var getListing = await _baseRepository.GetSingleAsync(m => m.OrganizationId == request.OrgId && m.Id == request.Id);
            if (getListing.Images.Count > 0)
            {
                //take image item
                var getImage = getListing.Images.FirstOrDefault(m => m.Image == request.Image);
                if (getImage != null)
                {
                    getListing.Images.ForEach((item) => { item.IsPrimary = false; });
                    getImage.IsPrimary = true;
                }
                    
            }
            await _baseRepository.UpdateAsync(getListing);
        }
    }
}
