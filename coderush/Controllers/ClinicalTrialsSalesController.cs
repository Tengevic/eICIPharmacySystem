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
    [Authorize(Roles = Pages.MainMenu.ClinicalTrialsSales.RoleName)]
    public class ClinicalTrialsSalesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClinicalTrialsSalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int id)
        {
            ClinicalTrialsSales clinicalTrialsSales = _context.ClinicalTrialsSales.SingleOrDefault(x => x.ClinicalTrialsSalesId.Equals(id));

            if (clinicalTrialsSales == null)
            {
                return NotFound();
            }
            return View(clinicalTrialsSales);
        }
    }
}