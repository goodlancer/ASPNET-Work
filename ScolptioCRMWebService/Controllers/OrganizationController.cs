
using Commands;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;

namespace ScolptioCRMWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationController : BaseController
    {

        private readonly IMediator _mediator;

        public OrganizationController(IMediator mediator
            )
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult> GetOrgDetail()
        {
            var result = await _mediator.Send(new GetOrgQuery { OrgId = SecurityContext.OrgId });
            return Ok(result);
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> CreateOrganizationAsync([FromBody] CreateNewOrgCommand command)
        {
            command.UserId = SecurityContext.UserId;
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> UpdateOrganizationAsync([FromBody] UpdateOrgCommand command)
        {
            command.UserId = SecurityContext.UserId;
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> DeleteOrganizationAsync([FromBody] DeleteOrgCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }




        /*

        [HttpPost("[action]")]
        public ActionResult CreateOrganization([FromBody] CreateNewUserWithOrgCommand command)
        {
            _mediator.Send(command);
            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<List<UserRoleMapping>> GetRoleDetailsWithOrgId(string orgId)
        {
            if (orgId != null)
                return await _userRoleMapping.GetAllAsync(x => x.OrganizationId == orgId);
            else return null;
        }

       
       

        [HttpGet("[action]")]
        public async Task<ActionResult> GetAllPermissionAsync()
        {
            List<Permission> permissions = await _permissionBaseRepository.GetAllAsync(x => x.IsActive == true);
            return Ok(permissions);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetPermissionByRoleIdAsync(string role)
        {
            List<RolePermissionMapping> permissions = await _rolePermissionMappingBaseRepository.GetAllAsync(x => x.RoleId == role);
            return Ok(permissions);
        }
        */
    }
}
