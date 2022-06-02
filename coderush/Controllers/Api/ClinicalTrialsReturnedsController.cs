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
    [Route("api/ClinicalTrialsReturneds")]
    public class ClinicalTrialsReturnedsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INumberSequence _numberSequence;

        public ClinicalTrialsReturnedsController(ApplicationDbContext context,
                        INumberSequence numberSequence)
        {
            _context = context;
            _numberSequence = numberSequence;
        }

        // GET: api/ClinicalTrialsReturneds
        [HttpGet]
        public async Task<IActionResult> GetReturnOrder()
        {
            List<ClinicalTrialsReturned> Items = await _context.ClinicalTrialsReturned.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        // GET: api/ClinicalTrialsReturneds/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            ClinicalTrialsReturned result = await _context.ClinicalTrialsReturned
                .Where(x => x.ClinicalTrialsReturnedId.Equals(id))
                .Include(x => x.ClinicalTrialsReturnedLines)
                .FirstOrDefaultAsync();

            return Ok(result);
        }

        // PUT: api/ClinicalTrialsReturneds/5
        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<ClinicalTrialsReturned> payload)
        {
            ClinicalTrialsReturned clinicalTrialsReturned = payload.value;
            clinicalTrialsReturned.ClinicalTrialsReturnedName = _numberSequence.GetNumberSequence("CTR");
            _context.ClinicalTrialsReturned.Add(clinicalTrialsReturned);
            _context.SaveChanges();
            //this.UpdateSalesOrder(salesOrder.SalesOrderId);
            return Ok(clinicalTrialsReturned);
        }

        // POST: api/ClinicalTrialsReturneds
        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<ClinicalTrialsReturned> payload)
        {
            ClinicalTrialsReturned clinicalTrialsReturned = payload.value;
            _context.ClinicalTrialsReturned.Update(clinicalTrialsReturned);
            _context.SaveChanges();
            return Ok(clinicalTrialsReturned);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<ClinicalTrialsReturned> payload)
        {
            ClinicalTrialsReturned clinicalTrialsReturned = _context.ClinicalTrialsReturned
                .Where(x => x.ClinicalTrialsReturnedId == (int)payload.key)
                .Include(x => x.ClinicalTrialsReturnedLines)
                .FirstOrDefault();
            if (clinicalTrialsReturned.ClinicalTrialsReturnedLines.Count > 0)
            {
                Err err = new Err
                {
                    message = "Record has return records"
                };
                string errMsg = JsonConvert.SerializeObject(err);

                return BadRequest(err);
            }
            _context.ClinicalTrialsReturned.Remove(clinicalTrialsReturned);
            _context.SaveChanges();
            return Ok(clinicalTrialsReturned);

        }
    }
}