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

        public async Task<IActionResult> Detail(int id)
        {
            Product product = await _context.Product.SingleOrDefaultAsync(x => x.ProductId.Equals(id));

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        public async Task<IActionResult> saleHistory(int id)
        {
            Product product = await _context.Product.SingleOrDefaultAsync(x => x.ProductId.Equals(id));

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public async Task<IActionResult> changeStock(int id)
        {
            GoodsRecievedNoteLine goodsRecievedNoteLine = await _context.GoodsRecievedNoteLine
                            .Include(x => x.Product)
                            .Where(x => x.GoodsRecievedNoteLineId == id)
                            .FirstOrDefaultAsync();
            
            if (goodsRecievedNoteLine == null)
            {
                return NotFound();
            }

            return View(goodsRecievedNoteLine);
        }
        public IActionResult LowStock()
        {

            return View();
        }

    }
}
