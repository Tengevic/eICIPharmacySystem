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
using Newtonsoft.Json;

namespace coderush.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/GoodsRecievedNoteLines")]
    public class GoodsRecievedNoteLinesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GoodsRecievedNoteLinesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/GoodsRecievedNoteLines
        [HttpGet]
        public async Task<IActionResult> GetGoodsRecievedNoteLines()
        {
            var headers = Request.Headers["GoodsReceivedNoteId"];
            var headersProduct = Request.Headers["ProductId"];
            int GoodsReceivedNoteId = Convert.ToInt32(headers);
            int productId = Convert.ToInt32(headersProduct);



            if (productId != 0)
            {
                List<GoodsRecievedNoteLine> Items = await _context.GoodsRecievedNoteLine
                .Where(x => x.ProductId.Equals(productId))
                .ToListAsync();

                int Count = Items.Count();
                return Ok(new { Items, Count });
            }
            else
            {
                List<GoodsRecievedNoteLine> Items = await _context.GoodsRecievedNoteLine
                .Where(x => x.GoodsReceivedNoteId.Equals(GoodsReceivedNoteId))
                .ToListAsync();
                int Count = Items.Count();
                return Ok(new { Items, Count });
            }
            
        }
        private void UpdateStock(GoodsRecievedNoteLine goodsRecievedNoteLine)
        {
            try
            {
                Stock stock = new Stock();
                stock = _context.Stock
                    .Where(x => x.ProductId.Equals(goodsRecievedNoteLine.ProductId))
                    .FirstOrDefault();

                if (stock != null)
                {
                    List<GoodsRecievedNoteLine> lines = new List<GoodsRecievedNoteLine>();
                    lines = _context.GoodsRecievedNoteLine.Where(x => x.ProductId.Equals(goodsRecievedNoteLine.ProductId)).ToList();

                    stock.TotalRecieved = lines.Sum(x => x.Quantity);

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
        public IActionResult Insert([FromBody]CrudViewModel<GoodsRecievedNoteLine> payload)
        {
            GoodsRecievedNoteLine goodsRecievedNoteLine = payload.value;
            DateTime current = DateTime.Now;
            double totaldays = (goodsRecievedNoteLine.ExpiryDate - current).TotalDays;
            
            if (totaldays > 360)
            {
                _context.GoodsRecievedNoteLine.Add(goodsRecievedNoteLine);
                _context.SaveChanges();
                this.UpdateStock(goodsRecievedNoteLine);
            } else if (totaldays< 360) 
            {
                Err err = new Err
                {

                    message = "Drug will expire less than one year"
                };
                string errMsg = JsonConvert.SerializeObject(err);
                
                return  BadRequest(err);

            }
            
            return Ok(goodsRecievedNoteLine);
        }
        // PUT: api/GoodsRecievedNoteLines
        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<GoodsRecievedNoteLine> payload)
        {
            GoodsRecievedNoteLine goodsRecievedNoteLine = payload.value;
            DateTime current = DateTime.Now;
            double totaldays = (goodsRecievedNoteLine.ExpiryDate - current).TotalDays;

            if (totaldays > 360)
            {
                _context.GoodsRecievedNoteLine.Update(goodsRecievedNoteLine);
                _context.SaveChanges();
                this.UpdateStock(goodsRecievedNoteLine);
            }
            else if (totaldays < 360)
            {
                Err err = new Err
                {
                    message = "Drug will expire less than one year"
                };
                string errMsg = JsonConvert.SerializeObject(err);

                return BadRequest(err);

            }
           
            return Ok(goodsRecievedNoteLine);
        }


        // DELETE: api/GoodsRecievedNoteLines/5
        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<GoodsRecievedNoteLine> payload)
        {
            GoodsRecievedNoteLine goodsRecievedNoteLine = _context.GoodsRecievedNoteLine
                .Where(x => x.GoodsRecievedNoteLineId == (int)payload.key)
                .FirstOrDefault();
            _context.GoodsRecievedNoteLine.Remove(goodsRecievedNoteLine);
            _context.SaveChanges();
            this.UpdateStock(goodsRecievedNoteLine);
            return Ok(goodsRecievedNoteLine);

        }

        public async Task<IActionResult> GetById(int id)
        {
            GoodsRecievedNoteLine result = await _context.GoodsRecievedNoteLine
                .Where(x => x.ProductId.Equals(id))
                .FirstOrDefaultAsync();

            return Ok(result);
        }
    }
    public class Err
    {
        public string message { get; set; }
    }
}