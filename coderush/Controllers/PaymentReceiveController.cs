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
    [Authorize(Roles = Pages.MainMenu.PaymentReceive.RoleName)]
    public class PaymentReceiveController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentReceiveController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail(int id)
        {
            PaymentReceive paymentReceive = _context.PaymentReceive.SingleOrDefault(x => x.PaymentReceiveId.Equals(id));

            if (paymentReceive == null)
            {
                return NotFound();
            }
            return View(paymentReceive);
        }
    }
}