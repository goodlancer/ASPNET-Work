using Commands.Query;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;

namespace ScolptioCRMWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostCardController : BaseController
    {

        private readonly IMediator _mediator;

        public PostCardController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetAllOrders()
        {
            var result = await _mediator.Send(new GetPostCardManiaAllOrderQuery { OrderId = 0 });
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetOrderDetailsById(int id)
        {
            var result = await _mediator.Send(new GetPostCardManiaAllOrderQuery { OrderId = id });
            return Ok(result);
        }




    }
}
