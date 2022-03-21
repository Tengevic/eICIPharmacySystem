using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coderush.Data;
using coderush.Models;
using Microsoft.AspNetCore.Mvc;

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
            Stock stock = _context.Stock.SingleOrDefault(x => x.StockId.Equals(id));

            if (stock == null)
            {
                return NotFound();
            }

            return View(stock);
        }
    }
}