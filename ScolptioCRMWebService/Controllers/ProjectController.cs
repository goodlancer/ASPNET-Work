using Commands;
using Commands.Query;
using Domains.DBModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScolptioCRMWebApi.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public class ProjectController : BaseController
    {
        private readonly IMediator _mediator;
        public ProjectController(IMediator _mediator)
        {
            this._mediator = _mediator;
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> GetAll([FromBody] GetAllProjectQuery GetAllProjectQuery)
        {
            GetAllProjectQuery.OrgId = SecurityContext.OrgId;
            GetAllProjectQuery.UserId = SecurityContext.UserId;
            GetAllProjectQuery.RoleIds = SecurityContext.Roles;
            var result = await _mediator.Send(GetAllProjectQuery);
            return Ok(result);
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult> GetTotalCount()
        {
            var getCountQuery = new GetCountQuery()
            {
                OrganizationId = SecurityContext.OrgId,
                EntityName = typeof(Projects)
            };
            var result = await _mediator.Send(getCountQuery);
            return Ok(result);
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult> GetById(string projectId)
        {
            var getprojectQuery = new GetProjectQuery
            {
                OrgId = SecurityContext.OrgId,
                Id = projectId
            };

            var result = await _mediator.Send(getprojectQuery);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> Add([FromBody] CreateProjectsCommand CreateProjectsCommand)
        {
            CreateProjectsCommand.OrgId = SecurityContext.OrgId;
            CreateProjectsCommand.CreatedDate = DateTime.Now;
            await _mediator.Send(CreateProjectsCommand);
            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> Update([FromBody] UpdateProjectCommand UpdateProjectCommand)
        {
            await _mediator.Send(UpdateProjectCommand);
            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> Delete([FromBody] DeleteProjectsCommand DeleteProjectsCommand)
        {
            await _mediator.Send(DeleteProjectsCommand);
            return Ok();
        }
        [HttpPost("[action]")]
        [Authorize]
        public async Task<IActionResult> UpdateProjectResource([FromBody] ProjectResourceUpdateCommand resourceUpdateCommand)
        {
            await _mediator.Send(resourceUpdateCommand);
            return Ok();
        }
        [HttpPost("[action]")]
        [Authorize]
        public async Task<IActionResult> GetDocuments([FromBody] GetProjectDocumentsQuery getDocumentsQuery)
        {
            var documents = await _mediator.Send(getDocumentsQuery);
            return Ok(documents);
        }
        [HttpDelete("[action]")]
        [Authorize]
        public async Task<IActionResult> Document([FromQuery] DeleteDocumentCommand deleteDocumentCommand)
        {
            var documents = await _mediator.Send(deleteDocumentCommand);
            return Ok(documents);
        }
        [HttpGet("[action]")]
        [Authorize]
        public async Task<IActionResult> GetS3ObjectUrl([FromQuery] GetS3ObjectUrlQuery s3ObjectUrlQuery)
        {
            var url = await _mediator.Send(s3ObjectUrlQuery);
            return Ok(url);
        }
        [HttpGet("[action]")]
        [Authorize]
        public async Task<IActionResult> GetTagsByPageNumber([FromQuery] GetTagsByPageNumberQuery getTagsByPageNumberQuery)
        {
            getTagsByPageNumberQuery.OrgId = SecurityContext.OrgId;
            var tagsForUi = await _mediator.Send(getTagsByPageNumberQuery);
            return Ok(tagsForUi);
        }
        [HttpPost("[action]")]
        [Authorize]
        public async Task<IActionResult> Tag([FromBody] AddTagCommand addTagCommand)
        {
            addTagCommand.OrgId = SecurityContext.OrgId;
            var newTagId = await _mediator.Send(addTagCommand);
            if (string.IsNullOrEmpty(newTagId))
                return Conflict();
            return Ok(newTagId);
        }
        [HttpPut("[action]")]
        [Authorize]
        public async Task<IActionResult> Tag([FromBody] EditTagCommand editTagCommand)
        {
            editTagCommand.OrgId = SecurityContext.OrgId;
            var result = await _mediator.Send(editTagCommand);
            if (result)
                return Ok();
            else
                return StatusCode(500);
        }
        [HttpDelete("[action]")]
        [Authorize]
        public async Task<IActionResult> Tag([FromQuery] DeleteTagCommand deleteTagCommand)
        {
            deleteTagCommand.OrgId = SecurityContext.OrgId;
            var result = await _mediator.Send(deleteTagCommand);
            if (result)
                return Ok();
            else
                return Forbid();
        }
        [HttpPut("[action]")]
        [Authorize]
        public async Task<IActionResult> UpdateDocumentTags([FromBody] UpdateDocumentTagsCommand updateDocumentTagsCommand)
        {
            var result = await _mediator.Send(updateDocumentTagsCommand);
            if (result)
                return Ok();
            else
                return StatusCode(500);
        }
        [HttpGet("[action]")]
        [Authorize]
        public async Task<IActionResult> PrepareQuestionnaire([FromQuery] PrepareQuestionnaireQuery prepareQuestionnaireQuery)
        {
            var result = await _mediator.Send(prepareQuestionnaireQuery);
            return Ok(result);
        }
        [HttpPost("[action]")]
        [Authorize]
        public async Task<IActionResult> Room([FromBody] SaveRoomCommand saveRoomCommand)
        {
            var result = await _mediator.Send(saveRoomCommand);
            return Ok(result);
        }
        [HttpDelete("[action]")]
        [Authorize]
        public async Task<IActionResult> Room([FromQuery] DeleteRoomCommand deleteRoomCommand)
        {
            var result = await _mediator.Send(deleteRoomCommand);
            return Ok(result);
        }
        [HttpPost("[action]")]
        [Authorize]
        public async Task<IActionResult> UploadToS3([FromForm]AddToS3Command addToS3Command)
        {
            var result = await _mediator.Send(addToS3Command);
            return Ok(result);
        }
        [HttpGet("[action]")]
        [Authorize]
        public async Task<IActionResult> RoomList([FromQuery] RoomListQuery roomListQuery)
        {
            var result = await _mediator.Send(roomListQuery);
            return Ok(result);
        }
        [HttpGet("[action]")]
        [Authorize]
        public async Task<IActionResult> RoomDetailsById([FromQuery] RoomDetailsByIdQuery roomDetailsById)
        {
            var result = await _mediator.Send(roomDetailsById);
            return Ok(result);
        }
        [HttpPut("[action]")]
        [Authorize]
        public async Task<IActionResult> SaveQuestion([FromBody] SaveQuestionCommand saveQuestionCommand)
        {
            var result = await _mediator.Send(saveQuestionCommand);
            return Ok(result);
        }
    }
}
