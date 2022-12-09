using AutoMapper;

using Commands.Query;

using Domains.DBModels;
using Domains.Dtos;

using MediatR;

using Services.Repository;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Numerics;

namespace CommandHandlers.QueryHandlers
{
    public class GetAllPropertiesQueryHandler : IRequestHandler<GetAllPropertiesQuery, List<PropertyForList>>
    {
        private readonly IBaseRepository<Properties> _baseRepositoryProperties;
        private readonly IMapper _mapper;

        public GetAllPropertiesQueryHandler(IMapper mapper
             , IBaseRepository<Properties> baseRepositoryProperties
           )
        {
            _mapper = mapper;
            _baseRepositoryProperties = baseRepositoryProperties;
        }

        public async Task<List<PropertyForList>> Handle(GetAllPropertiesQuery request, CancellationToken cancellationToken)
        {
            var propertyForList = new List<PropertyForList>();
            var properties = await _baseRepositoryProperties.GetAllWithPagingAsync(x => x.OrgId == request.OrgId, request.PageNumber, request.PageSize);
            var allowed = new List<bool>();
            foreach (var property in properties.ToList())
            {
                allowed.Add(true);
            }

            if (request.SearchKey != null && request.SearchKey.Length > 0)
            {
                int j = 0;
                foreach (var property in properties.ToList())
                {
                    if (property.PropertyAddress.Contains(request.SearchKey) == false && property.OwnerName.Contains(request.SearchKey) == false && property.APN.Contains(request.SearchKey) == false)
                        allowed[j] = false;
                    j++;
                }
            }

            int w = 0;
            if(request.FilterObj != null)
            {
                foreach (var property in properties.ToList())
                {
                    if (request.FilterObj[0] != null && request.FilterObj[0].Length > 0 && property.PZip.ToLower().Contains(request.FilterObj[0].ToLower()) == false)
                        allowed[w] = false;
                    if (request.FilterObj[1] != null && request.FilterObj[1].Length > 0 && property.OwnerName.ToLower().Contains(request.FilterObj[1].ToLower()) == false)
                        allowed[w] = false;
                    if (request.FilterObj[2] != null && request.FilterObj[2].Length > 0 && property.PCity.ToLower().Contains(request.FilterObj[2].ToLower()) == false)
                        allowed[w] = false;
                    if (request.FilterObj[3] != null && request.FilterObj[3].Length > 0 && property.CountyName != request.FilterObj[3])
                        allowed[w] = false;
                    if (request.FilterObj[4] != null && request.FilterObj[4].Length > 0 && property.PropertyStatus != request.FilterObj[4])
                        allowed[w] = false;
                    w++;
                }
            }

            w = 0;
            foreach (var property in properties.ToList())
            {
                if (allowed[w])
                {
                    var propertyForUi = _mapper.Map<Properties, PropertyForList>(property);
                    propertyForUi.CampaignStatus = propertyForUi.CampaignStatus ?? "Not Assigned";
                    propertyForList.Add(propertyForUi);
                }
                w++;
            }

            return propertyForList;
        }
    }
}
