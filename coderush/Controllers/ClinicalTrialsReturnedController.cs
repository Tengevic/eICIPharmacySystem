using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coderush.Data;
using coderush.Models;
using coderush.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace coderush.Controllers
{
    [Authorize(Roles = Pages.MainMenu.ClinicalTrialsReturned.RoleName)]
    public class ClinicalTrialsReturnedController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClinicalTrialsReturnedController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int id)
        {
            ClinicalTrialsReturned clinicalTrialsReturned = _context.ClinicalTrialsReturned.SingleOrDefault(x => x.ClinicalTrialsReturnedId.Equals(id));

            if (clinicalTrialsReturned == null)
            {
                return NotFound();
            }
            return View(clinicalTrialsReturned);
        }
    }
}
