using Commands.Query;
using Domains.DBModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ScolptioCRMWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : BaseController
    {
        private readonly IMediator _mediator;
        public DashboardController(IMediator _mediator)
        {
            this._mediator = _mediator;
        }


        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> GetTotalCount([FromBody] GetCountByDateQuery GetCountByDateQuery)
        {
            var tempStartDate = GetCountByDateQuery.StartDateString!=string.Empty?
                DateTime.ParseExact(GetCountByDateQuery.StartDateString, "MM/dd/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
           // tempStartDate = DateTime.SpecifyKind(tempStartDate, DateTimeKind.Utc);

            var tempEndDate = GetCountByDateQuery.StartDateString != string.Empty ? DateTime.ParseExact(GetCountByDateQuery.EndDateString, "MM/dd/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
            //tempEndDate = DateTime.SpecifyKind(tempEndDate, DateTimeKind.Utc);

            var getCountQuery = new GetCountByDateQuery()
            {
                OrganizationId = SecurityContext.OrgId,
                EntityName = typeof(Properties),
                StartDate = tempStartDate,
                EndDate = tempEndDate,
                SectionName = GetCountByDateQuery.SectionName
            };
            var result = await _mediator.Send(getCountQuery);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> GetAllOrders([FromBody] GetOrderCountByDateQuery GetCountByDateQuery)
        {
            var tempStartDate = GetCountByDateQuery.StartDateString != string.Empty ?
               DateTime.ParseExact(GetCountByDateQuery.StartDateString, "MM/dd/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
            // tempStartDate = DateTime.SpecifyKind(tempStartDate, DateTimeKind.Utc);

            var tempEndDate = GetCountByDateQuery.StartDateString != string.Empty ? DateTime.ParseExact(GetCountByDateQuery.EndDateString, "MM/dd/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
            //tempEndDate = DateTime.SpecifyKind(tempEndDate, DateTimeKind.Utc);

            var getCountQuery = new GetOrderCountByDateQuery()
            {
                OrganizationId = SecurityContext.OrgId,
                EntityName = typeof(Properties),
                StartDate = tempStartDate,
                EndDate = tempEndDate,
                SectionName = GetCountByDateQuery.SectionName
            };
            var result = await _mediator.Send(getCountQuery);
            return Ok(result);
        }


    }
}
