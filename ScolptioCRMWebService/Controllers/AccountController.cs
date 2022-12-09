
using Commands;
using Commands.Query;

using Domains.DBModels;
using Domains.Dtos;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ScolptioCRMWebApi.ApplicationContext;
using Services.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScolptioCRMWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IBaseRepository<User> _userBaseRepository;
        public AccountController(IMediator mediator, IBaseRepository<User> userBaseRepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _userBaseRepository = userBaseRepository;
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> SaveUser([FromBody] CreateUserCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> UpdateUserInformation([FromBody] UserUpdateCommand command)
        {
            command.Id = SecurityContext.UserId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult<UserForUi>> GetUserInformation()
        {
            var getUserQuery = new GetUserQuery { OrgId = SecurityContext.OrgId, UserId = SecurityContext.UserId };
            var response = await _mediator.Send(getUserQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult<UserForUi>> GetUserInformationByUserId(string userId)
        {
            var getUserQuery = new GetUserQuery { UserId = userId };
            var response = await _mediator.Send(getUserQuery);
            return Ok(response);
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult<List<UserForUi>>> GetUsersInformationByOrg([FromBody] GetAllUserByOrgQuery getUserQuery)
        {
            getUserQuery.OrgId = SecurityContext.OrgId;
            getUserQuery.LoogedInUserId = SecurityContext.UserId;
            getUserQuery.RoleIds = SecurityContext.Roles;
            var response = await _mediator.Send(getUserQuery);
            return Ok(response);
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult<Organization>> GetUserOrganization([FromBody] GetUserSpecificOrgQuery getUserQuery)
        {
            getUserQuery.UserId = SecurityContext.UserId;
            var response = await _mediator.Send(getUserQuery);
            return Ok(response);
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult> GetUserOrganizationTotalCount()
        {
            var getCountQuery = new GetCountQuery()
            {
                OrganizationId = SecurityContext.OrgId,
                UserId = SecurityContext.UserId,
                EntityName = typeof(Organization)
            };
            var result = await _mediator.Send(getCountQuery);
            return Ok(result);
        }


        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult> GetUserTotalCount()
        {
            var getCountQuery = new GetCountQuery()
            {
                OrganizationId = SecurityContext.OrgId,
                LoogedInUserId = SecurityContext.UserId,
                Roles = SecurityContext.Roles,
                EntityName = typeof(User)
            };
            var result = await _mediator.Send(getCountQuery);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult<UserForUi>> TokenExchange([FromBody] ExchangeTokenCommand exchangeTokenCommand)
        {
            exchangeTokenCommand.UserName = SecurityContext.UserName;
            var response = await _mediator.Send(exchangeTokenCommand);
            return Ok(response);
        }
        /*
        [HttpPut("[action]")]
        [Authorize]
        public ActionResult UpdateUserRole([FromBody] UpdateUserRoleCommand command)
        {
            _mediator.Send(command);
            return Ok();
        }
        */

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult<UserForUi>> RemoveUserFromOrganization([FromBody] RemoveUserCommand removeUserCommand)
        {
            removeUserCommand.OrgId = SecurityContext.OrgId;
            if (removeUserCommand.UserId != SecurityContext.UserId)
            {
                await _mediator.Send(removeUserCommand);
            }

            return Ok();
        }



        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> AcceptInvitation([FromBody] AcceptInvitationCommand command)
        {
            command.UserName = SecurityContext.UserName;
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> InviteUserAsync([FromBody] SendInvitationCommand command)
        {
            var user = await _userBaseRepository.GetByIdAsync(SecurityContext.UserId);

            command.TeamId = user.TeamId;
            command.Address = user.CompanyAdress;
            command.Website = user.Website;
            command.CompanyAdress = user.CompanyAdress;
            command.Logo = user.Logo;
            command.CompanyName = user.CompanyName;
            command.TeamId = user.TeamId;
            command.IsInvited = true;
            command.UserType = user.UserType;

            command.UserId = SecurityContext.UserId;
            command.UserDisplayName = SecurityContext.DisplayName;
            command.OrgId = SecurityContext.OrgId;
            command.OrgName = SecurityContext.OrgName;
            command.InvitedBy = SecurityContext.UserId;
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> ResetPasswordAsync([FromBody] ChangePasswordCommand changePasswordCommand)
        {
            changePasswordCommand.Email = SecurityContext.Email;
            var result = await _mediator.Send(changePasswordCommand);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> GetUserByRole([FromBody] GetUserByRoleCommand role)
        {
            role.OrgId = SecurityContext.OrgId;
            var result = await _mediator.Send(role);
            return Ok(result);
        }
    }
}
