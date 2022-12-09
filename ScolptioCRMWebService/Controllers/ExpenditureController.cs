
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
    public class ExpenditureController : BaseController
    {
        private readonly IMediator _mediator;
        public ExpenditureController(IMediator _mediator)
        {
            this._mediator = _mediator;
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> GetAll([FromBody] GetAllExpenditureQuery getAllExpenditureQuery)
        {
            getAllExpenditureQuery.OrgId = SecurityContext.OrgId;
            var result = await _mediator.Send(getAllExpenditureQuery);
            return Ok(result);
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult> GetTotalCount()
        {
            var getCountQuery = new GetCountQuery()
            {
                OrganizationId = SecurityContext.OrgId,
                EntityName = typeof(Expenditure)
            };
            var result = await _mediator.Send(getCountQuery);
            return Ok(result);
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult> GetById(string expenditureId)
        {
            var getExpenditureQuery = new GetExpenditureQuery
            {
                OrgId = SecurityContext.OrgId,
                ExpenditureId = expenditureId
            };

            var result = await _mediator.Send(getExpenditureQuery);
            return Ok(result);
        }



        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> Add([FromBody] CreateExpenditureCommand createExpenditureCommand)
        {
            createExpenditureCommand.OrgId = SecurityContext.OrgId;
            createExpenditureCommand.CreatedDate = DateTime.Now;
            await _mediator.Send(createExpenditureCommand);
            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> Update([FromBody] UpdateExpenditureCommand updateExpenditureCommand)
        {
            await _mediator.Send(updateExpenditureCommand);
            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> Delete([FromBody] DeleteExpenditureCommand deleteListingCommand)
        {
            await _mediator.Send(deleteListingCommand);
            return Ok();
        }

    }
}
