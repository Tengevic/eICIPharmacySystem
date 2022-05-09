using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using coderush.Data;
using coderush.Models;
using coderush.Services;
using coderush.Models.SyncfusionViewModels;
using Microsoft.AspNetCore.Authorization;

namespace coderush.Controllers.Api
{
   
    [Produces("application/json")]
    [Route("api/Prescription")]
    public class PrescriptionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INumberSequence _numberSequence;

        public PrescriptionController(ApplicationDbContext context,
                        INumberSequence numberSequence)
        {
            _context = context;
            _numberSequence = numberSequence;
        }
        [HttpGet]
        public async Task<IActionResult> GetPrescription()
        {
            List<Prescription> Items = await _context.Prescription.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }
        //api/Prescription/GetNotSoldYet
        [HttpGet("[action]")]
        public async Task<IActionResult> GetNotSoldYet()
        {
            List<Prescription> prescription = new List<Prescription>();
            try
            {
                List<SalesOrder> salesOrder = new List<SalesOrder>();
                salesOrder = await _context.SalesOrder.ToListAsync();
                List<int?> ids = new List<int?>();

                foreach (var item in salesOrder)
                {
                    ids.Add(item.PrescriptionId);
                }

                prescription = await _context.Prescription
                    .Where(x => !ids.Contains(x.PrescriptionId))
                    .ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(prescription);
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Prescription result = await _context.Prescription
                .Where(x => x.PrescriptionId.Equals(id))
                .Include(x => x.prescriptionLines)
                .FirstOrDefaultAsync();

            return Ok(result);
        }
        [HttpPost("[action]")]
        public IActionResult Insert([FromBody] CrudViewModel<Prescription> payload)
        {
            Prescription prescription = payload.value;
            prescription.PrescriptionName = _numberSequence.GetNumberSequence("PS");
            _context.Prescription.Add(prescription);
            _context.SaveChanges();        
            return Ok(prescription);
        }
        //Endpoint
        [HttpPost("[action]")]
        public IActionResult Add([FromBody] Prescription payload)
        {
            Prescription prescription = payload;
            prescription.PrescriptionName = _numberSequence.GetNumberSequence("PS");
            _context.Prescription.Add(prescription);
            _context.SaveChanges();
            return Ok(prescription);
        }
        [HttpPost("[action]")]
        public IActionResult Update([FromBody] CrudViewModel<Prescription> payload)
        {
            Prescription prescription = payload.value;
            _context.Prescription.Update(prescription);
            _context.SaveChanges();
            return Ok(prescription);
        }
        [HttpPost("[action]")]
        public IActionResult Put([FromBody] Prescription payload)
        {
            Prescription prescription = payload;
            _context.Prescription.Update(prescription);
            _context.SaveChanges();
            return Ok(prescription);
        }
        [HttpPost("[action]")]
        public IActionResult Remove([FromBody] CrudViewModel<Prescription> payload)
        {
            Prescription prescription = _context.Prescription
                .Where(x => x.PrescriptionId == (int)payload.key)
                .FirstOrDefault();
            _context.Prescription.Remove(prescription);
            _context.SaveChanges();
            return Ok(prescription);

        }
        [HttpPost("[action]/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            Prescription prescription = _context.Prescription
                 .Where(x => x.PrescriptionId == id)
                 .FirstOrDefault();
            _context.Prescription.Remove(prescription);
            _context.SaveChanges();
            return Ok(prescription);

        }
    }
}
