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
using coderush.Models.SyncfusionViewModels;
using Newtonsoft.Json;

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
            var headers = Request.Headers["GoodsRecievedNoteLineId"];
            int GoodsRecievedNoteLineId = Convert.ToInt32(headers);

            List<stockNumber> Items = await _context.stockNumber.Where(x =>x.GoodsRecievedNoteLineId == GoodsRecievedNoteLineId).ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Insert([FromBody] CrudViewModel<stockNumber> payload)
        {
            stockNumber stockNumber = payload.value;
            GoodsRecievedNoteLine batch = _context.GoodsRecievedNoteLine.Find(stockNumber.GoodsRecievedNoteLineId);
            if(stockNumber.subtract > batch.InStock)
            {
                Err err = new Err
                {
                    message = "Subtract amount is greater than amount in stock",
                };
                string errMsg = JsonConvert.SerializeObject(err);

                return BadRequest(err);
            }
            stockNumber.quantity = stockNumber.Add - stockNumber.subtract;
            var user = await _userManager.GetUserAsync(HttpContext.User);
            UserProfile userProfile =  _context.UserProfile.Where(x => x.ApplicationUserId == user.Id).FirstOrDefault();
            stockNumber.UserId = user.UserName;
            _context.stockNumber.Add(stockNumber);
            _context.SaveChanges();
            UpdateBatch(stockNumber.GoodsRecievedNoteLineId);
            return Ok(stockNumber);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Update([FromBody] CrudViewModel<stockNumber> payload)
        {
            stockNumber stockNumber = payload.value;
            stockNumber.quantity = stockNumber.Add - stockNumber.subtract;
            var user = await _userManager.GetUserAsync(HttpContext.User);
            stockNumber.UserId = user.Id;
            _context.stockNumber.Update(stockNumber);
            _context.SaveChanges();
            UpdateBatch(stockNumber.GoodsRecievedNoteLineId);
            return Ok(stockNumber);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody] CrudViewModel<stockNumber> payload)
        {
            stockNumber stockNumber = _context.stockNumber
                .Where(x => x.stockNumberId == (int)payload.key)
                .FirstOrDefault();
            GoodsRecievedNoteLine batch = _context.GoodsRecievedNoteLine.Find(stockNumber.GoodsRecievedNoteLineId);
            if (stockNumber.Add > batch.InStock)
            {
                Err err = new Err
                {
                    message = "changed amount may already be in use",
                };
                string errMsg = JsonConvert.SerializeObject(err);

                return BadRequest(err);
            }
            _context.stockNumber.Remove(stockNumber);
            _context.SaveChanges();
            UpdateBatch(stockNumber.GoodsRecievedNoteLineId);
            UpdateStock(stockNumber.GoodsRecievedNoteLineId);
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
                    batch.InStock = batch.Quantity - batch.Sold - batch.Expired + batch.changestock;

                    _context.Update(batch);

                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void UpdateStock(int batchId)
        {
            try
            {
                GoodsRecievedNoteLine noteLine = _context.GoodsRecievedNoteLine.Find(batchId);
                int productId = noteLine.ProductId;
                Product stock = new Product();
                stock = _context.Product
                    .Where(x => x.ProductId.Equals(productId))
                    .FirstOrDefault();

                if (stock != null)
                {

                    List<GoodsRecievedNoteLine> batch = _context.GoodsRecievedNoteLine.Where(x => x.ProductId.Equals(productId)).ToList();

                    stock.TotalRecieved = batch.Sum(x => x.Quantity);

                    stock.TotalSales = batch.Sum(x => x.Sold);
                    stock.InStock = batch.Sum(x => x.InStock);

                    _context.Update(stock);

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