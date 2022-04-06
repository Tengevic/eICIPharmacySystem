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
    [Authorize(Roles = Pages.MainMenu.ClinicalTrialsDonations.RoleName)]
    public class ClinicalTrialsDonationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ClinicalTrialsDonationsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail(int id)
        {
            ClinicalTrialsDonation clinicalTrialsDonation = _context.ClinicalTrialsDonation.SingleOrDefault(x => x.ClinicalTrialsDonationId.Equals(id));

            if (clinicalTrialsDonation == null)
            {
                return NotFound();
            }

            return View(clinicalTrialsDonation);
        }
    }
}