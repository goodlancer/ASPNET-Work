
using Commands;
using Commands.Query;
using Domains.DBModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace ScolptioCRMWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesWebsiteController : BaseController
    {
        private readonly IMediator _mediator;
        public SalesWebsiteController(IMediator _mediator)
        {
            this._mediator = _mediator;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> GetAll(GetAllSalesWebsiteQuery query)
        {
            query.OrgId = SecurityContext.OrgId;
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetById(string saleswebsiteId)
        {
            var getSalesWebsiteQuery = new GetSalesWebsiteQuery
            {
                SaleswebsiteId = saleswebsiteId ?? "1"
            };

            var result = await _mediator.Send(getSalesWebsiteQuery);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Add([FromBody] CreateSalesWebsiteCommand createSalesWebsiteCommand)
        {/*
            createListingCommand.OrganizationId = SecurityContext.OrgId;*/
            var result = await _mediator.Send(createSalesWebsiteCommand);
            return Ok(new JsonResult(result));
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Update([FromBody] UpdateSalesWebsiteCommand updateSalesWebsiteCommand)
        {
            await _mediator.Send(updateSalesWebsiteCommand);
            return Ok();
        }


        [HttpPost("[action]")]
        public async Task<ActionResult> Delete([FromBody] DeleteSalesWebsiteCommand deleteSalesWebsiteCommand)
        {
            await _mediator.Send(deleteSalesWebsiteCommand);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> UploadImage([FromBody] UploadImageCommand uploadImageCommand)
        {
            await _mediator.Send(uploadImageCommand);
            return Ok();
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult> GetTotalCount()
        {
            var getCountQuery = new GetCountQuery()
            {
                OrganizationId = SecurityContext.OrgId,
                EntityName = typeof(SalesWebsite)
            };
            var result = await _mediator.Send(getCountQuery);
            return Ok(result);
        }

    }
}
