using AutoMapper;

using Commands.Query;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    public class GetPropertiesQueryHandler : IRequestHandler<GetPropertiesQuery, Properties>
    {

        private readonly IBaseRepository<Properties> _baseRepositoryProperties;
        private readonly IMapper _mapper;


        public GetPropertiesQueryHandler(IMapper mapper
             , IBaseRepository<Properties> baseRepositoryProperties
           )
        {
            _mapper = mapper;
            _baseRepositoryProperties = baseRepositoryProperties;
        }

        public async Task<Properties> Handle(GetPropertiesQuery request, CancellationToken cancellationToken)
        {
            var property = await _baseRepositoryProperties.GetSingleAsync(x => x.Id == request.PropertiesId);
            return property;
        }

    }
}
