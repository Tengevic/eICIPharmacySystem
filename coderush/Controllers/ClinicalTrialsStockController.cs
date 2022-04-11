using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coderush.Data;
using coderush.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace coderush.Controllers
{
    [Authorize(Roles = Pages.MainMenu.ClinicalTrialsStock.RoleName)]
    public class ClinicalTrialsStockController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClinicalTrialsStockController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
           
            return View();
        }

        public IActionResult Detail(int id)
        {
            ClinicalTrialsProduct product = _context.ClinicalTrialsProducts.SingleOrDefault(x => x.ClinicalTrialsProductId.Equals(id));

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}