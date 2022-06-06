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
    [Authorize]
    [Produces("application/json")]
    [Route("api/GoodsReceivedNote")]
    public class GoodsReceivedNoteController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INumberSequence _numberSequence;

        public GoodsReceivedNoteController(ApplicationDbContext context,
                        INumberSequence numberSequence)
        {
            _context = context;
            _numberSequence = numberSequence;
        }

        // GET: api/GoodsReceivedNote
        // GET: api/GoodsReceivedNote
        [HttpGet]
        public async Task<IActionResult> GetGoodsReceivedNote()
        {
            List<GoodsReceivedNote> Items = await _context.GoodsReceivedNote.OrderByDescending(x => x.GoodsReceivedNoteId).ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            GoodsReceivedNote result = await _context.GoodsReceivedNote
                .Where(x => x.GoodsReceivedNoteId.Equals(id))
                .Include(x => x.goodsRecievedNoteLines)
                .FirstOrDefaultAsync();

            return Ok(result);

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetNotBilledYet()
        {
            List<GoodsReceivedNote> goodsReceivedNotes = new List<GoodsReceivedNote>();
            try
            {
                List<Bill> bills = new List<Bill>();
                bills = await _context.Bill.ToListAsync();
                List<int> ids = new List<int>();

                foreach (var item in bills)
                {
                    ids.Add(item.GoodsReceivedNoteId);
                }

                goodsReceivedNotes = await _context.GoodsReceivedNote
                    .Where(x => !ids.Contains(x.GoodsReceivedNoteId))
                    .ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(goodsReceivedNotes);
        }

        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<GoodsReceivedNote> payload)
        {
            GoodsReceivedNote goodsReceivedNote = payload.value;
            goodsReceivedNote.GoodsReceivedNoteName = _numberSequence.GetNumberSequence("GRN");
            if (goodsReceivedNote.IsFullReceive)
            {
                PurchaseOrder purchaseOrder = _context.PurchaseOrder.Find(goodsReceivedNote.PurchaseOrderId);
                purchaseOrder.fullyPaid = goodsReceivedNote.IsFullReceive;
                _context.PurchaseOrder.Update(purchaseOrder);
                _context.SaveChanges();
            }
            _context.GoodsReceivedNote.Add(goodsReceivedNote);
            _context.SaveChanges();
            return Ok(goodsReceivedNote);
        }
        [HttpPost("[action]")]
        public IActionResult Add([FromBody] GoodsReceivedNote payload)
        {
            GoodsReceivedNote goodsReceivedNote = payload;
            goodsReceivedNote.GoodsReceivedNoteName = _numberSequence.GetNumberSequence("GRN");
            if (goodsReceivedNote.IsFullReceive)
            {
                PurchaseOrder purchaseOrder = _context.PurchaseOrder.Find(goodsReceivedNote.PurchaseOrderId);
                purchaseOrder.fullyPaid = goodsReceivedNote.IsFullReceive;
                _context.PurchaseOrder.Update(purchaseOrder);
                _context.SaveChanges();
            }
            _context.GoodsReceivedNote.Add(goodsReceivedNote);
            _context.SaveChanges();
            return Ok(goodsReceivedNote);
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<GoodsReceivedNote> payload)
        {
            GoodsReceivedNote goodsReceivedNote = payload.value;
            if (goodsReceivedNote.IsFullReceive)
            {
                PurchaseOrder purchaseOrder = _context.PurchaseOrder.Find(goodsReceivedNote.PurchaseOrderId);
                purchaseOrder.fullyPaid = goodsReceivedNote.IsFullReceive;
                _context.PurchaseOrder.Update(purchaseOrder);
                _context.SaveChanges();
            }
            _context.GoodsReceivedNote.Update(goodsReceivedNote);
            _context.SaveChanges();
            return Ok(goodsReceivedNote);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<GoodsReceivedNote> payload)
        {
            GoodsReceivedNote goodsReceivedNote = _context.GoodsReceivedNote
                .Where(x => x.GoodsReceivedNoteId == (int)payload.key)
                .Include(x => x.goodsRecievedNoteLines)
                .FirstOrDefault();
            if (goodsReceivedNote.IsFullReceive)
            {
                PurchaseOrder purchaseOrder = _context.PurchaseOrder.Find(goodsReceivedNote.PurchaseOrderId);
                purchaseOrder.fullyPaid = !goodsReceivedNote.IsFullReceive;
                _context.PurchaseOrder.Update(purchaseOrder);
                _context.SaveChanges();
            }
            if (goodsReceivedNote.goodsRecievedNoteLines.Count > 0)
            {
                Err err = new Err
                {
                    message = "Record has recieved goods"
                };
                string errMsg = JsonConvert.SerializeObject(err);

                return BadRequest(err);
            }
            _context.GoodsReceivedNote.Remove(goodsReceivedNote);
            _context.SaveChanges();
            return Ok(goodsReceivedNote);

        }
    }
}