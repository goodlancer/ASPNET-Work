using AutoMapper;
using Commands.Query;
using Domains.DBModels;
using MediatR;
using Services.Repository;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    public class GetPropertyStatusQueryHandler : IRequestHandler<GetPropertyStatusQuery, List<PropertyStatus>>
    {

        private readonly IBaseRepository<PropertyStatus> _baseRepositoryProperties;
        private readonly IMapper _mapper;

        public GetPropertyStatusQueryHandler(IMapper mapper
             , IBaseRepository<PropertyStatus> baseRepositoryProperties
           )
        {
            _mapper = mapper;
            _baseRepositoryProperties = baseRepositoryProperties;
        }

        public async Task<List<PropertyStatus>> Handle(GetPropertyStatusQuery request, CancellationToken cancellationToken)
        {
            var property = await _baseRepositoryProperties.GetAsync();
            return (List<PropertyStatus>)property;
        }
    }
}
