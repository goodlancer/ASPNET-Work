
using Commands;
using Commands.Query;

using Domains.DBModels;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace ScolptioCRMWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertiesController : BaseController
    {
        private readonly IMediator _mediator;
        public PropertiesController(IMediator _mediator)
        {
            this._mediator = _mediator;
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult> GetTotalCount()
        {
            var getCountQuery = new GetCountQuery()
            {
                OrganizationId = SecurityContext.OrgId,
                EntityName = typeof(Properties)
            };
            var result = await _mediator.Send(getCountQuery);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> GetAll([FromBody] GetAllPropertiesQuery getAllPropertiesQuery)
        {
            getAllPropertiesQuery.OrgId = SecurityContext.OrgId;
            var result = await _mediator.Send(getAllPropertiesQuery);
            return Ok(result);
        }


        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult> GetProperties(string propertiesId)
        {
            var getPropertiesQuery = new GetPropertiesQuery
            {
                OrgId = SecurityContext.OrgId,
                PropertiesId = propertiesId
            };
            var result = await _mediator.Send(getPropertiesQuery);
            return Ok(result);
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult> GetPropertyStatus()
        {
            var getPropertiesStatusQuery = new GetPropertyStatusQuery();
            var result = await _mediator.Send(getPropertiesStatusQuery);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<IActionResult> ImportFile([FromBody] ImportFileCommand importFileCommand)
        {
            importFileCommand.UserId = SecurityContext.UserId;
            importFileCommand.UserName = SecurityContext.UserName;
            importFileCommand.OrgId = SecurityContext.OrgId;
            var result = await _mediator.Send(importFileCommand);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<IActionResult> UpdatePropertiesResource([FromBody] PropertiesResourceUpdateCommand propertiesResourceUpdate)
        {
            propertiesResourceUpdate.OrgId = SecurityContext.OrgId;
            propertiesResourceUpdate.UserId = SecurityContext.UserId;
            var result = await _mediator.Send(propertiesResourceUpdate);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<IActionResult> UpdatePropertiesStatus([FromBody] PropertiesStatusUpdateCommand propertiesResourceUpdate)
        {
            propertiesResourceUpdate.OrgId = SecurityContext.OrgId;
            propertiesResourceUpdate.UserId = SecurityContext.UserId;
            var result = await _mediator.Send(propertiesResourceUpdate);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> Update([FromBody] PropertyUpdateCommand propertyUpdateCommand)
        {
            propertyUpdateCommand.UserId = SecurityContext.UserId;
            await _mediator.Send(propertyUpdateCommand);
            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<IActionResult> MapImportHeader([FromBody] MapPropertiesColumnCommand mapPropertiesColumnCommand)
        {
            var result = await _mediator.Send(mapPropertiesColumnCommand);
            return Ok(result);
        }


        [HttpPost("[action]")]
        [Authorize]
        public async Task<IActionResult> MapProperty([FromBody] CustomColumnMapperCommand customColumnMapper)
        {
            var result = await _mediator.Send(customColumnMapper);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<IActionResult> InitiateImport([FromBody] InitiateFileImportCommand initiateFileImport)
        {
            initiateFileImport.OrgId = SecurityContext.OrgId;
            var result = await _mediator.Send(initiateFileImport);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> MigrateData([FromBody] MigrateDataCommand initiateFileImport)
        {
            initiateFileImport.OrgId = SecurityContext.OrgId;
            var result = await _mediator.Send(initiateFileImport);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<IActionResult> OfferPrice([FromBody] SetOfferPricesCommand setOfferPriceCommand)
        {
            setOfferPriceCommand.UserId = SecurityContext.UserId;
            var result = await _mediator.Send(setOfferPriceCommand);
            return Ok(result);
        }
        [HttpPost("[action]")]
        [Authorize]
        public async Task<IActionResult> DeletePropertyBulk([FromBody] DeletePropertyBulkCommand deletePropertyBulkCommand)
        {
            var result = await _mediator.Send(deletePropertyBulkCommand);
            return Ok(result);
        }
        [HttpGet("[action]")]
        [Authorize]
        public async Task<IActionResult> Timeline([FromQuery] GetTimelineByPropertyIdQuery getTimelineByPropertyIdQuery)
        {
            var result = await _mediator.Send(getTimelineByPropertyIdQuery);
            return Ok(result);
        }
        [HttpPost("[action]")]
        [Authorize]
        public async Task<IActionResult> GetLinkedCampaignCount([FromBody] GetLinkedCampaignCountQuery getLinkedCampaignCountQuery)
        {
            var result = await _mediator.Send(getLinkedCampaignCountQuery);
            return Ok(result);
        }
        /* [HttpGet("[action]")]
         public async Task<ActionResult> GetAll(string orgId)
         {
             var getAllListingQuery = new GetAllListingQuery
             {
                 OrgId = orgId
             };

             var result = await _mediator.Send(getAllListingQuery);
             return Ok(result);
         }

         [HttpGet("[action]")]
         public async Task<ActionResult> GetById(string listingId)
         {
             var getListingQuery = new GetListingQuery
             {
                 ListingId = listingId ?? "1"
             };

             var result = await _mediator.Send(getListingQuery);
             return Ok(result);
         }

         [HttpPost("[action]")]
         public async Task<ActionResult> Add([FromBody] CreateListingCommand createListingCommand)
         {*//*
             createListingCommand.OrganizationId = SecurityContext.OrgId;*//*
             await _mediator.Send(createListingCommand);
             return Ok();
         }

         [HttpPost("[action]")]
         public async Task<ActionResult> Update([FromBody] UpdateListingCommand updateListingCommand)
         {
             await _mediator.Send(updateListingCommand);
             return Ok();
         }


         [HttpPost("[action]")]
         public async Task<ActionResult> Delete([FromBody] DeleteListingCommand deleteListingCommand)
         {
             await _mediator.Send(deleteListingCommand);
             return Ok();
         }*/

    }
}
