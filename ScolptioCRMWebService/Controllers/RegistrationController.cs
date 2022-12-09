
using Commands;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace ScolptioCRMWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RegistrationController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> RegisterUserAsync([FromBody] CreateUserCommand command)
        {
            var isSuccess = await _mediator.Send(command);
            return Ok(isSuccess);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> UserExist([FromBody] UserExistQuery command)
        {
            bool result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> VerifyEmail([FromBody] VerifyCodeAndEmail command)
        {
            bool result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
        {
            bool result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            bool result = await _mediator.Send(command);
            return Ok(result);
        }



    }
}
