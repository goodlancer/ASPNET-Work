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
    public class GetMailHousePricingQueryHandler : IRequestHandler<GetMailHousePricingQuery, List<MailHousePricing>>
    {
        private readonly IBaseRepository<MailHousePricing> _baseRepositoryProperties;
        private readonly IMapper _mapper;

        public GetMailHousePricingQueryHandler(IMapper mapper
             , IBaseRepository<MailHousePricing> baseRepositoryProperties
           )
        {
            _mapper = mapper;
            _baseRepositoryProperties = baseRepositoryProperties;
        }

        public async Task<List<MailHousePricing>> Handle(GetMailHousePricingQuery request, CancellationToken cancellationToken)
        {
            var property = await _baseRepositoryProperties.GetAsync();
            return (List<MailHousePricing>)property;
        }

    }
}
