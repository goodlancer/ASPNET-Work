
using Commands;
using Commands.Query;

using Domains.DBModels;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MongoDB.Driver;

using Services.Repository;

using System.Threading.Tasks;

namespace ScolptioCRMWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListingController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMongoScolptioDBContext _mongoContext;
        private readonly IMongoCollection<Listing> _dbCollection;
        public ListingController(IMediator _mediator, IMongoScolptioDBContext context)
        {
            this._mediator = _mediator;
            //_mongoContext = context;


            //_dbCollection = _mongoContext.GetCollection<Listing>("Listing");
            //    //_dbCollection.UpdateMany<Listing>(x => true, Builders<Listing>.Update.Set(x => x.OrganizationId, "6bdc1f5c-482e-4ee9-95d8-24d4011025ce"));
            //    _dbCollection.DeleteManyAsync(x=>true);
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> GetAll([FromBody] GetAllListingQuery getAllListingQuery)
        {
            getAllListingQuery.OrgId = SecurityContext.OrgId;
            var result = await _mediator.Send(getAllListingQuery);
            return Ok(result);

            /* var getAllListingQuery = new GetAllListingQuery
            {
                OrgId = orgId
            };

            var result = await _mediator.Send(getAllListingQuery);
            return Ok(result); */
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult> GetTotalCount()
        {
            var getCountQuery = new GetCountQuery()
            {
                OrganizationId = SecurityContext.OrgId,
                EntityName = typeof(Listing)
            };
            var result = await _mediator.Send(getCountQuery);
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
        [Authorize]
        public async Task<ActionResult> Add([FromBody] CreateListingCommand createListingCommand)
        {
            createListingCommand.OrganizationId = SecurityContext.OrgId;
            createListingCommand.UserId = SecurityContext.UserId;
            var id = await _mediator.Send(createListingCommand);
            if(string.IsNullOrEmpty(id))
                return BadRequest("Please link this listing with corresponding property");
            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> Update([FromBody] UpdateListingCommand updateListingCommand)
        {
            updateListingCommand.UserId = SecurityContext.UserId;
            await _mediator.Send(updateListingCommand);
            return Ok();
        }


        [HttpPost("[action]")]
        public async Task<ActionResult> Delete([FromBody] DeleteListingCommand deleteListingCommand)
        {
            await _mediator.Send(deleteListingCommand);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> UpdatePropertiesResource([FromBody] ListingResourceUpdateCommand listingResourceUpdateCommand)
        {
            await _mediator.Send(listingResourceUpdateCommand);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> DeleteImage([FromBody] DeleteListingImageCommand DeleteListingImageCommand)
        {
            await _mediator.Send(DeleteListingImageCommand);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> MainImage([FromBody] MainImageCommand MainImageCommand)
        {
            await _mediator.Send(MainImageCommand);
            return Ok();
        }
        [HttpGet("[action]")]
        public async Task<ActionResult> ApnLookup([FromQuery] ApnLookupQuery request)
        {
            request.OrgId = SecurityContext.OrgId;
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
