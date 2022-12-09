using Commands;
using Commands.Query;

using Domains.DBModels;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ScolptioCRMWebApi.ApplicationContext;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ScolptioCRMWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateController : BaseController
    {
        private readonly IMediator _mediator;
        public TemplateController(IMediator mediator)
        {
            this._mediator = mediator;
        }


        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> GetAllTemplates([FromBody] GetAllTemplateQuery getAllTemplateQuery)
        {
            getAllTemplateQuery.OrganizationId = SecurityContext.OrgId;
            var result = await _mediator.Send(getAllTemplateQuery);
            return Ok(result);

        }


        //[HttpPost("[action]")]
        //[Authorize]
        //public async Task<ActionResult> GetAll([FromBody] GetAllPropertiesQuery getAllPropertiesQuery)
        //{
        //    getAllPropertiesQuery.OrgId = SecurityContext.OrgId;
        //    var result = await _mediator.Send(getAllPropertiesQuery);
        //    return Ok(result);
        //}

        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult> GetTotalCount()
        {
            var getCountQuery = new GetCountQuery()
            {
                OrganizationId = SecurityContext.OrgId,
                EntityName = typeof(DocumentTemplate)
            };
            var result = await _mediator.Send(getCountQuery);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> Transact([FromBody] CreateTemplateCommand createTemplateCommand)
        {
            try
            {
                createTemplateCommand.OrganizationId = SecurityContext.OrgId;
                createTemplateCommand.CreatedBy = SecurityContext.UserId;
                await _mediator.Send(createTemplateCommand);
                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut("[action]")]
        [Authorize]
        public async Task<ActionResult> Transact([FromBody] UpdateTemplateCommand updateTemplateCommand)
        {
            try
            {
                await _mediator.Send(updateTemplateCommand);
                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("[action]")]
        [Authorize]
        public async Task<ActionResult> Transact([FromQuery] DeleteTemplateCommand deleteTemplateCommand)
        {
            try
            {
                await _mediator.Send(deleteTemplateCommand);
                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
