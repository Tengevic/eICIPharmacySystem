using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coderush.Data;
using coderush.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace coderush.Controllers
{
    [Authorize(Roles = Pages.MainMenu.Prescription.RoleName)]
    public class PrescriptionController : Controller
    {
           
            private readonly ApplicationDbContext _context;

            public PrescriptionController(ApplicationDbContext context)
            {
                _context = context;
            }

            public IActionResult Index()
            {
                return View();
            }

            public async Task<IActionResult> Detail(int id)
            {
            Prescription prescription = await _context.Prescription
                            .Include(x => x.Customer)
                            .Include(x=> x.SalesOrder)
                           .FirstOrDefaultAsync(x => x.PrescriptionId.Equals(id));
                                

                if (prescription == null)
                {
                    return NotFound();
                }
                return View(prescription);
            }
        }
    
}
