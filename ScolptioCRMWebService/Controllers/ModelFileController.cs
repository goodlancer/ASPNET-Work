
using Amazon.Runtime;
using Amazon.S3;
using Commands;
using Commands.Query;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Services.Repository;
using System.IO;
using System.Threading.Tasks;

namespace ScolptioCRMWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModelFileController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly AwsSettings awsSettings;
        public ModelFileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> GetFiles([FromBody] GetModelFilesQuery modelFilesQuery)
        {

            var result = await _mediator.Send(modelFilesQuery);
            return Ok(result);
        }


        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> UploadFile([FromBody] UploadModelFilesCommand uploadModelFileCommand)
        {
            var result = await _mediator.Send(uploadModelFileCommand);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> DownloadFile([FromBody] GetModelFileByIdQuery getModelFileByIdQuery)
        {
            var result = await _mediator.Send(getModelFileByIdQuery);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> Delete([FromBody] DeleteModelFileCommand deleteModelFileCommand)
        {
            var result = await _mediator.Send(deleteModelFileCommand);
            return Ok(result);
        }

    }
}
