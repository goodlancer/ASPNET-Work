
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
    public class TeamController : BaseController
    {
        private readonly IMediator _mediator;
        public TeamController(IMediator _mediator)
        {
            this._mediator = _mediator;
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> GetAll([FromBody] GetAllTeamQuery getAllTeamQuery)
        {
            getAllTeamQuery.OrgId = SecurityContext.OrgId;
            var result = await _mediator.Send(getAllTeamQuery);
            return Ok(result);
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult> GetTotalCount()
        {
            var getCountQuery = new GetCountQuery()
            {
                OrganizationId = SecurityContext.OrgId,
                EntityName = typeof(Team)
            };
            var result = await _mediator.Send(getCountQuery);
            return Ok(result);
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult> GetById(string teamId)
        {
            var getTeamQuery = new GetTeamQuery
            {
                OrgId = SecurityContext.OrgId,
                TeamId = teamId
            };

            var result = await _mediator.Send(getTeamQuery);
            return Ok(result);
        }



        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> Add([FromBody] CreateTeamCommand createTeamCommand)
        {
            createTeamCommand.OrganizationId = SecurityContext.OrgId;
            createTeamCommand.CreatedBy = SecurityContext.UserId;
            createTeamCommand.CreatedDate = DateTime.UtcNow;
            await _mediator.Send(createTeamCommand);
            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> Update([FromBody] UpdateTeamCommand updateTeamCommand)
        {
            if (updateTeamCommand.OrganizationId == SecurityContext.OrgId)
            {
                await _mediator.Send(updateTeamCommand);
            }

            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> Delete([FromBody] DeleteTeamCommand deleteListingCommand)
        {
            await _mediator.Send(deleteListingCommand);
            return Ok();
        }

    }
}
