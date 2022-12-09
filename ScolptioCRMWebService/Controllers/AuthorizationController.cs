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
    public class AuthorizationController : BaseController
    {
        private readonly IMediator _mediator;
        public AuthorizationController(IMediator _mediator)
        {
            this._mediator = _mediator;
        }


        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult> GetRoleTotalCount()
        {
            var getCountQuery = new GetCountQuery()
            {
                OrganizationId = SecurityContext.OrgId,
                EntityName = typeof(Role)
            };
            var result = await _mediator.Send(getCountQuery);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> GetRole(GetRoleQuery query)
        {
            query.OrgId = SecurityContext.OrgId;
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> CreateRoleAsync([FromBody] CreateRoleCommand createRoleCommand)
        {
            createRoleCommand.OrgId = SecurityContext.OrgId;
            await _mediator.Send(createRoleCommand);
            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> UpdateRoleAsync([FromBody] UpdateRoleCommand updateRoleCommand)
        {
            updateRoleCommand.OrganizationId = SecurityContext.OrgId;
            await _mediator.Send(updateRoleCommand);
            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> DeleteRoleAsync([FromBody] DeleteRoleCommand updateRoleCommand)
        {
            bool result = await _mediator.Send(updateRoleCommand);
            return Ok(result);
        }


        [HttpPost("[action]")]
        public async Task<ActionResult> AssignUserRoleAsync([FromBody] AssignRoleCommand command)
        {
            command.OrgId = SecurityContext.OrgId;
            await _mediator.Send(command);
            return Ok();
        }

    }
}
