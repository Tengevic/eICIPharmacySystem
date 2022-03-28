using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coderush.Data;
using coderush.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            List<GoodsRecievedNoteLine> drugs =  _context.GoodsRecievedNoteLine
                                    .ToList();

            DateTime current = DateTime.Now;
            int oneMonth = 0;
            int twomonths = 0;
            int threemonths = 0;

            foreach (GoodsRecievedNoteLine drug in drugs)
            {
                Double months = (drug.ExpiryDate - current).TotalDays;

                if (months <= 30)
                {
                    oneMonth = oneMonth + 1;
                }
                else if (months <= 60 && months > 30)
                {
                    twomonths = twomonths + 1;
                }
                if (months <= 90 && months > 60)
                {
                    threemonths = threemonths + 1;
                }
            }
            if (oneMonth > 0)
            {
                ViewBag.error = String.Format(oneMonth + " drug(s) batch will expire in one month");
            }
            if (twomonths > 0)
            {
                ViewBag.warning = String.Format(twomonths + " drug(s) batch will expire in two months");
            }
            if (threemonths > 0)
            {
                ViewBag.info = String.Format(oneMonth + " drug(s) batch will expire in three months");
            }

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