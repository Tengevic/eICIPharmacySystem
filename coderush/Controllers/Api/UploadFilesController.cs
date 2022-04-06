using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using coderush.Data;
using coderush.Models;
using coderush.Models.SyncfusionViewModels;
using coderush.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var headers = Request.Headers["PaymentReceiveId"];
            int PaymentReceiveId = Convert.ToInt32(headers);
            List<uploadVm> Items = new List<uploadVm>();

            if (PaymentReceiveId != 0) 
            {
                List<Upload> upload = await _context.Uploads.Where(x => x.PaymentReceiveId == PaymentReceiveId).ToListAsync();
                
                foreach (Upload u in upload)
                {
                    uploadVm item = new uploadVm
                    {
                        Id = u.Id,
                        filename = u.filename,
                        PaymentReceiveId = u.PaymentReceiveId
                    };
                    Items.Add(item);
                }
            }
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }
        [HttpPost]
        public async Task<IActionResult> PostUpload(List<IFormFile> UploadDefault)
        {
            var headers = Request.Headers["test"];
            int PaymentReceiveId = Convert.ToInt32(headers);
            try
            {
                foreach (var formFile in UploadDefault)
                {
                 
                    using (var memoryStream = new MemoryStream())
                    {
                        await formFile.CopyToAsync(memoryStream);
                        var filename = formFile.FileName;
                        var contentType = formFile.ContentType;

                        Upload file = new Upload
                        {
                            filename = filename,
                            PaymentReceiveId = PaymentReceiveId,
                            contentType = contentType,
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
        [HttpPost("[action]")]
        public IActionResult Remove([FromBody] CrudViewModel<uploadVm> payload)
        {
            Upload upload = _context.Uploads
                .Where(x => x.Id == (int)payload.key)
                .FirstOrDefault();
            _context.Uploads.Remove(upload);
            _context.SaveChanges();
            return Ok(upload);

        }
        [HttpGet("[action]/{id}")]
        public FileContentResult Download([FromRoute] string id)
        {
            int Id = Convert.ToInt32(id);
            Upload file = _context.Uploads.Find(Id);

            return  File(file.Content,file.contentType, file.filename);
        }
        public class uploadVm 
        {
            public int Id { get; set; }
            public string filename { get; set; }
            public int PaymentReceiveId { get; set; }
        }
    }

}