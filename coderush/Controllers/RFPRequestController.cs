using coderush.Data;
using coderush.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Controllers
{
    public class RFPRequestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RFPRequestController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Detail(int id)
        {
            RFPRequest request = await _context.RFPRequest
                .Include(x => x.RFPCustomer)
                .SingleOrDefaultAsync(x => x.RFPRequestId.Equals(id));

            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }
    }
}
