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

namespace coderush.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/ClinicalTrialsDonations")]
    public class ClinicalTrialsDonationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INumberSequence _numberSequence;

        public ClinicalTrialsDonationsController(ApplicationDbContext context,
                        INumberSequence numberSequence)
        {
            _context = context;
            _numberSequence = numberSequence;
        }

        // GET: api/ClinicalTrialsDonations
        [HttpGet]
        public async Task<IActionResult> GetClinicalTrialsDonations()
        {
            List<ClinicalTrialsDonation> Items = await _context.ClinicalTrialsDonation.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        // GET: api/ClinicalTrialsDonations/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            ClinicalTrialsDonation result = await _context.ClinicalTrialsDonation
                .Where(x => x.ClinicalTrialsDonationId.Equals(id))
                .Include(x => x.clinicalTrialsDonationLine)
                .FirstOrDefaultAsync();

            return Ok(result);

        }
     

        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<ClinicalTrialsDonation> payload)
        {
            ClinicalTrialsDonation clinicalTrialsDonation = payload.value;
            clinicalTrialsDonation.ClinicalTrialsDonationName = _numberSequence.GetNumberSequence("CTD");
            _context.ClinicalTrialsDonation.Add(clinicalTrialsDonation);
            _context.SaveChanges();
            return Ok(clinicalTrialsDonation);
        }
        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<ClinicalTrialsDonation> payload)
        {
            ClinicalTrialsDonation clinicalTrialsDonation = payload.value;
            _context.ClinicalTrialsDonation.Update(clinicalTrialsDonation);
            _context.SaveChanges();
            return Ok(clinicalTrialsDonation);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<ClinicalTrialsDonation> payload)
        {
            ClinicalTrialsDonation clinicalTrialsDonation = _context.ClinicalTrialsDonation
                .Where(x => x.ClinicalTrialsDonationId == (int)payload.key)
                .FirstOrDefault();
            _context.ClinicalTrialsDonation.Remove(clinicalTrialsDonation);
            _context.SaveChanges();
            return Ok(clinicalTrialsDonation);

        }

    }
}