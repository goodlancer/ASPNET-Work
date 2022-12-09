
using Commands;
using Commands.Query;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
//ing Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ScolptioCRMWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : BaseController
    {
        private readonly IMediator _mediator;
        public FileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> GetFiles([FromBody] GetFilesQuery filesQuery)
        {
            filesQuery.OrgId = this.SecurityContext.OrgId;

            var result = await _mediator.Send(filesQuery);
            return Ok(result);
        }


        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> Add([FromBody] CreateFileCommand createFileCommand)
        {
            createFileCommand.OrgId = SecurityContext.OrgId;
            createFileCommand.UploadedBy = SecurityContext.UserId;
            var id = await _mediator.Send(createFileCommand);
            return Ok(id);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> UploadEcho3DFile([FromBody] CreateFileCommand createFileCommand)
        {
            var client = new RestClient("https://api.echo3D.co/upload");
            client.Timeout = -1;
            /*
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "multipart/form-data");
            request.AddParameter("key", "sparkling-frog-6857");
            request.AddParameter("email", "contact@scolptio.com");
            request.AddParameter("target_type", "2");
            request.AddParameter("hologram_type", "2");
            request.AddParameter("type", "upload");
            request.AddFile("file_model", "E://f_2022.5.4_scolptio//_design_obj//Cabinet with TAP and Basin.glb");
            request.AddParameter("secKey", "0zROLz9vCAyIvfXABW0rnQ5U");
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);*/

            var request2 = new RestRequest(Method.POST);
/*            request2.AddParameter("email", "richworld3tai@gmail.com");
            request2.AddParameter("target_type", "2");
            request2.AddParameter("hologram_type", "2");
            request2.AddParameter("type", "upload");
            request2.AddFile("file_model", "E://f_2022.5.4_scolptio//_design_obj//Cabinet with TAP and Basin.glb");
            request2.AddParameter("secKey", "1tTujWk3SsZTgtU6tXxEHVa9");
            request2.AddParameter("key", "cold-term-5928");
*/
            request2.AddParameter("email", "contact@scolptio.com");
            request2.AddParameter("target_type", "2");
            request2.AddParameter("hologram_type", "2");
            request2.AddParameter("type", "upload");
            //            request2.AddFile("file_model", "E://f_2022.5.4_scolptio//_design_obj//Cabinet with TAP and Basin.glb");
            request2.AddFile("file_model", createFileCommand.Url);
            request2.AddParameter("secKey", "0zROLz9vCAyIvfXABW0rnQ5U");
            request2.AddParameter("key", "sparkling-frog-6857");

            IRestResponse response1 = client.Execute(request2);
            Console.WriteLine(response1.Content);


//            createFileCommand.OrgId = SecurityContext.OrgId;
//            createFileCommand.UploadedBy = SecurityContext.UserId;
            // var id = await _mediator.Send(createFileCommand);
            return Ok(response1.Content);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> UploadEcho3DBlob( IFormFile assetfile)
        {
            string filePath = "";
            //string imagePath = HttpContext.Current.Server.MapPath("~/image/turnon.bmp");
            //var siteRoot = VirtualPathUtility.ToAppRelative("~");
            //string rootpath = Page.Request.ApplicationPath;

            string uploads = "C:\\";// Path.Combine("E:\\", "f_2022.5.4_scolptio");
            {
                if (assetfile.Length > 0)
                {
                    filePath = Path.Combine(uploads, assetfile.FileName);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await assetfile.CopyToAsync(fileStream);
                    }
                }
            }

            var client = new RestClient("https://api.echo3D.co/upload");
            client.Timeout = -1;


            var request2 = new RestRequest(Method.POST);
            /*            request2.AddParameter("email", "richworld3tai@gmail.com");
                        request2.AddParameter("target_type", "2");
                        request2.AddParameter("hologram_type", "2");
                        request2.AddParameter("type", "upload");
                        request2.AddFile("file_model", "E://f_2022.5.4_scolptio//_design_obj//Cabinet with TAP and Basin.glb");
                        request2.AddParameter("secKey", "1tTujWk3SsZTgtU6tXxEHVa9");
                        request2.AddParameter("key", "cold-term-5928");
            */
            request2.AddParameter("email", "contact@scolptio.com");
            request2.AddParameter("target_type", "2");
            request2.AddParameter("hologram_type", "2");
            request2.AddParameter("type", "upload");
            //            request2.AddFile("file_model", "E://f_2022.5.4_scolptio//_design_obj//Cabinet with TAP and Basin.glb");
            request2.AddFile("file_model", filePath);
            request2.AddParameter("secKey", "0zROLz9vCAyIvfXABW0rnQ5U");
            request2.AddParameter("key", "sparkling-frog-6857");

            IRestResponse response1 = client.Execute(request2);
            Console.WriteLine(response1.Content);


            //            createFileCommand.OrgId = SecurityContext.OrgId;
            //            createFileCommand.UploadedBy = SecurityContext.UserId;
            // var id = await _mediator.Send(createFileCommand);
            return Ok(response1.Content);
        }


        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> Delete([FromBody] DeleteFileCommand deleteFileCommand)
        {
            await _mediator.Send(deleteFileCommand);
            return Ok();
        }

    }
}
