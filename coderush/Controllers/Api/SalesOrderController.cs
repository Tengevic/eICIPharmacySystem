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
using Microsoft.AspNetCore.Identity;

namespace coderush.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/SalesOrder")]
    public class SalesOrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INumberSequence _numberSequence;
        private readonly UserManager<ApplicationUser> _userManager;

        public SalesOrderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
                        INumberSequence numberSequence)
        {
            _context = context;
            _numberSequence = numberSequence;
            _userManager = userManager;
        }

        // GET: api/SalesOrder
        [HttpGet]
        //public async Task<IActionResult> GetSalesOrder()
        //{
        //    List<SalesOrder> Items = await _context.SalesOrder.OrderByDescending(x => x.SalesOrderId).ToListAsync();
        //    int Count = Items.Count();
        //    return Ok(new { Items, Count });
        //}
        public async Task<IActionResult> GetSalesOrder() 
        {
            var headers = Request.Headers["Invoiced"];
            bool Invoiced = Convert.ToBoolean(headers);

            List<SalesOrder> Items = new List<SalesOrder>();
            try
            {
                List<Invoice> invoice = new List<Invoice>();
                invoice = await _context.Invoice.ToListAsync();
                List<int> ids = new List<int>();

                foreach (var item in invoice)
                {
                    ids.Add(item.SalesOrderId);
                }
                if (Invoiced)
                {
                    Items = await _context.SalesOrder
                    .Where(x => ids.Contains(x.SalesOrderId))
                    .OrderByDescending(x => x.SalesOrderId)
                    .ToListAsync();
                }
                else
                {
                    Items = await _context.SalesOrder
                    .Where(x => !ids.Contains(x.SalesOrderId))
                    .OrderByDescending(x => x.SalesOrderId)
                    .ToListAsync();
                }
                
            }
            catch (Exception)
            {

                throw;
            }
            int Count = Items.Count();
            return Ok(new { Items, Count });

        }
        [HttpGet("[action]")]
        public async Task<IActionResult> Get()
        {
            List<SalesOrder> Items = await _context.SalesOrder.OrderByDescending(x => x.SalesOrderId).ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }
        //[HttpGet("[action]")]
        //public async Task<IActionResult> GetNotShippedYet()
        //{
        //    List<SalesOrder> salesOrders = new List<SalesOrder>();
        //    try
        //    {
        //        List<Shipment> shipments = new List<Shipment>();
        //        shipments = await _context.Shipment.ToListAsync();
        //        List<int> ids = new List<int>();

        //        foreach (var item in shipments)
        //        {
        //            ids.Add(item.SalesOrderId);
        //        }

        //        salesOrders = await _context.SalesOrder
        //            .Where(x => !ids.Contains(x.SalesOrderId))
        //            .ToListAsync();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return Ok(salesOrders);
        //}
        [HttpGet("[action]")]
        public async Task<IActionResult> GetNotInvoicedYet()
        {
            List<SalesOrder> salesOrders = new List<SalesOrder>();
            try
            {
                List<Invoice> invoices = new List<Invoice>();
                invoices = await _context.Invoice.ToListAsync();
                List<int> ids = new List<int>();

                foreach (var item in invoices)
                {
                    ids.Add(item.SalesOrderId);
                }

                salesOrders = await _context.SalesOrder
                    .Where(x => !ids.Contains(x.SalesOrderId))
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
            SalesOrder result = await _context.SalesOrder
                .Where(x => x.SalesOrderId.Equals(id))
                .Include(x => x.SalesOrderLines)
                .FirstOrDefaultAsync();

            return Ok(result);
        }

        private void UpdateSalesOrder(int salesOrderId)
        {
            try
            {
                SalesOrder salesOrder = new SalesOrder();
                salesOrder = _context.SalesOrder
                    .Where(x => x.SalesOrderId.Equals(salesOrderId))
                    .FirstOrDefault();

                if (salesOrder != null)
                {
                    List<SalesOrderLine> lines = new List<SalesOrderLine>();
                    lines = _context.SalesOrderLine.Where(x => x.SalesOrderId.Equals(salesOrderId)).ToList();

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
        public async Task<IActionResult> Insert([FromBody]CrudViewModel<SalesOrder> payload)
        {
            SalesOrder salesOrder = payload.value;
            salesOrder.SalesOrderName = _numberSequence.GetNumberSequence("SO");
            
            if(salesOrder.PrescriptionId > 0)
            {
                Prescription prescription = _context.Prescription.Find(salesOrder.PrescriptionId);

                if (!prescription.Approved)
                {
                    Err err = new Err
                    {
                        message = "Prescription is not approved"
                    };
                    string errMsg = JsonConvert.SerializeObject(err);

                    return BadRequest(err);
                }
            }
            var user = await _userManager.GetUserAsync(HttpContext.User);
            salesOrder.UserId = user.Id;
            _context.SalesOrder.Add(salesOrder);
            _context.SaveChanges();
            this.UpdateSalesOrder(salesOrder.SalesOrderId);
            return Ok(salesOrder);
        }
        //Endpoint
        [HttpPost("[action]")]
        public async Task<IActionResult> Add([FromBody] SalesOrder payload)
        {
            SalesOrder salesOrder = payload;
            salesOrder.SalesOrderName = _numberSequence.GetNumberSequence("SO");
            if (salesOrder.PrescriptionId == null)
            {
                salesOrder.PrescriptionId = 0;
            }
            else
            {
                Prescription prescription = _context.Prescription.Find(salesOrder.PrescriptionId);

                if (!prescription.Approved)
                {
                    Err err = new Err
                    {
                        message = "Prescription is not approved"
                    };
                    string errMsg = JsonConvert.SerializeObject(err);

                    return BadRequest(err);
                }
            }
            var user = await _userManager.GetUserAsync(HttpContext.User);
            salesOrder.UserId = user.Id;
            _context.SalesOrder.Add(salesOrder);
            _context.SaveChanges();
            this.UpdateSalesOrder(salesOrder.SalesOrderId);
            return Ok(salesOrder);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddByPrescription([FromBody] SalesOrder payload)
        {
            SalesOrder salesOrder = payload;

            Prescription prescription = _context.Prescription.Find(salesOrder.PrescriptionId);

            if (!prescription.Approved)
            {
                Err err = new Err
                {
                    message = "Prescription is not approved"
                };
                string errMsg = JsonConvert.SerializeObject(err);

                return BadRequest(err);
            }

            SalesOrder order = _context.SalesOrder
                .Where(x => x.PrescriptionId == salesOrder.PrescriptionId)
                .FirstOrDefault();

            if(order == null)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                salesOrder.UserId = user.Id;
                salesOrder.SaleDate = DateTime.Now;
                salesOrder.SalesOrderName = _numberSequence.GetNumberSequence("SO");
                _context.SalesOrder.Add(salesOrder);
                _context.SaveChanges();
                this.UpdateSalesOrder(salesOrder.SalesOrderId);
            }
            else
            {
                salesOrder = order;
            }
           
            return Ok(salesOrder);
        }



        [HttpPost("[action]")]
        public async Task<IActionResult> Update([FromBody]CrudViewModel<SalesOrder> payload)
        {
            SalesOrder salesOrder = payload.value;
            if (salesOrder.PrescriptionId == null)
            {
                salesOrder.PrescriptionId = 0;
            }
            var user = await _userManager.GetUserAsync(HttpContext.User);
            salesOrder.UserId = user.Id;
            _context.SalesOrder.Update(salesOrder);
            _context.SaveChanges();
            return Ok(salesOrder);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Put([FromBody] SalesOrder payload)
        {
            SalesOrder salesOrder = payload;
            if (salesOrder.PrescriptionId == null)
            {
                salesOrder.PrescriptionId = 0;
            }
            var user = await _userManager.GetUserAsync(HttpContext.User);
            salesOrder.UserId = user.Id;
            _context.SalesOrder.Update(salesOrder);
            _context.SaveChanges();
            return Ok(salesOrder);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<SalesOrder> payload)
        {
            SalesOrder salesOrder = _context.SalesOrder
                .Where(x => x.SalesOrderId == (int)payload.key)
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

            _context.SalesOrder.Remove(salesOrder);
            _context.SaveChanges();
            return Ok(salesOrder);

        }
        [HttpPost("[action]/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            SalesOrder salesOrder = _context.SalesOrder
                .Where(x => x.SalesOrderId == id)
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
            _context.SalesOrder.Remove(salesOrder);
            _context.SaveChanges();
            return Ok(salesOrder);

        }
    }
}