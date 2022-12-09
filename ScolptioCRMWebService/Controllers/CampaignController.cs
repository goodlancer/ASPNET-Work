using MediatR;
using Commands;
using Commands.Query;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ScolptioCRMWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampaignController : BaseController
    {
        private readonly IMediator _mediator;
        public CampaignController(IMediator _mediator)
        {
            this._mediator = _mediator;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetAll(string orgId)
        {
            var getCampaignQuery = new GetCampaignQuery
            {
                OrgId = orgId
            };

            var result = await _mediator.Send(getCampaignQuery);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetById(string campaignId)
        {
            var getCampaignByIdQuery = new GetCampaignByIdQuery
            {
                CampaignId = campaignId
            };

            var result = await _mediator.Send(getCampaignByIdQuery);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Add([FromBody] CreateCampaignCommand createCampaignCommand)
        {
            createCampaignCommand.UserId = SecurityContext.UserId;
            var id =  await _mediator.Send(createCampaignCommand);
            return Ok(id);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Update([FromBody] UpdateCampaignCommand updateCampaignCommand)
        {
            updateCampaignCommand.UserId = SecurityContext.UserId;
            await _mediator.Send(updateCampaignCommand);
            return Ok();
        }


        [HttpPost("[action]")]
        public async Task<ActionResult> Delete([FromBody] DeleteCampaignCommand deleteCampaignCommand)
        {
            await _mediator.Send(deleteCampaignCommand);
            return Ok();
        }
    }
}
