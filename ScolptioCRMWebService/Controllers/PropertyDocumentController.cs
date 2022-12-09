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
    public class PropertyDocumentController : BaseController
    {
        private readonly IMediator _mediator;
        public PropertyDocumentController(IMediator _mediator)
        {
            this._mediator = _mediator;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetAll(string orgId)
        {
            var getPropertyDocumentQuery = new GetPropertyDocumentQuery
            {
                OrgId = orgId
            };

            var result = await _mediator.Send(getPropertyDocumentQuery);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetById(string documentId)
        {
            var getPropertyDocumentByIdQuery = new GetPropertyDocumentByIdQuery
            {
                DocumentId = documentId
            };

            var result = await _mediator.Send(getPropertyDocumentByIdQuery);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Add([FromBody] CreatePropertyDocumentCommand createPropertyDocumentCommand)
        {
            createPropertyDocumentCommand.UserId = SecurityContext.UserId;
            var id = await _mediator.Send(createPropertyDocumentCommand);
            return Ok(id);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Update([FromBody] UpdatePropertyDocumentCommand updatePropertyDocumentCommand)
        {
            await _mediator.Send(updatePropertyDocumentCommand);
            return Ok();
        }


        [HttpPost("[action]")]
        public async Task<ActionResult> Delete([FromBody] DeletePropertyDocumentCommand deletePropertyDocumentCommand)
        {
            await _mediator.Send(deletePropertyDocumentCommand);
            return Ok();
        }
    }
}
