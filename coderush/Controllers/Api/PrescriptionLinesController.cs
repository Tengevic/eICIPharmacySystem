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
using Newtonsoft.Json;

namespace coderush.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/PrescriptionLines")]
    public class PrescriptionLinesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INumberSequence _numberSequence;

        public PrescriptionLinesController(ApplicationDbContext context,
                        INumberSequence numberSequence)
        {
            _context = context;
            _numberSequence = numberSequence;
        }
        [HttpGet]
        public async Task<IActionResult> GetPrescriptionLine()
        {
            var headers = Request.Headers["PrescriptionId"];
            int PrescriptionId = Convert.ToInt32(headers);

            if (PrescriptionId != 0)
            {
                List<PrescriptionLines> Items = await _context.PrescriptionLines
                    .Where(x => x.PrescriptionId == PrescriptionId)
                    .ToListAsync();
                int Count = Items.Count();
                return Ok(new { Items, Count });
            }
            else
            {
                List<PrescriptionLines> Items = await _context.PrescriptionLines.ToListAsync();
                int Count = Items.Count();
                return Ok(new { Items, Count });
            }
        }
        //api/PrescriptionLines/GetByPrescriptionId
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetByPrescriptionId([FromRoute] int id)
        {
            List<PrescriptionLines> Items = await _context.PrescriptionLines
                    .Include(x => x.Product.UnitOfMeasure)
                    .Include(x => x.Product.ProductType)
                    .Where(x => x.PrescriptionId == id)
                    .ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }
        [HttpPost("[action]")]
        public IActionResult Insert([FromBody] CrudViewModel<PrescriptionLines> payload)
        {
            PrescriptionLines prescriptionLines = payload.value;
            Prescription prescription = _context.Prescription
                .Where(x => x.PrescriptionId == prescriptionLines.PrescriptionId)
                .Include(x => x.SalesOrder)
                .FirstOrDefault();
            if(prescription.SalesOrder != null)
            {
                Err err = new Err
                {
                    message = "This prescription is already sold"
                };
                string errMsg = JsonConvert.SerializeObject(err);

                return BadRequest(err);
            }
            prescriptionLines.OderId = 0;
            prescriptionLines.PrescriptionLinesName = _numberSequence.GetNumberSequence("PSL");
            _context.PrescriptionLines.Add(prescriptionLines);
            _context.SaveChanges();
            return Ok(prescriptionLines);
        }
        //Endpoint
        [HttpPost("[action]")]
        public IActionResult Add([FromBody] PrescriptionLines payload)
        {
            PrescriptionLines prescriptionLines = payload;
            prescriptionLines.OderId = 0;
            Prescription prescription = _context.Prescription
               .Where(x => x.PrescriptionId == prescriptionLines.PrescriptionId)
               .Include(x => x.SalesOrder)
               .FirstOrDefault();
            if (prescription.SalesOrder != null)
            {
                Err err = new Err
                {
                    message = "This prescription is already sold"
                };
                string errMsg = JsonConvert.SerializeObject(err);

                return BadRequest(err);
            }
            prescriptionLines.PrescriptionLinesName = _numberSequence.GetNumberSequence("PSL");
            _context.PrescriptionLines.Add(prescriptionLines);
            _context.SaveChanges();
            return Ok(prescriptionLines);
        }
        [HttpPost("[action]")]
        public IActionResult Update([FromBody] CrudViewModel<PrescriptionLines> payload)
        {
            PrescriptionLines prescriptionLines = payload.value;
            Prescription prescription = _context.Prescription
               .Where(x => x.PrescriptionId == prescriptionLines.PrescriptionId)
               .Include(x => x.SalesOrder)
               .FirstOrDefault();
            if (prescription.SalesOrder != null)
            {
                Err err = new Err
                {
                    message = "This prescription is already sold"
                };
                string errMsg = JsonConvert.SerializeObject(err);

                return BadRequest(err);
            }
            _context.PrescriptionLines.Update(prescriptionLines);
            _context.SaveChanges();
            return Ok(prescriptionLines);
        }
        [HttpPost("[action]")]
        public IActionResult Put([FromBody] PrescriptionLines payload)
        {
            PrescriptionLines prescriptionLines = payload;
            Prescription prescription = _context.Prescription
               .Where(x => x.PrescriptionId == prescriptionLines.PrescriptionId)
               .Include(x => x.SalesOrder)
               .FirstOrDefault();
            if (prescription.SalesOrder != null)
            {
                Err err = new Err
                {
                    message = "This prescription is already sold"
                };
                string errMsg = JsonConvert.SerializeObject(err);

                return BadRequest(err);
            }
            _context.PrescriptionLines.Update(prescriptionLines);
            _context.SaveChanges();
            return Ok(prescriptionLines);
        }
        [HttpPost("[action]")]
        public IActionResult Remove([FromBody] CrudViewModel<PrescriptionLines> payload)
        {
            PrescriptionLines prescriptionLines = _context.PrescriptionLines
                .Where(x => x.PrescriptionLinesId == (int)payload.key)
                .FirstOrDefault();
            _context.PrescriptionLines.Remove(prescriptionLines);
            _context.SaveChanges();
            return Ok(prescriptionLines);

        }
        [HttpPost("[action]/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            PrescriptionLines prescriptionLines = _context.PrescriptionLines
                .Where(x => x.PrescriptionLinesId == id)
                .FirstOrDefault();
            _context.PrescriptionLines.Remove(prescriptionLines);
            _context.SaveChanges();
            return Ok(prescriptionLines); ;

        }
    }
}
