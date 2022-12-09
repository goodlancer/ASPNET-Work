
using Commands;
using Commands.Query;

using Domains.DBModels;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;

namespace ScolptioCRMWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncomeController : BaseController
    {
        private readonly IMediator _mediator;
        public IncomeController(IMediator _mediator)
        {
            this._mediator = _mediator;
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> GetAll([FromBody] GetAllIncomeQuery getAllIncomeQuery)
        {
            getAllIncomeQuery.OrgId = SecurityContext.OrgId;
            var result = await _mediator.Send(getAllIncomeQuery);
            return Ok(result);
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult> GetTotalCount()
        {
            var getCountQuery = new GetCountQuery()
            {
                OrganizationId = SecurityContext.OrgId,
                EntityName = typeof(Income)
            };
            var result = await _mediator.Send(getCountQuery);
            return Ok(result);
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult> GetById(string incomeId)
        {
            var getIncomeQuery = new GetIncomeQuery
            {
                OrgId = SecurityContext.OrgId,
                IncomeId = incomeId
            };

            var result = await _mediator.Send(getIncomeQuery);
            return Ok(result);
        }
        
        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> Add([FromBody] CreateIncomeCommand createIncomeCommand)
        {
            createIncomeCommand.OrgId = SecurityContext.OrgId;
            createIncomeCommand.CreatedDate = DateTime.Now;
            await _mediator.Send(createIncomeCommand);
            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> Update([FromBody] UpdateIncomeCommand updateIncomeCommand)
        {
            await _mediator.Send(updateIncomeCommand);
            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> Delete([FromBody] DeleteIncomeCommand deleteListingCommand)
        {
            await _mediator.Send(deleteListingCommand);
            return Ok();
        }

    }
}
