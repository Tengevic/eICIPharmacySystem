using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult DrugHistory()
        {
            return View();
        }
        public IActionResult CustomerHistory()
        {
            return View();
        }
        public IActionResult SalesVSPurchase()
        {
            return View();
        }
    }
}
