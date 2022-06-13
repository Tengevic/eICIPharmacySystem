using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using coderush.Data;
using coderush.Models;
using Microsoft.AspNetCore.Identity;

namespace coderush.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/stockNumber")]
    public class stockNumbersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public stockNumbersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/stockNumbers
        [HttpGet]
        public async Task<IActionResult> GetstockNumber()
        {
            List<stockNumber> Items = await _context.stockNumber.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Add([FromBody] stockNumber payload)
        {
            stockNumber stockNumber = payload;
            stockNumber.quantity = stockNumber.Add - stockNumber.subtract;
            var user = await _userManager.GetUserAsync(HttpContext.User);
            stockNumber.UserId = user.Id;
            _context.stockNumber.Add(stockNumber);
            _context.SaveChanges();
            UpdateBatch(stockNumber.GoodsRecievedNoteLineId);
            return Ok(stockNumber);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Update([FromBody] stockNumber payload)
        {
            stockNumber stockNumber = payload;
            stockNumber.quantity = stockNumber.Add - stockNumber.subtract;
            var user = await _userManager.GetUserAsync(HttpContext.User);
            stockNumber.UserId = user.Id;
            _context.stockNumber.Update(stockNumber);
            _context.SaveChanges();
            UpdateBatch(stockNumber.GoodsRecievedNoteLineId);
            return Ok(stockNumber);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody] stockNumber payload)
        {
            stockNumber stockNumber = _context.stockNumber
                .Where(x => x.stockNumberId == payload.stockNumberId)
                .FirstOrDefault();
            _context.stockNumber.Remove(stockNumber);
            _context.SaveChanges();
            UpdateBatch(stockNumber.GoodsRecievedNoteLineId);
            return Ok(stockNumber);

        }
        private void UpdateBatch(int batchId)
        {
            try
            {
                GoodsRecievedNoteLine batch = _context.GoodsRecievedNoteLine.Find(batchId);
                if (batch != null)
                {
                    List<SalesOrderLine> lines = new List<SalesOrderLine>();
                    lines = _context.SalesOrderLine.Where(x => x.GoodsRecievedNoteLineId.Equals(batch.GoodsRecievedNoteLineId)).ToList();
                    
                    List<stockNumber> stockNumber = _context.stockNumber.Where(x => x.GoodsRecievedNoteLineId.Equals(batch.GoodsRecievedNoteLineId)).ToList();

                    batch.changestock = stockNumber.Sum(x => x.Add) - stockNumber.Sum(x => x.subtract);
                    batch.Sold = lines.Sum(x => x.Quantity);
                    batch.InStock = batch.Quantity - batch.Sold - batch.Expired - batch.changestock;

                    _context.Update(batch);

                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}