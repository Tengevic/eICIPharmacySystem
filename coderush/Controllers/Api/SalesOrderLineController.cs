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
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace coderush.Controllers.Api
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/SalesOrderLine")]
    public class SalesOrderLineController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesOrderLineController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SalesOrderLine
        [HttpGet]
        public async Task<IActionResult> GetSalesOrderLine()
        {
            var headers = Request.Headers["SalesOrderId"];
            var headersRFP = Request.Headers["RFPSaleorderId"];
            var headersProduct = Request.Headers["ProductId"];
            int salesOrderId = Convert.ToInt32(headers);
            int productId = Convert.ToInt32(headersProduct);
            int RFPSaleorderId = Convert.ToInt32(headersRFP); ;

            if (productId != 0)
            {
                List<SalesOrderLine> Items = await _context.SalesOrderLine
                .Where(x => x.ProductId.Equals(productId))
                .ToListAsync();
                int Count = Items.Count();
                return Ok(new { Items, Count });
            } 
            else if (RFPSaleorderId != 0)
            {
                List<SalesOrderLine> Items = await _context.SalesOrderLine
                .Where(x => x.RFPSaleorderId.Equals(RFPSaleorderId))
                .ToListAsync();
                int Count = Items.Count();
                return Ok(new { Items, Count });
            }
            else
            {
                List<SalesOrderLine> Items = await _context.SalesOrderLine
                .Where(x => x.SalesOrderId.Equals(salesOrderId))
                .ToListAsync();
                int Count = Items.Count();
                return Ok(new { Items, Count });
            }
            
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetSalesOrderLineByRFPSaleOrderId([FromRoute] int id)
        {
            List<SalesOrderLine> Items = await _context.SalesOrderLine
                .Where(x => x.RFPSaleorderId == id)
                .Include(x => x.Product.UnitOfMeasure)
                .Include(x => x.Product.ProductType)
                .ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });

        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSalesOrderLineByShipmentId()
        {
            var headers = Request.Headers["ShipmentId"];
            int shipmentId = Convert.ToInt32(headers);
            Shipment shipment = await _context.Shipment.SingleOrDefaultAsync(x => x.ShipmentId.Equals(shipmentId));
            List<SalesOrderLine> Items = new List<SalesOrderLine>();
            if (shipment != null)
            {
                int salesOrderId = shipment.SalesOrderId;
                Items = await _context.SalesOrderLine
                    .Where(x => x.SalesOrderId.Equals(salesOrderId))
                    .ToListAsync();
            }
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        //[HttpGet("[action]")]
        //public async Task<IActionResult> GetSalesOrderLineByInvoiceId()
        //{
        //    var headers = Request.Headers["InvoiceId"];
        //    int invoiceId = Convert.ToInt32(headers);
        //    Invoice invoice = await _context.Invoice.SingleOrDefaultAsync(x => x.InvoiceId.Equals(invoiceId));
        //    List<SalesOrderLine> Items = new List<SalesOrderLine>();
        //    if (invoice != null)
        //    {
        //        int shipmentId = invoice.ShipmentId;
        //        Shipment shipment = await _context.Shipment.SingleOrDefaultAsync(x => x.ShipmentId.Equals(shipmentId));
        //        if (shipment != null)
        //        {
        //            int salesOrderId = shipment.SalesOrderId;
        //            Items = await _context.SalesOrderLine
        //                .Where(x => x.SalesOrderId.Equals(salesOrderId))
        //                .ToListAsync();
        //        }
        //    }
        //    int Count = Items.Count();
        //    return Ok(new { Items, Count });
        //}
        private void UpdateStock(int productId)
        {
            try
            {
                Product stock = new Product();
                stock = _context.Product
                    .Where(x => x.ProductId.Equals(productId))
                    .FirstOrDefault();

                if (stock != null)
                {
                    List<SalesOrderLine> lines = new List<SalesOrderLine>();
                    lines = _context.SalesOrderLine.Where(x => x.ProductId.Equals(productId)).ToList();
                    

                    List<GoodsRecievedNoteLine> batch = _context.GoodsRecievedNoteLine.Where(x => x.ProductId.Equals(productId)).ToList();

                    stock.TotalRecieved = batch.Sum(x => x.Quantity);

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
     

        private SalesOrderLine Recalculate(SalesOrderLine salesOrderLine)
        {
            try
            {
                salesOrderLine.Amount = salesOrderLine.Quantity * salesOrderLine.Price;
                salesOrderLine.DiscountAmount = (salesOrderLine.DiscountPercentage * salesOrderLine.Amount) / 100.0;
                salesOrderLine.SubTotal = salesOrderLine.Amount - salesOrderLine.DiscountAmount;
                salesOrderLine.TaxAmount = (salesOrderLine.TaxPercentage * salesOrderLine.SubTotal) / 100.0;
                salesOrderLine.Total = salesOrderLine.SubTotal + salesOrderLine.TaxAmount;

            }
            catch (Exception)
            {

                throw;
            }

            return salesOrderLine;
        }

        private void UpdateSalesOrder(SalesOrderLine salesOrderLine)
        {
            if(salesOrderLine.SalesOrderId != null)
            {
                try
                {
                    SalesOrder salesOrder = new SalesOrder();
                    salesOrder = _context.SalesOrder
                        .Where(x => x.SalesOrderId.Equals(salesOrderLine.SalesOrderId))
                        .FirstOrDefault();

                    if (salesOrder != null)
                    {
                        List<SalesOrderLine> lines = new List<SalesOrderLine>();
                        lines = _context.SalesOrderLine.Where(x => x.SalesOrderId.Equals(salesOrderLine.SalesOrderId)).ToList();

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
            if (salesOrderLine.RFPSaleorderId != null)
            {
                try
                {
                    RFPSaleorder salesOrder = new RFPSaleorder();
                    salesOrder = _context.RFPSaleorder
                        .Where(x => x.RFPSaleorderId.Equals(salesOrderLine.RFPSaleorderId))
                        .FirstOrDefault();

                    if (salesOrder != null)
                    {
                        List<SalesOrderLine> lines = new List<SalesOrderLine>();
                        lines = _context.SalesOrderLine.Where(x => x.RFPSaleorderId.Equals(salesOrderLine.RFPSaleorderId)).ToList();

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
                    batch.InStock = batch.Quantity - batch.Sold;

                    _context.Update(batch);

                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<SalesOrderLine> payload)
        {
            SalesOrderLine salesOrderLine = payload.value;

            // Checks if saleOrder/rfpsaleorder has been invoices
            if (salesOrderLine.SalesOrderId != null)
            {
                SalesOrder salesOrder = _context.SalesOrder
                    .Where(x => x.SalesOrderId == salesOrderLine.SalesOrderId)
                    .Include(x => x.Invoice)
                    .FirstOrDefault();
                if (salesOrder.Invoice != null)
                {
                    Err err = new Err
                    {
                        message = "This oder is already Invoiced",
                    };
                    string errMsg = JsonConvert.SerializeObject(err);

                    return BadRequest(err);
                }
            }
            if (salesOrderLine.RFPSaleorderId != null)
            {
                RFPSaleorder saleorder = _context.RFPSaleorder
                    .Where(x => x.RFPSaleorderId == salesOrderLine.RFPSaleorderId)
                    .Include(x => x.RFPinvoice)
                    .FirstOrDefault();
                    
                if (saleorder.RFPinvoice != null)
                {
                    Err err = new Err
                    {
                        message = "This oder is already Invoiced",
                    };
                    string errMsg = JsonConvert.SerializeObject(err);

                    return BadRequest(err);
                }

            }

            // check if product is in stock
            Product product = _context.Product.Find(salesOrderLine.ProductId);
            if (product.InStock < salesOrderLine.Quantity)
            {
                Err err = new Err
                {
                    message = "Drug in instock is " + product.InStock ,
                };
                string errMsg = JsonConvert.SerializeObject(err);

                return BadRequest(err);
            }
            // auto select batch 
            double Quantity = salesOrderLine.Quantity;
            double remain = salesOrderLine.Quantity;
            do {
                double sold;
                
                GoodsRecievedNoteLine batch = _context.GoodsRecievedNoteLine
                  .Where(x => x.ProductId == salesOrderLine.ProductId)
                  .Where(x => x.InStock > 0)
                  .OrderBy(x => x.ExpiryDate)
                  .First();

                if (batch.InStock > Quantity)
                {
                    sold = remain;
                    Quantity = Quantity - sold;
                }
                else
                {
                    sold = batch.InStock;
                    Quantity = Quantity - sold;
                    remain = Quantity;
                }
                SalesOrderLine orderLine = new SalesOrderLine
                {
                    ProductId = salesOrderLine.ProductId,
                    SalesOrderId = salesOrderLine.SalesOrderId,
                    RFPSaleorderId = salesOrderLine.RFPSaleorderId,
                    Quantity = salesOrderLine.Quantity,
                    Amount = salesOrderLine.Amount,
                    Description = salesOrderLine.Description,
                    DiscountPercentage = salesOrderLine.DiscountPercentage,
                    GoodsRecievedNoteLineId = salesOrderLine.GoodsRecievedNoteLineId,
                    TaxPercentage = salesOrderLine.TaxPercentage,
                    Price = salesOrderLine.Price,
                    
                };
                orderLine.Quantity = sold;
                orderLine.GoodsRecievedNoteLineId = batch.GoodsRecievedNoteLineId;
                orderLine = this.Recalculate(orderLine);
                _context.SalesOrderLine.Add(orderLine);
                _context.SaveChanges();
                salesOrderLine = orderLine;
                this.UpdateSalesOrder(salesOrderLine);
                this.UpdateStock(salesOrderLine.ProductId);
                this.UpdateBatch(salesOrderLine.GoodsRecievedNoteLineId);
                
            } while (Quantity > 0);
            return Ok(salesOrderLine);

        }
        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<SalesOrderLine> payload)
        {
            SalesOrderLine salesOrderLine = payload.value;
            SalesOrderLine OrderLine = _context.SalesOrderLine
                .Where(x => x.SalesOrderLineId == (int)payload.key)
                .Include(x => x.SalesOrder.Invoice)
                .Include(x => x.RFPSaleorder.RFPinvoice)
                .FirstOrDefault();

            if(salesOrderLine.SalesOrderId != null)
            {
                if (OrderLine.SalesOrder.Invoice != null)
                {
                    Err err = new Err
                    {
                        message = "This oder is already Invoiced",
                    };
                    string errMsg = JsonConvert.SerializeObject(err);

                    return BadRequest(err);
                }
            }
            if(salesOrderLine.RFPSaleorderId != null)
            {
                if (OrderLine.RFPSaleorder.RFPinvoice != null)
                {
                    Err err = new Err
                    {
                        message = "This oder is already Invoiced",
                    };
                    string errMsg = JsonConvert.SerializeObject(err);

                    return BadRequest(err);
                }

            }

            GoodsRecievedNoteLine batch = _context.GoodsRecievedNoteLine
                .Where(x => x.GoodsRecievedNoteLineId == salesOrderLine.GoodsRecievedNoteLineId)
                .FirstOrDefault();

            double instock = OrderLine.Quantity + batch.InStock;
            if (instock < salesOrderLine.Quantity)
            {
                Err err = new Err
                {
                    message = "Units in batch " + batch.InStock,
                };
                string errMsg = JsonConvert.SerializeObject(err);

                return BadRequest(err);
            }
            salesOrderLine.GoodsRecievedNoteLineId = batch.GoodsRecievedNoteLineId;
            salesOrderLine = this.Recalculate(salesOrderLine);
            OrderLine = salesOrderLine;
            _context.SalesOrderLine.Update(OrderLine);
            _context.SaveChanges();
            this.UpdateSalesOrder(salesOrderLine);
            this.UpdateStock(salesOrderLine.ProductId);
            this.UpdateBatch(salesOrderLine.GoodsRecievedNoteLineId);
            return Ok(salesOrderLine);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<SalesOrderLine> payload)
        {
            SalesOrderLine salesOrderLine = _context.SalesOrderLine
                .Where(x => x.SalesOrderLineId == (int)payload.key)
                .FirstOrDefault();
            if (salesOrderLine.SalesOrderId != null)
            {
                SalesOrder salesOrder = _context.SalesOrder
                                .Where(x => x.SalesOrderId == salesOrderLine.SalesOrderId)
                                .Include(x => x.Invoice)
                                .FirstOrDefault();
                if (salesOrder.Invoice != null)
                {
                    Err err = new Err
                    {
                        message = "This oder is already Invoiced",
                    };
                    string errMsg = JsonConvert.SerializeObject(err);

                    return BadRequest(err);
                }
            }
            if (salesOrderLine.RFPSaleorderId != null)
            {
                RFPSaleorder salesOrder = _context.RFPSaleorder
                                .Where(x => x.RFPSaleorderId == salesOrderLine.RFPSaleorderId)
                                .Include(x => x.RFPinvoice)
                                .FirstOrDefault();
                if (salesOrder.RFPinvoice != null)
                {
                    Err err = new Err
                    {
                        message = "This oder is already Invoiced",
                    };
                    string errMsg = JsonConvert.SerializeObject(err);

                    return BadRequest(err);
                }
            }
            _context.SalesOrderLine.Remove(salesOrderLine);
            _context.SaveChanges();
            this.UpdateSalesOrder(salesOrderLine);
            this.UpdateStock(salesOrderLine.ProductId);
            this.UpdateBatch(salesOrderLine.GoodsRecievedNoteLineId);
            return Ok(salesOrderLine);

        }
    }
}