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
            var monthsHeaders = Request.Headers["months"];
            var headersProduct = Request.Headers["ProductId"];
            int GoodsReceivedNoteId = Convert.ToInt32(headers);
            int productId = Convert.ToInt32(headersProduct);
            int months = Convert.ToInt32(monthsHeaders);

            if (months != 0)
            {
                DateTime current = DateTime.Now;
                List<GoodsRecievedNoteLine> drugs = await _context.GoodsRecievedNoteLine
                                .Where(x => x.InStock > 0)
                                .ToListAsync();
                List<GoodsRecievedNoteLine> Items = new List<GoodsRecievedNoteLine>();

                foreach (GoodsRecievedNoteLine drug in drugs)
                {
                    Double TotalDays = (drug.ExpiryDate - current).TotalDays;

                    if (TotalDays <= 90)
                    {
                        Items.Add(drug);
                    }
                }
                int Count = Items.Count();
                return Ok(new { Items, Count });
            }
            else if (productId != 0)
            {
                List<GoodsRecievedNoteLine> Items = await _context.GoodsRecievedNoteLine
                .Where(x => x.ProductId.Equals(productId))
                .ToListAsync();

                int Count = Items.Count();
                return Ok(new { Items, Count });
            }
            else if (GoodsReceivedNoteId != 0)
            {
                List<GoodsRecievedNoteLine> Items = await _context.GoodsRecievedNoteLine
                .Where(x => x.GoodsReceivedNoteId.Equals(GoodsReceivedNoteId))
                .ToListAsync();
                int Count = Items.Count();
                return Ok(new { Items, Count });
            }
            else
            {
                List<GoodsRecievedNoteLine> Items = await _context.GoodsRecievedNoteLine
                   .ToListAsync();
                int Count = Items.Count();
                return Ok(new { Items, Count });
            }
        }
        private void UpdateStock(GoodsRecievedNoteLine goodsRecievedNoteLine)
        {
            try
            {
                Product stock = new Product();
                stock = _context.Product
                    .Where(x => x.ProductId.Equals(goodsRecievedNoteLine.ProductId))
                    .FirstOrDefault();

                if (stock != null)
                {
                    List<GoodsRecievedNoteLine> lines = new List<GoodsRecievedNoteLine>();
                    lines = _context.GoodsRecievedNoteLine.Where(x => x.ProductId.Equals(goodsRecievedNoteLine.ProductId)).ToList();

                    stock.TotalRecieved = lines.Sum(x => x.Quantity);
                    stock.ExpiredStock = lines.Sum(x => x.Expired);
                   
                    if (stock.TotalRecieved < stock.TotalSales)
                    {
                        stock.Deficit = stock.TotalSales - stock.TotalRecieved;
                        stock.InStock = 0;
                    }
                    else
                    {
                        stock.InStock = stock.TotalRecieved - stock.TotalSales - stock.ExpiredStock;
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
        private void UpdateBatch(int batchId)
        {
            try
            {
                GoodsRecievedNoteLine batch = _context.GoodsRecievedNoteLine.Find(batchId);
                if (batch != null)
                {
                    List<SalesOrderLine> lines = new List<SalesOrderLine>();
                    lines = _context.SalesOrderLine.Where(x => x.GoodsRecievedNoteLineId.Equals(batch.GoodsRecievedNoteLineId)).ToList();

                    batch.Sold = lines.Sum(x => x.Quantity);
                    batch.InStock = batch.Quantity - batch.Sold - batch.Expired;

                    _context.Update(batch);

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
        public IActionResult Insert([FromBody] CrudViewModel<GoodsRecievedNoteLine> payload)
        {
            GoodsRecievedNoteLine goodsRecievedNoteLine = payload.value;
            DateTime current = DateTime.Now;
            double totaldays = (goodsRecievedNoteLine.ExpiryDate - current).TotalDays;

            if (goodsRecievedNoteLine.ManufareDate > current)
            {
                Err err = new Err
                {
                    message = "Invalid Manufacture date"
                };
                string errMsg = JsonConvert.SerializeObject(err);

                return BadRequest(err);
            }

            if (totaldays < 360)
            {
                Err err = new Err
               {

                    message = "Drug will expire less than one year"
                };
                string errMsg = JsonConvert.SerializeObject(err);

                return BadRequest(err);
            }

            goodsRecievedNoteLine.InStock = goodsRecievedNoteLine.Quantity;
            _context.GoodsRecievedNoteLine.Add(goodsRecievedNoteLine);
            _context.SaveChanges();
            this.UpdateStock(goodsRecievedNoteLine);

            return Ok(goodsRecievedNoteLine);
        }
        // PUT: api/GoodsRecievedNoteLines
        [HttpPost("[action]")]
        public IActionResult Update([FromBody] CrudViewModel<GoodsRecievedNoteLine> payload)
        {
            GoodsRecievedNoteLine goodsRecievedNoteLine = payload.value;

            DateTime current = DateTime.Now;
            double totaldays = (goodsRecievedNoteLine.ExpiryDate - current).TotalDays;
            if (goodsRecievedNoteLine.ManufareDate > current)
            {
                Err err = new Err
                {
                    message = "Invalid Manufacture date"
                };
                string errMsg = JsonConvert.SerializeObject(err);

                return BadRequest(err);
            }
            if (totaldays < 360)
            {
                Err err = new Err
                {
                    message = "Drug will expire less than one year"
                };
                string errMsg = JsonConvert.SerializeObject(err);

                return BadRequest(err);
            }
          
            goodsRecievedNoteLine.InStock = goodsRecievedNoteLine.Quantity;
            _context.GoodsRecievedNoteLine.Update(goodsRecievedNoteLine);
            _context.SaveChanges();
            this.UpdateStock(goodsRecievedNoteLine);
            this.UpdateBatch(goodsRecievedNoteLine.GoodsRecievedNoteLineId);

            return Ok(goodsRecievedNoteLine);
        }
        [HttpPost("[action]")]
        public IActionResult Expired([FromBody] CrudViewModel<GoodsRecievedNoteLine> payload)
        {
            GoodsRecievedNoteLine goodsRecievedNoteLine = payload.value;

          

            if (goodsRecievedNoteLine.InStock < goodsRecievedNoteLine.Expired)
            {
                Err err = new Err
                {
                    message = "Expired Stock cannot be more than stock"
                };
                string errMsg = JsonConvert.SerializeObject(err);

                return BadRequest(err);
            }
            if (goodsRecievedNoteLine.Dispose)
            {
                goodsRecievedNoteLine.Expired = goodsRecievedNoteLine.InStock;
                goodsRecievedNoteLine.InStock = 0;
            }
        
            _context.GoodsRecievedNoteLine.Update(goodsRecievedNoteLine);
            _context.SaveChanges();
            this.UpdateStock(goodsRecievedNoteLine);
            this.UpdateBatch(goodsRecievedNoteLine.GoodsRecievedNoteLineId);

            return Ok(goodsRecievedNoteLine);
        }


        // DELETE: api/GoodsRecievedNoteLines/5
        [HttpPost("[action]")]
        public IActionResult Remove([FromBody] CrudViewModel<GoodsRecievedNoteLine> payload)
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
        [HttpGet("[action]")]
        public async Task<IActionResult> GetInstock()
        {
            List<GoodsRecievedNoteLine> result = await _context.GoodsRecievedNoteLine
                .Where(x => x.InStock != 0)
                .ToListAsync();

            return Ok(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetExpired()
        {
            List<GoodsRecievedNoteLine> goodsRecievedNoteLine = await _context.GoodsRecievedNoteLine
                                                            .Where(x => x.InStock > 0)
                                                            .ToListAsync();
            DateTime current = DateTime.Now;
            ExpiredDrugs expiredDrugs = new ExpiredDrugs()
            {
                one = 0,
                two = 0,
                three = 0
            };

            foreach (GoodsRecievedNoteLine drug in goodsRecievedNoteLine)
            {
                Double months = (drug.ExpiryDate - current).TotalDays;

                if (months < 30)
                {
                    expiredDrugs.one = expiredDrugs.one + 1;
                }
                else if (months < 60 )
                {
                    expiredDrugs.two = expiredDrugs.two + 1;
                }
                else if(months < 90 )
                {
                    expiredDrugs.three = expiredDrugs.three + 1;

                }
            }

            return Ok(expiredDrugs);
        }
        //api/GoodsRecievedNoteLines/GetByProductId
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetByProductId(int id)
        {
            List<GoodsRecievedNoteLine> result = await _context.GoodsRecievedNoteLine
                .Where(x => x.ProductId.Equals(id))
                .Where(x =>x.InStock > 0)
                .ToListAsync();

            return Ok(result);

        }
    }


    public class Err
    {
        public string message { get; set; }
    }
    public class ExpiredDrugs
    {
        public int one { get; set; }
        public int two { get; set; }
        public int three { get; set; }
    }

}