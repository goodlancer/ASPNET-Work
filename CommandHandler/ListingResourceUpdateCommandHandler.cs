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
    public class ListingResourceUpdateCommandHandler : IRequestHandler<ListingResourceUpdateCommand, bool>
    {
        private readonly IMapper _mapper;
        private IBaseRepository<Listing> _baseRepositoryListing;

        public ListingResourceUpdateCommandHandler(IMapper mapper
            , IBaseRepository<Listing> _baseRepositoryListing
        )
        {
            _mapper = mapper;
            this._baseRepositoryListing = _baseRepositoryListing;
        }


        public async Task<bool> Handle(ListingResourceUpdateCommand request, CancellationToken cancellationToken)
        {
            var property = await _baseRepositoryListing.GetSingleAsync(m => m.Id == request.Id && m.OrganizationId == request.OrgId);

            if (property != null)
            {
                if (request.ResourceType.ToLower() == "images")
                {
                    var findNewImages = new List<string>();
                    if (property.Images.Count > 0)
                    {
                        findNewImages = request.Keys.Where(m => !property.Images.Select(x => x.Image).ToList().Contains(m)).ToList();
                    }
                    else
                    {
                        findNewImages.AddRange(request.Keys);
                    }
                    int i = 0;
                    foreach (var item in findNewImages)
                    {
                        property.Images.Add(new PropertyImage() { Image = item , IsPrimary = i == 0 ? true : false });
                        i = 1;
                    }
                }
                //else if (request.ResourceType.ToLower() == "documents")
                //{
                //    property.Documents = request.Keys;
                //}

                await _baseRepositoryListing.UpdateAsync(property);
                return true;
            }
            return false;
        }
    }
}
