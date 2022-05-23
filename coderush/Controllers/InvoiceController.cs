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
    [Authorize(Roles = Pages.MainMenu.Invoice.RoleName)]
    public class InvoiceController : Controller
    {
        private readonly ApplicationDbContext _context;
        public InvoiceController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail(int id)
        {
            Invoice invoice = _context.Invoice
                .Include(x => x.SalesOrder)
                .SingleOrDefault(x => x.InvoiceId.Equals(id));

            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }
    }
}