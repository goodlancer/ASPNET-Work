using Commands;
using Commands.Query;

using Domains.DBModels;
using Domains.Dtos;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ScolptioCRMWebApi.ApplicationContext;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScolptioCRMWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SettingsController : BaseController
    {
        private readonly IMediator _mediator;
        public SettingsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        [HttpGet("[action]")]
        [Authorize]
        public async Task<IActionResult> Placeholders([FromQuery] PlaceholdersQuery query)
        {
            try
            {
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch(Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}
