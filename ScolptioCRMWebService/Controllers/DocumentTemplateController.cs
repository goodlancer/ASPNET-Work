using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ScolptioCRMWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentTemplateController : BaseController
    {

        [HttpGet]
        public async Task<ActionResult> GetTemplate(int TemplateId)
        {
            string body = string.Empty;
            //using streamreader for reading my htmltemplate   
            using (StreamReader reader = new StreamReader(Path.Combine("Template", "Template_"+TemplateId + ".html")))
            {
                body = reader.ReadToEnd();
            }
            return Ok(new { template = body });
        }
    }
}
