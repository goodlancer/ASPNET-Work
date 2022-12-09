
using Domains.PostMania;

using MediatR;

using System.Collections.Generic;

namespace Commands.Query
{
    public class GetPostCardManiaAllOrderQuery : IRequest<List<Order>>
    {
        public int OrderId { get; set; }
    }
}
