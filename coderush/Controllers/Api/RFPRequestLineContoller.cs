using coderush.Data;
using coderush.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/RFPRequestLine")]
    public class RFPRequestLineContoller : Controller
    {
        private readonly ApplicationDbContext _context;

        public RFPRequestLineContoller(ApplicationDbContext context)
        {
            _context = context;
    

        }
        [HttpGet]
        public async Task<IActionResult> GetRFPRequestline()
        {
            var headers = Request.Headers["RFPRequestId"];
            int RFPRequestId = Convert.ToInt32(headers);
            List<RFPRequestLine> Items = await _context.RFPRequestLine
                .Where(x => x.RFPRequestId == RFPRequestId)
                .ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }
    }
}
