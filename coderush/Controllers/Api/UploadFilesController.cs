using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using coderush.Data;
using coderush.Models;
using coderush.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace coderush.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/UploadFiles")]
    public class UploadFilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UploadFilesController( ApplicationDbContext context)
        {
       
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Upload()
        {
            List<Upload> Items = _context.Uploads.ToList();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }
        [HttpPost]
        public async Task<IActionResult> PostUpload(List<IFormFile> UploadDefault)
        {
            
            try
            {
                
                var headers = Request.Headers["test"];
                int PaymentReceiveId = Convert.ToInt32(headers);

                foreach (var formFile in UploadDefault)
                {
                 
                    using (var memoryStream = new MemoryStream())
                    {
                        await formFile.CopyToAsync(memoryStream);
                        var filename = formFile.FileName;

                        Upload file = new Upload
                        {
                            filename = filename,
                            PaymentReceiveId = PaymentReceiveId,
                            Content = memoryStream.ToArray()
                        };

                        _context.Uploads.Add(file);
                        _context.SaveChanges();
                    }   
                }


                return Ok();
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = ex.Message });
            }


        }
    }

}