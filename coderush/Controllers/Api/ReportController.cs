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
using coderush.Models.Eici_models;

namespace coderush.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Report")]
    public class ReportController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)             
        {
            _context = context;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> SalesVSPurchase()
        {
            List<SalesVsPurchaseVm> Items = new List<SalesVsPurchaseVm>();

            List<Product> products = await _context.Product.ToListAsync();

            foreach(Product product in products)
            {
                SalesVsPurchaseVm salesVsPurchaseVm = new SalesVsPurchaseVm();

                salesVsPurchaseVm.ProductName = product.ProductName;

                List<SalesOrderLine> salesOrderLines = await _context.SalesOrderLine
                                                .Where(x => x.ProductId == product.ProductId)
                                                .ToListAsync();
                salesVsPurchaseVm.TotalUnitsSold = salesOrderLines.Sum(x => x.Quantity);
                salesVsPurchaseVm.TotalSales = salesOrderLines.Sum(x => x.Total);

                List<PurchaseOrderLine> purchaseOrderLines = await _context.PurchaseOrderLine
                                                .Where(x => x.ProductId == product.ProductId)
                                                .ToListAsync();
                salesVsPurchaseVm.TotalUnitsPurchased = purchaseOrderLines.Sum(x => x.Quantity);
                salesVsPurchaseVm.TotalPurchase = purchaseOrderLines.Sum(x => x.Total);

                List<GoodsRecievedNoteLine> goodsRecievedNoteLines = await _context.GoodsRecievedNoteLine
                                                  .Where(x => x.ProductId == product.ProductId)
                                                .ToListAsync();
                salesVsPurchaseVm.TotalUnitsRecieved = goodsRecievedNoteLines.Sum(x => x.Quantity);

                Items.Add(salesVsPurchaseVm);
            }
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> SalesVSPurchaseMonth()
        {
            List<SalesVsPurchaseVm> Items = new List<SalesVsPurchaseVm>();

            List<Product> products = await _context.Product.ToListAsync();

            var salesOrderLine = from p in _context.SalesOrderLine
                                 select p;
            var purchaseOrderLine = from p in _context.PurchaseOrderLine
                                    select p;
            var goodsRecievedNoteLine = from p in _context.GoodsRecievedNoteLine 
                                    select p;
            salesOrderLine = salesOrderLine.Where(x => x.SalesOrder.SaleDate.Month == 6);
            salesOrderLine = salesOrderLine.Where(x => x.SalesOrder.SaleDate.Year == 2020);
            purchaseOrderLine = purchaseOrderLine.Where(x => x.PurchaseOrder.OrderDate.Month == 6);
            purchaseOrderLine = purchaseOrderLine.Where(x => x.PurchaseOrder.OrderDate.Year == 2020);
            goodsRecievedNoteLine = goodsRecievedNoteLine.Where(x => x.GoodsReceivedNote.GRNDate.Month == 6);
            goodsRecievedNoteLine = goodsRecievedNoteLine.Where(x => x.GoodsReceivedNote.GRNDate.Year == 2020);


            foreach (Product product in products)
            {
                SalesVsPurchaseVm salesVsPurchaseVm = new SalesVsPurchaseVm();

                salesVsPurchaseVm.ProductName = product.ProductName;

                List<SalesOrderLine> salesOrderLines = await salesOrderLine
                                                .Where(x => x.ProductId == product.ProductId)
                                                .ToListAsync();
                salesVsPurchaseVm.TotalUnitsSold = salesOrderLines.Sum(x => x.Quantity);
                salesVsPurchaseVm.TotalSales = salesOrderLines.Sum(x => x.Total);

                List<PurchaseOrderLine> purchaseOrderLines = await purchaseOrderLine
                                                .Where(x => x.ProductId == product.ProductId)
                                                .ToListAsync();
                salesVsPurchaseVm.TotalUnitsPurchased = purchaseOrderLines.Sum(x => x.Quantity);
                salesVsPurchaseVm.TotalPurchase = purchaseOrderLines.Sum(x => x.Total);

                List<GoodsRecievedNoteLine> goodsRecievedNoteLines = await goodsRecievedNoteLine
                                                  .Where(x => x.ProductId == product.ProductId)
                                                .ToListAsync();
                salesVsPurchaseVm.TotalUnitsRecieved = goodsRecievedNoteLines.Sum(x => x.Quantity);

                Items.Add(salesVsPurchaseVm);
            }
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetbyCustomerId([FromRoute] int id)
        {
            Customer customer = await _context.Customer
                .Where(x => x.CustomerId == id)
                .Include(x => x.customerType)
                .FirstOrDefaultAsync();

            CustomerReport customerReport = new CustomerReport();

            if(customer != null)
            {
                customerReport.Customer = customer;
                customerReport.CustomerName = customer.CustomerName;

                var product = from p in _context.Product
                              select p;
                List<SalesOrder> salesOrders = await _context.SalesOrder
                                .Where(x => x.CustomerId == customerReport.Customer.CustomerId)
                                .Include(x => x.SalesOrderLines)
                                .ToListAsync();
                List<int> Productid = new List<int>();
                foreach(SalesOrder salesOrder in salesOrders)
                {
                    foreach(SalesOrderLine sales in salesOrder.SalesOrderLines)
                    {
                        Productid.Add(sales.ProductId);
                    }
                }
                Productid = Productid.Distinct().ToList();
                List<Product> Drugs = await product
                            .Where(x => Productid.Contains(x.ProductId))
                            .ToListAsync();
                List<Product> products = Drugs.Distinct().ToList();

                var salesOrderLine = from p in _context.SalesOrderLine
                                     select p;

                salesOrderLine = salesOrderLine.Where(x => x.SalesOrder.CustomerId == customerReport.Customer.CustomerId);

                List<ProductUsage> productUsages = new List<ProductUsage>();
                List<CustomerSaleOrderLineVM> customerSaleOrderLineVM = new List<CustomerSaleOrderLineVM>();
                foreach (Product drug in products)
                {
                    ProductUsage productUsage = new ProductUsage
                    {
                        ProductId = drug.ProductId,
                        ProductName = drug.ProductName,
                    };

                    List<SalesOrderLine> salesOrderLines = await salesOrderLine
                        .Include(x => x.SalesOrder.Invoice.PaymentReceive.PaymentType)
                        .Include(x => x.GoodsRecievedNoteLine)
                        .Where(x => x.ProductId == drug.ProductId)
                        .ToListAsync();
                    salesOrderLines = salesOrderLines.Distinct().ToList();

                    productUsage.Quantity = salesOrderLines.Sum(x => x.Quantity);

                    productUsages.Add(productUsage);
                    foreach (SalesOrderLine line in salesOrderLines)
                    {
                        CustomerSaleOrderLineVM lineVM = new CustomerSaleOrderLineVM
                        {
                            ProductName = drug.ProductName,
                            Quantity = line.Quantity,
                            saledate = line.SalesOrder.SaleDate.Date,
                            SalesOrderName = line.SalesOrder.SalesOrderName,
                            Total = line.Total,
                            BatchNo = line.GoodsRecievedNoteLine.BatchID
                        };
                        if (line.SalesOrder.Invoice != null)
                        {
                            lineVM.InvoceName = line.SalesOrder.Invoice.InvoiceName;

                            if (line.SalesOrder.Invoice.PaymentReceive != null)
                            {
                                lineVM.PaymentReciveName = line.SalesOrder.Invoice.PaymentReceive.PaymentReceiveName;
                                lineVM.PaymentType = line.SalesOrder.Invoice.PaymentReceive.PaymentType.PaymentTypeName;
                            }

                        }
                        if (lineVM != null)
                        {
                            customerSaleOrderLineVM.Add(lineVM);
                        }
                    }
                }
                customerReport.productUsage = productUsages;
                customerReport.salesOrderLines = customerSaleOrderLineVM;
            }

            return Ok(customerReport);
        }
    }
}
