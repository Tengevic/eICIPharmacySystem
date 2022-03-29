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
    [Route("api/ClinicalTrialsStocks")]
    public class ClinicalTrialsStocksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClinicalTrialsStocksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ClinicalTrialsStocks
        [HttpGet]
        public async Task<IActionResult> GetStock()
        {
            List<ClinicalTrialsStock> Items = await _context.ClinicalTrialsStock.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }
        private void UpdateStock(int productId)
        {
            try
            {
                ClinicalTrialsStock stock = new ClinicalTrialsStock();
                stock = _context.ClinicalTrialsStock
                    .Where(x => x.ClinicalTrialsProductsId.Equals(productId))
                    .FirstOrDefault();

                if (stock != null)
                {
                    List<ClinicalTrialsDonationLine> line = new List<ClinicalTrialsDonationLine>();
                    line = _context.ClinicalTrialsDonationLine.Where(x => x.ClinicalTrialsProductsId.Equals(productId)).ToList();

                    stock.TotalRecieved = line.Sum(x => x.Quantity);

                    List<ClinicalTrialsSalesLine> lines = new List<ClinicalTrialsSalesLine>();
                    lines = _context.ClinicalTrialsSalesLine.Where(x => x.ClinicalTrialsProductsId.Equals(productId)).ToList();

                    stock.TotalSales = lines.Sum(x => x.Quantity);


                    if (stock.TotalRecieved < stock.TotalSales)
                    {
                        stock.Deficit = stock.TotalSales - stock.TotalRecieved;
                        stock.InStock = 0;
                    }
                    else
                    {
                        stock.InStock = stock.TotalRecieved - stock.TotalSales;
                        stock.Deficit = 0;
                    }


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
        public IActionResult Insert([FromBody]CrudViewModel<ClinicalTrialsStock> payload)
        {

            ClinicalTrialsStock stock = payload.value;
            _context.ClinicalTrialsStock.Add(stock);
            _context.SaveChanges();
            this.UpdateStock(stock.ClinicalTrialsProductsId);
            return Ok(stock);
        }

        // GET: api/ClinicalTrialsStocks/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetClinicalTrialsStock([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var clinicalTrialsStock = await _context.ClinicalTrialsStock.SingleOrDefaultAsync(m => m.ClinicalTrialsStockId == id);

        //    if (clinicalTrialsStock == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(clinicalTrialsStock);
        //}

        //// PUT: api/ClinicalTrialsStocks/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutClinicalTrialsStock([FromRoute] int id, [FromBody] ClinicalTrialsStock clinicalTrialsStock)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != clinicalTrialsStock.ClinicalTrialsStockId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(clinicalTrialsStock).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ClinicalTrialsStockExists(id))
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

        //// POST: api/ClinicalTrialsStocks
        //[HttpPost]
        //public async Task<IActionResult> PostClinicalTrialsStock([FromBody] ClinicalTrialsStock clinicalTrialsStock)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.ClinicalTrialsStock.Add(clinicalTrialsStock);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetClinicalTrialsStock", new { id = clinicalTrialsStock.ClinicalTrialsStockId }, clinicalTrialsStock);
        //}

        //// DELETE: api/ClinicalTrialsStocks/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteClinicalTrialsStock([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var clinicalTrialsStock = await _context.ClinicalTrialsStock.SingleOrDefaultAsync(m => m.ClinicalTrialsStockId == id);
        //    if (clinicalTrialsStock == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.ClinicalTrialsStock.Remove(clinicalTrialsStock);
        //    await _context.SaveChangesAsync();

        //    return Ok(clinicalTrialsStock);
        //}

        //private bool ClinicalTrialsStockExists(int id)
        //{
        //    return _context.ClinicalTrialsStock.Any(e => e.ClinicalTrialsStockId == id);
        //}
    }
}