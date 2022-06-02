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
using Newtonsoft.Json;

namespace coderush.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/ClinicalTrialsSales")]
    public class ClinicalTrialsSalesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INumberSequence _numberSequence;

        public ClinicalTrialsSalesController(ApplicationDbContext context,
                        INumberSequence numberSequence)
        {
            _context = context;
            _numberSequence = numberSequence;
        }

        // GET: api/ClinicalTrialsSales
        [HttpGet]
        public async Task<IActionResult> GetSalesOrder()
        {
            List<ClinicalTrialsSales> Items = await _context.ClinicalTrialsSales.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        // GET: api/ClinicalTrialsSales/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            ClinicalTrialsSales result = await _context.ClinicalTrialsSales
                .Where(x => x.ClinicalTrialsSalesId.Equals(id))
                .Include(x => x.clinicalTrialsSalesLine)
                .FirstOrDefaultAsync();

            return Ok(result);
        }

        // PUT: api/ClinicalTrialsSales/5
        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<ClinicalTrialsSales> payload)
        {
            ClinicalTrialsSales clinicalTrialsSales = payload.value;
            clinicalTrialsSales.ClinicalTrialsSalesName = _numberSequence.GetNumberSequence("CTS");
            _context.ClinicalTrialsSales.Add(clinicalTrialsSales);
            _context.SaveChanges();
            //this.UpdateSalesOrder(salesOrder.SalesOrderId);
            return Ok(clinicalTrialsSales);
        }

        // POST: api/ClinicalTrialsSales
        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<ClinicalTrialsSales> payload)
        {
            ClinicalTrialsSales clinicalTrialsSales = payload.value;
            _context.ClinicalTrialsSales.Update(clinicalTrialsSales);
            _context.SaveChanges();
            return Ok(clinicalTrialsSales);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<ClinicalTrialsSales> payload)
        {
            ClinicalTrialsSales clinicalTrialsSales = _context.ClinicalTrialsSales
                .Where(x => x.ClinicalTrialsSalesId == (int)payload.key)
                .Include(x => x.clinicalTrialsSalesLine)
                .FirstOrDefault();
            if (clinicalTrialsSales.clinicalTrialsSalesLine.Count > 0)
            {
                Err err = new Err
                {
                    message = "Record has use record"
                };
                string errMsg = JsonConvert.SerializeObject(err);

                return BadRequest(err);
            }
            _context.ClinicalTrialsSales.Remove(clinicalTrialsSales);
            _context.SaveChanges();
            return Ok(clinicalTrialsSales);

        }
    }
}