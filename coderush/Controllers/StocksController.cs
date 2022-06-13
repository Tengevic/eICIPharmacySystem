using coderush.Data;
using coderush.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Controllers
{
    public class StocksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StocksController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Detail(int id)
        {
            Product product = _context.Product.SingleOrDefault(x => x.ProductId.Equals(id));

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public IActionResult saleHistory(int id)
        {
            Product product = _context.Product.SingleOrDefault(x => x.ProductId.Equals(id));

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        public IActionResult LowStock()
        {

            return View();
        }

    }
}
