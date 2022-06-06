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
    [Route("api/RFPDrugRecieves")]
    public class RFPDrugRecievesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INumberSequence _numberSequence;

        public RFPDrugRecievesController(ApplicationDbContext context,
                        INumberSequence numberSequence)
        {
            _context = context;
            _numberSequence = numberSequence;
        }

        [HttpGet]
        public async Task<IActionResult> GetRFPDrugRecieves()
        {
            List<RFPDrugRecieve> Items = await _context.RFPDrugRecieve.OrderByDescending(x => x.RFPDrugRecieveId).ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            RFPDrugRecieve result = await _context.RFPDrugRecieve
                .Where(x => x.RFPDrugRecieveId.Equals(id))
                .Include(x => x.goodsRecievedNoteLines)
                .FirstOrDefaultAsync();

            return Ok(result);

        }

        [HttpPost("[action]")]
        public IActionResult Insert([FromBody] CrudViewModel<RFPDrugRecieve> payload)
        {
            RFPDrugRecieve  RFPDrugRecieve = payload.value;
            RFPDrugRecieve.RFPDrugRecieveName = _numberSequence.GetNumberSequence("RDR");
            _context.RFPDrugRecieve.Add(RFPDrugRecieve);
            _context.SaveChanges();
            return Ok(RFPDrugRecieve);
        }
        [HttpPost("[action]")]
        public IActionResult Add([FromBody] RFPDrugRecieve payload)
        {
            RFPDrugRecieve RFPDrugRecieve = payload;

            RFPDrugRecieve drugRecieve = _context.RFPDrugRecieve.FirstOrDefault(x => x.RFPpaymentRecievedId == RFPDrugRecieve.RFPpaymentRecievedId);
            RFPDrugRecieve.RFPDrugRecieveName = _numberSequence.GetNumberSequence("RDR");
            _context.RFPDrugRecieve.Add(RFPDrugRecieve);
            _context.SaveChanges();
            return Ok(RFPDrugRecieve);
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody] CrudViewModel<RFPDrugRecieve> payload)
        {
            RFPDrugRecieve RFPDrugRecieve = payload.value;
            _context.RFPDrugRecieve.Update(RFPDrugRecieve);
            _context.SaveChanges();
            return Ok(RFPDrugRecieve);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody] CrudViewModel<RFPDrugRecieve> payload)
        {
            RFPDrugRecieve RFPDrugRecieve = _context.RFPDrugRecieve
                .Where(x => x.RFPDrugRecieveId == (int)payload.key)
                .Include(x => x.goodsRecievedNoteLines)
                .FirstOrDefault();
            if (RFPDrugRecieve.goodsRecievedNoteLines.Count > 0)
            {
                Err err = new Err
                {
                    message = "Record has recieved goods"
                };
                string errMsg = JsonConvert.SerializeObject(err);

                return BadRequest(err);
            }
            _context.RFPDrugRecieve.Remove(RFPDrugRecieve);
            _context.SaveChanges();
            return Ok(RFPDrugRecieve);

        }
    }
}