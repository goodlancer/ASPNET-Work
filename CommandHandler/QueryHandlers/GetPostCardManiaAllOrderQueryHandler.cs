using Commands.Query;

using Domains.PostMania;

using MediatR;

using ScolptioCRMCoreService.Services;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    public class GetPostCardManiaAllOrderQueryHandler : IRequestHandler<GetPostCardManiaAllOrderQuery, List<Order>>
    {
        private readonly PostCardManiaService _postCardManiaService;
        public GetPostCardManiaAllOrderQueryHandler(PostCardManiaService postCardManiaService)
        {
            _postCardManiaService = postCardManiaService;
        }
        public async Task<List<Order>> Handle(GetPostCardManiaAllOrderQuery request, CancellationToken cancellationToken)
        {
            var orderList = new List<Order>();
            if (request.OrderId == 0)
            {
                orderList = await _postCardManiaService.GetAllOrdersAsync();
            }
            else
            {
                var order = await _postCardManiaService.GetOrderByIdAsync(request.OrderId);
                orderList.Add(order);
            }

            return orderList;
        }
    }
}
