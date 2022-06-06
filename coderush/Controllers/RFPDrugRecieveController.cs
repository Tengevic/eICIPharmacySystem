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
    public class RFPDrugRecieveController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RFPDrugRecieveController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail(int id)
        {
            RFPDrugRecieve goodsReceivedNote = _context.RFPDrugRecieve
                .Include(x => x.RFPpaymentRecieved)
                    .ThenInclude(x => x.RFPinvoice)
                .SingleOrDefault(x => x.RFPDrugRecieveId.Equals(id));
            if (goodsReceivedNote == null)
            {
                return NotFound();
            }
            return View(goodsReceivedNote);
        }
    }
}
