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
    public class RFPSaleorderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RFPSaleorderController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int id)
        {
            RFPSaleorder salesOrder = _context.RFPSaleorder
                .SingleOrDefault(x => x.RFPSaleorderId.Equals(id));

            if (salesOrder == null)
            {
                return NotFound();
            }

            return View(salesOrder);
        }
    }
}
