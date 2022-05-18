﻿using System;
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

            public IActionResult Detail(int id)
            {
            Prescription prescription = _context.Prescription
                            .Include(x => x.Customer)
                           .SingleOrDefault(x => x.PrescriptionId.Equals(id));
                                

                if (prescription == null)
                {
                    return NotFound();
                }
                return View(prescription);
            }
        }
    
}