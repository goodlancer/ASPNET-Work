
using Commands;
using Commands.Query;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace ScolptioCRMWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MailhouseController : BaseController
    {
        private readonly IMediator _mediator;
        public MailhouseController(IMediator _mediator)
        {
            this._mediator = _mediator;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetAll(string orgId)
        {
            var getAllMailhouseQuery = new GetAllMailhouseQuery
            {
                OrgId = orgId
            };

            var result = await _mediator.Send(getAllMailhouseQuery);
            return Ok(result);
        }

        //[HttpGet("[action]")]
        //public async Task<ActionResult> GetAllNoOrg()
        //{
        //    var getAllMailhouseQuery = new GetAllMailhouseQuery
        //    {
        //        OrgId = null
        //    };

        //    var result = await _mediator.Send(getAllMailhouseQuery);
        //    return Ok(result);
        //}

        [HttpGet("[action]")]
        public async Task<ActionResult> GetPricing()
        {
            var getMailHousePricingQuery = new GetMailHousePricingQuery();
            var result = await _mediator.Send(getMailHousePricingQuery);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetById(string mailhouseId)
        {
            var getMailhouseQuery = new GetMailhouseQuery
            {
                MailhouseId = mailhouseId ?? "1"
            };

            var result = await _mediator.Send(getMailhouseQuery);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Add([FromBody] CreateMailhouseCommand createMailhouseCommand)
        {/*
            createListingCommand.OrganizationId = SecurityContext.OrgId;*/
            await _mediator.Send(createMailhouseCommand);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Update([FromBody] UpdateMailhouseCommand updateMailhouseCommand)
        {
            await _mediator.Send(updateMailhouseCommand);
            return Ok();
        }


        [HttpPost("[action]")]
        public async Task<ActionResult> Delete([FromBody] DeleteMailhouseCommand deleteMailhouseCommand)
        {
            await _mediator.Send(deleteMailhouseCommand);
            return Ok();
        }

    }
}
