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
using Microsoft.AspNetCore.Identity;

namespace coderush.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/RFPSaleorders")]
    public class RFPSaleordersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INumberSequence _numberSequence;
        private readonly UserManager<ApplicationUser> _userManager;

        public RFPSaleordersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
                        INumberSequence numberSequence)
        {
            _context = context;
            _numberSequence = numberSequence;
            _userManager = userManager;
        }

        // GET: api/SalesOrder
        [HttpGet]
        public async Task<IActionResult> GetSalesOrder()
        {
            List<RFPSaleorder> Items = await _context.RFPSaleorder.OrderByDescending(x => x.RFPSaleorderId).ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetNotInvoicedYet()
        {
            List<RFPSaleorder> salesOrders = new List<RFPSaleorder>();
            try
            {
                List<RFPinvoice> invoices = new List<RFPinvoice>();
                invoices = await _context.RFPinvoice.ToListAsync();
                List<int> ids = new List<int>();

                foreach (var item in invoices)
                {
                    ids.Add(item.RFPSaleorderId);
                }

                salesOrders = await _context.RFPSaleorder
                    .Where(x => !ids.Contains(x.RFPSaleorderId))
                    .ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(salesOrders);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            RFPSaleorder result = await _context.RFPSaleorder
                .Where(x => x.RFPSaleorderId.Equals(id))
                .Include(x => x.SalesOrderLines)
                .FirstOrDefaultAsync();

            return Ok(result);
        }

        private void UpdateSalesOrder(int RFPSaleorderId)
        {
            try
            {
                RFPSaleorder salesOrder = new RFPSaleorder();
                salesOrder = _context.RFPSaleorder
                    .Where(x => x.RFPSaleorderId.Equals(RFPSaleorderId))
                    .FirstOrDefault();

                if (salesOrder != null)
                {
                    List<SalesOrderLine> lines = new List<SalesOrderLine>();
                    lines = _context.SalesOrderLine.Where(x => x.SalesOrderId.Equals(RFPSaleorderId)).ToList();

                    //update master data by its lines
                    salesOrder.Amount = lines.Sum(x => x.Amount);
                    salesOrder.SubTotal = lines.Sum(x => x.SubTotal);

                    salesOrder.Discount = lines.Sum(x => x.DiscountAmount);
                    salesOrder.Tax = lines.Sum(x => x.TaxAmount);

                    salesOrder.Total = salesOrder.Freight + lines.Sum(x => x.Total);

                    _context.Update(salesOrder);

                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Insert([FromBody] CrudViewModel<RFPSaleorder> payload)
        {
            RFPSaleorder salesOrder = payload.value;
            salesOrder.RFPSaleorderName = _numberSequence.GetNumberSequence("RSO");
            var user = await _userManager.GetUserAsync(HttpContext.User);
            salesOrder.UserId = user.Id;
            _context.RFPSaleorder.Add(salesOrder);
            _context.SaveChanges();
            this.UpdateSalesOrder(salesOrder.RFPSaleorderId);
            return Ok(salesOrder);
        }
        //Endpoint
        [HttpPost("[action]")]
        public async Task<IActionResult> Add([FromBody] RFPSaleorder payload)
        {
            RFPSaleorder salesOrder = payload;
            salesOrder.RFPSaleorderName = _numberSequence.GetNumberSequence("RSO");
            var user = await _userManager.GetUserAsync(HttpContext.User);
            salesOrder.UserId = user.Id;
            _context.RFPSaleorder.Add(salesOrder);
            _context.SaveChanges();
            this.UpdateSalesOrder(salesOrder.RFPSaleorderId);
            return Ok(salesOrder);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Update([FromBody] CrudViewModel<RFPSaleorder> payload)
        {
            RFPSaleorder salesOrder = payload.value;
            var user = await _userManager.GetUserAsync(HttpContext.User);
            salesOrder.UserId = user.Id;
            _context.RFPSaleorder.Update(salesOrder);
            _context.SaveChanges();
            return Ok(salesOrder);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Put([FromBody] RFPSaleorder payload)
        {
            RFPSaleorder salesOrder = payload;
            var user = await _userManager.GetUserAsync(HttpContext.User);
            salesOrder.UserId = user.Id;
            _context.RFPSaleorder.Update(salesOrder);
            _context.SaveChanges();
            return Ok(salesOrder);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody] CrudViewModel<RFPSaleorder> payload)
        {
            RFPSaleorder salesOrder = _context.RFPSaleorder
                .Where(x => x.RFPSaleorderId == (int)payload.key)
                .Include(x => x.SalesOrderLines)
                .FirstOrDefault();
            if (salesOrder.SalesOrderLines.Count > 0)
            {
                Err err = new Err
                {
                    message = "Record has Orders"
                };
                string errMsg = JsonConvert.SerializeObject(err);

                return BadRequest(err);
            }

            _context.RFPSaleorder.Remove(salesOrder);
            _context.SaveChanges();
            return Ok(salesOrder);

        }
        [HttpPost("[action]/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            RFPSaleorder salesOrder = _context.RFPSaleorder
                .Where(x => x.RFPSaleorderId == id)
                .Include(x => x.SalesOrderLines)
                .FirstOrDefault();
            if (salesOrder.SalesOrderLines.Count > 0)
            {
                Err err = new Err
                {
                    message = "Record has Orders"
                };
                string errMsg = JsonConvert.SerializeObject(err);

                return BadRequest(err);
            }
            _context.RFPSaleorder.Remove(salesOrder);
            _context.SaveChanges();
            return Ok(salesOrder);

        }
    }
}