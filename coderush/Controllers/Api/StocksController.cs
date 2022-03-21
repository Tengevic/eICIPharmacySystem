using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using coderush.Data;
using coderush.Models;
using coderush.Models.SyncfusionViewModels;

namespace coderush.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Stocks")]
    public class StocksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StocksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Stocks
        [HttpGet]
        public async Task<IActionResult> GetStock()
        {
            List<Stock> Items = await _context.Stock.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }
        private void UpdateStock(int productId)
        {
            try
            {
                Stock stock = new Stock();
                stock = _context.Stock
                    .Where(x => x.ProductId.Equals(productId))
                    .FirstOrDefault();

                if (stock != null)
                {
                    List<GoodsRecievedNoteLine> line = new List<GoodsRecievedNoteLine>();
                    line = _context.GoodsRecievedNoteLine.Where(x => x.ProductId.Equals(productId)).ToList();

                    stock.TotalRecieved = line.Sum(x => x.Quantity);

                    List<SalesOrderLine> lines = new List<SalesOrderLine>();
                    lines = _context.SalesOrderLine.Where(x => x.ProductId.Equals(productId)).ToList();

                    stock.TotalSales = lines.Sum(x => x.Quantity);


                    stock.InStock = stock.TotalRecieved - stock.TotalSales;

                    _context.Update(stock);

                    _context.SaveChanges();

                }

            }
            catch (Exception)
            {

                throw;
            }

        }
        // POST: api/GoodsRecievedNoteLines/5
        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<Stock> payload)
        {

            Stock stock = payload.value;
            _context.Stock.Add(stock);
            _context.SaveChanges();
            this.UpdateStock(stock.ProductId);
            return Ok(stock);
        }

        //// GET: api/Stocks/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetStock([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var stock = await _context.Stock.SingleOrDefaultAsync(m => m.Id == id);

        //    if (stock == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(stock);
        //}

        //// PUT: api/Stocks/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutStock([FromRoute] int id, [FromBody] Stock stock)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != stock.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(stock).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!StockExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Stocks
        //[HttpPost]
        //public async Task<IActionResult> PostStock([FromBody] Stock stock)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.Stock.Add(stock);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetStock", new { id = stock.Id }, stock);
        //}

        //// DELETE: api/Stocks/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteStock([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var stock = await _context.Stock.SingleOrDefaultAsync(m => m.Id == id);
        //    if (stock == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Stock.Remove(stock);
        //    await _context.SaveChangesAsync();

        //    return Ok(stock);
        //}

        //private bool StockExists(int id)
        //{
        //    return _context.Stock.Any(e => e.Id == id);
        //}
    }
}