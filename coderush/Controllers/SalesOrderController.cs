using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coderush.Data;
using coderush.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace coderush.Controllers
{
    [Authorize(Roles = Pages.MainMenu.SalesOrder.RoleName)]
    public class SalesOrderController : Controller
    {
        private readonly ApplicationDbContext _context;
      
        public SalesOrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Detail(int id)
        {
            SalesOrder salesOrder = await _context.SalesOrder
                .Where(x => x.SalesOrderId.Equals(id))
                .Include(x =>x.Prescription)
                .Include(x => x.Invoice)
                .FirstAsync();
                
            
            if (salesOrder == null)
            {
                return NotFound();
            }
           
            return View(salesOrder);
        }
    }
}