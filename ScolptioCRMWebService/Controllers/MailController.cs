
using MediatR;

using Microsoft.AspNetCore.Mvc;

using ScolptioCRMCoreService.IManagers;

using System.Threading.Tasks;

namespace ScolptioCRMWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MailController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMailManager mailManager;

        public MailController(IMediator mediator, IMailManager mailManager)
        {
            this._mediator = mediator;
            this.mailManager = mailManager;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> SendAsync()
        {
            await mailManager.SendEmail(new string[] { "" }, new string[] { "" }, new string[] { "" }, "", "");
            return Ok();
        }



    }
}
