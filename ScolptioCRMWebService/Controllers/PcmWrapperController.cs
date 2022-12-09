using Commands;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using ScolptioCRMWebApi.ApplicationContext;
using System.Threading.Tasks;
using System.Linq;

namespace ScolptioCRMWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PcmWrapperController : BaseController
    {
        private readonly IMediator _mediator;
        public PcmWrapperController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        //[Authorize]
        [HttpPost("[action]")]
        public async Task<ActionResult<string>> Campaign([FromBody] CampaignMailerCommand command = null)
        {
            if (!SecurityContext.claims.Claims.Any() || SecurityContext.OrgName != "SYSTEM" || !SecurityContext.Permission.Where(x =>x == "edit_campaigns").Any())
                return Forbid();
            if (command == null)
                command = new CampaignMailerCommand();
            return await _mediator.Send(command);
        }
    }
}
