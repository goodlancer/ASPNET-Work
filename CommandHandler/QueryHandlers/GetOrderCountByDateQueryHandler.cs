using Commands.Query;
using Domains.PostMania;
using MediatR;
using ScolptioCRMCoreService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    public class GetOrderCountByDateQueryHandler : IRequestHandler<GetOrderCountByDateQuery, object>
    {
        private readonly PostCardManiaService _postCardManiaService;
        public GetOrderCountByDateQueryHandler(
         PostCardManiaService postCardManiaService
        )
        {
            _postCardManiaService = postCardManiaService;
        }

        public async Task<object> Handle(GetOrderCountByDateQuery request, CancellationToken cancellationToken)
        {
            var getAllOrders = await _postCardManiaService.GetAllOrdersAsync();

            var filterOrders = getAllOrders.GroupBy(m => m.OrderDate.Date).Select(m => new { date = m.Key, count = m.Count() }).AsQueryable();
            if (request.StartDate != null && request.EndDate != null)
            {
                filterOrders = getAllOrders.Where(f => f.OrderDate >= ((DateTime?)request.StartDate).Value.Date && f.OrderDate < ((DateTime?)request.EndDate).Value.Date).GroupBy(m => m.OrderDate.Date).Select(m => new { date = m.Key, count = m.Count() }).AsQueryable();
            }
            var filterdOrders = filterOrders.ToList();
            var getAllDates = filterdOrders.Select(m => m.date.ToShortDateString()).ToArray();
            var countsArray = filterdOrders.Select(m => m.count).ToArray();
            return new { datesArray = getAllDates, countsArray = countsArray };
        }
    }
}
