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
using coderush.Services;

namespace coderush.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Report")]
    public class ReportController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IFunctional _functional;

        public ReportController(ApplicationDbContext context, IFunctional functional)             
        {
            _context = context;
            _functional = functional;
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
                salesVsPurchaseVm.RemainderStock = salesVsPurchaseVm.TotalUnitsRecieved - salesVsPurchaseVm.TotalUnitsSold;
                salesVsPurchaseVm.Profit = salesVsPurchaseVm.TotalSales - salesVsPurchaseVm.TotalPurchase;

                Items.Add(salesVsPurchaseVm);
            }
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> SalesVSPurchaseExcel()
        {
            List<SalesVsPurchaseVm> Items = new List<SalesVsPurchaseVm>();

            List<Product> products = await _context.Product.ToListAsync();

            foreach (Product product in products)
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
                salesVsPurchaseVm.RemainderStock = salesVsPurchaseVm.TotalUnitsRecieved - salesVsPurchaseVm.TotalUnitsSold;
                salesVsPurchaseVm.Profit = salesVsPurchaseVm.TotalSales - salesVsPurchaseVm.TotalPurchase;

                Items.Add(salesVsPurchaseVm);
            }
            DateTime now = DateTime.Now;
            string name = "SalesVsPurchase" + now.ToString("M") + ".xlsx";

            var ExelByte = _functional.ExporttoExcel<SalesVsPurchaseVm>(Items);

            return File(
               fileContents: ExelByte,
               contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
               fileDownloadName: name
                );
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> SalesVSPurchaseMonth([FromBody] DateRange payload)
        {
            DateRange date = payload;
            List<SalesVsPurchaseVm> Items = new List<SalesVsPurchaseVm>();

            List<Product> products = await _context.Product.ToListAsync();

            var salesOrderLine = from p in _context.SalesOrderLine
                                 select p;
            var purchaseOrderLine = from p in _context.PurchaseOrderLine
                                    select p;
            var goodsRecievedNoteLine = from p in _context.GoodsRecievedNoteLine 
                                    select p;
            salesOrderLine = salesOrderLine.Where(x => x.SalesOrder.SaleDate >= date.Start);
            purchaseOrderLine = purchaseOrderLine.Where(x => x.PurchaseOrder.OrderDate >= date.Start);
            goodsRecievedNoteLine = goodsRecievedNoteLine.Where(x => x.GoodsReceivedNote.GRNDate >= date.Start);
            salesOrderLine = salesOrderLine.Where(x => x.SalesOrder.SaleDate <= date.End);
            purchaseOrderLine = purchaseOrderLine.Where(x => x.PurchaseOrder.OrderDate <= date.End);
            goodsRecievedNoteLine = goodsRecievedNoteLine.Where(x => x.GoodsReceivedNote.GRNDate <= date.End);
            //salesOrderLine = salesOrderLine.Where(x => x.SalesOrder.SaleDate.Month == 6);
            //salesOrderLine = salesOrderLine.Where(x => x.SalesOrder.SaleDate.Year == 2020);
            //purchaseOrderLine = purchaseOrderLine.Where(x => x.PurchaseOrder.OrderDate.Month == 6);
            //purchaseOrderLine = purchaseOrderLine.Where(x => x.PurchaseOrder.OrderDate.Year == 2020);
            //goodsRecievedNoteLine = goodsRecievedNoteLine.Where(x => x.GoodsReceivedNote.GRNDate.Month == 6);
            //goodsRecievedNoteLine = goodsRecievedNoteLine.Where(x => x.GoodsReceivedNote.GRNDate.Year == 2020);


            foreach (Product product in products)
            {
                SalesVsPurchaseVm salesVsPurchaseVm = new SalesVsPurchaseVm();

                salesVsPurchaseVm.ProductName = product.ProductName;

                List<SalesOrderLine> salesOrderLines = await salesOrderLine
                                                .Where(x => x.ProductId == product.ProductId)
                                                .ToListAsync();;
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

                salesVsPurchaseVm.RemainderStock = salesVsPurchaseVm.TotalUnitsRecieved - salesVsPurchaseVm.TotalUnitsSold;
                salesVsPurchaseVm.Profit = salesVsPurchaseVm.TotalSales - salesVsPurchaseVm.TotalPurchase;

                Items.Add(salesVsPurchaseVm);
            }
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> SalesVSPurchaseMonthEXcel([FromBody] DateRange payload)
        {
            DateRange date = payload;
            List<SalesVsPurchaseVm> Items = new List<SalesVsPurchaseVm>();

            List<Product> products = await _context.Product.ToListAsync();

            var salesOrderLine = from p in _context.SalesOrderLine
                                 select p;
            var purchaseOrderLine = from p in _context.PurchaseOrderLine
                                    select p;
            var goodsRecievedNoteLine = from p in _context.GoodsRecievedNoteLine
                                        select p;
            salesOrderLine = salesOrderLine.Where(x => x.SalesOrder.SaleDate >= date.Start);
            purchaseOrderLine = purchaseOrderLine.Where(x => x.PurchaseOrder.OrderDate >= date.Start);
            goodsRecievedNoteLine = goodsRecievedNoteLine.Where(x => x.GoodsReceivedNote.GRNDate >= date.Start);
            salesOrderLine = salesOrderLine.Where(x => x.SalesOrder.SaleDate <= date.End);
            purchaseOrderLine = purchaseOrderLine.Where(x => x.PurchaseOrder.OrderDate <= date.End);
            goodsRecievedNoteLine = goodsRecievedNoteLine.Where(x => x.GoodsReceivedNote.GRNDate <= date.End);
            //salesOrderLine = salesOrderLine.Where(x => x.SalesOrder.SaleDate.Month == 6);
            //salesOrderLine = salesOrderLine.Where(x => x.SalesOrder.SaleDate.Year == 2020);
            //purchaseOrderLine = purchaseOrderLine.Where(x => x.PurchaseOrder.OrderDate.Month == 6);
            //purchaseOrderLine = purchaseOrderLine.Where(x => x.PurchaseOrder.OrderDate.Year == 2020);
            //goodsRecievedNoteLine = goodsRecievedNoteLine.Where(x => x.GoodsReceivedNote.GRNDate.Month == 6);
            //goodsRecievedNoteLine = goodsRecievedNoteLine.Where(x => x.GoodsReceivedNote.GRNDate.Year == 2020);


            foreach (Product product in products)
            {
                SalesVsPurchaseVm salesVsPurchaseVm = new SalesVsPurchaseVm();

                salesVsPurchaseVm.ProductName = product.ProductName;

                List<SalesOrderLine> salesOrderLines = await salesOrderLine
                                                .Where(x => x.ProductId == product.ProductId)
                                                .ToListAsync(); ;
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

                salesVsPurchaseVm.RemainderStock = salesVsPurchaseVm.TotalUnitsRecieved - salesVsPurchaseVm.TotalUnitsSold;
                salesVsPurchaseVm.Profit = salesVsPurchaseVm.TotalSales - salesVsPurchaseVm.TotalPurchase;

                Items.Add(salesVsPurchaseVm);
            }
            DateTime now = DateTime.Now;
            string name = "SalesVsPurchase" + now.ToString("M") + ".xlsx";

            var ExelByte = _functional.ExporttoExcel<SalesVsPurchaseVm>(Items);

            return File(
               fileContents: ExelByte,
               contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
               fileDownloadName: name
                );
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
                            saledate = line.SalesOrder.SaleDate.Date.ToString("dd MMMM yyyy"),
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
                customerReport.salesOrderLines = customerSaleOrderLineVM.Distinct().ToList();
            }

            return Ok(customerReport);
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> CustomerIdToExcel([FromRoute] int id)
        {
            Customer customer = await _context.Customer
                .Where(x => x.CustomerId == id)
                .Include(x => x.customerType)
                .FirstOrDefaultAsync();

            CustomerReport customerReport = new CustomerReport();

            if (customer != null)
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
                foreach (SalesOrder salesOrder in salesOrders)
                {
                    foreach (SalesOrderLine sales in salesOrder.SalesOrderLines)
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
                            saledate = line.SalesOrder.SaleDate.Date.ToString("dd MMMM yyyy"),
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
                customerReport.salesOrderLines = customerSaleOrderLineVM.Distinct().ToList();
            }

            DateTime now = DateTime.Now;
            string name = customer.CustomerName + now.ToString("M") + ".xlsx";

            var ExelByte = _functional.ExporttoExcel<CustomerSaleOrderLineVM>(customerReport.salesOrderLines);

            return File(
               fileContents: ExelByte,
               contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
               fileDownloadName: name
                );
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetbyProductId([FromRoute] int id)
        {
            Product drug = await _context.Product
                    .Include(x => x.UnitOfMeasure)
                    .Include(x => x.ProductType)
                    .Where(x => x.ProductId == id)
                    .FirstOrDefaultAsync();
            DrugHistoryVM drugHistoryVM = new DrugHistoryVM();
            drugHistoryVM.ProductName = drug.ProductName;

            var salesOrderLine = from p in _context.SalesOrderLine
                                 select p;
            var purchaseOrderLine = from p in _context.PurchaseOrderLine
                                    select p;
            var goodsRecievedNoteLine = from p in _context.GoodsRecievedNoteLine
                                        select p;
            salesOrderLine = salesOrderLine.Where(x => x.ProductId == drug.ProductId);
            purchaseOrderLine = purchaseOrderLine.Where(x => x.ProductId == drug.ProductId);
            goodsRecievedNoteLine = goodsRecievedNoteLine.Where(x => x.ProductId == drug.ProductId);

            List<SalesOrderLine> SalesOrderLine = await salesOrderLine
                            .Include(x => x.SalesOrder.Customer)
                            .Include(x => x.SalesOrder.Invoice.PaymentReceive.PaymentType)
                            .Include(x => x.RFPSaleorder.RFPCustomer)
                            .ToListAsync();
            List<PurchaseOrderLine> PurchaseOrderLine = await purchaseOrderLine
                .Include(x => x.PurchaseOrder.Vendor)
                .ToListAsync();
            List<GoodsRecievedNoteLine> GoodsRecievedNoteLine = await goodsRecievedNoteLine
                .Include(x => x.RFPDrugRecieve)
                .Include(x => x.GoodsReceivedNote.purchaseOrder)
                .ToListAsync();
            SalesOrderLine = SalesOrderLine.Distinct().ToList();
            PurchaseOrderLine = PurchaseOrderLine.Distinct().ToList();
            GoodsRecievedNoteLine = GoodsRecievedNoteLine.Distinct().ToList();
            List<SaleHistory> Items = new List<SaleHistory>();
            foreach (SalesOrderLine salesOrderLines in SalesOrderLine)
            {
                SaleHistory sales = new SaleHistory
                {
                    ProductName = drug.ProductName,
                    Quanity = salesOrderLines.Quantity,
                    Total = salesOrderLines.Total
                };
                if (salesOrderLines.SalesOrder != null)
                {
                    sales.CustomerName = salesOrderLines.SalesOrder.Customer.CustomerName;
                    sales.NHIF = salesOrderLines.SalesOrder.Customer.Address;
                    sales.saledate = salesOrderLines.SalesOrder.SaleDate.ToString("dd MMMM yyyy");
                    sales.SaleOrderName = salesOrderLines.SalesOrder.SalesOrderName;
                    if (salesOrderLines.SalesOrder.Invoice != null)
                    {
                        if (salesOrderLines.SalesOrder.Invoice.PaymentReceive != null)
                        {
                            sales.PaymentMode = salesOrderLines.SalesOrder.Invoice.PaymentReceive.PaymentType.PaymentTypeName;
                        }
                    }


                }
                if (salesOrderLines.RFPSaleorder != null)
                {
                    sales.CustomerName = salesOrderLines.RFPSaleorder.RFPCustomer.RFPCustomerName;
                    sales.saledate = salesOrderLines.RFPSaleorder.SaleDate.ToString("dd MMMM yyyy");
                    sales.SaleOrderName = salesOrderLines.RFPSaleorder.RFPSaleorderName;
                }

                Items.Add(sales);
            }
            List<PurchaseOrderLineVM> purchaseOrderLineVMs = new List<PurchaseOrderLineVM>();
            foreach(PurchaseOrderLine purchaseLine in PurchaseOrderLine)
            {
                PurchaseOrderLineVM purchaseOrder = new PurchaseOrderLineVM
                {
                    Quantity = purchaseLine.Quantity,
                    Total = purchaseLine.Total,
                    
                };
                if(purchaseLine.PurchaseOrder != null)
                {
                    purchaseOrder.DeliveryDate = purchaseLine.PurchaseOrder.DeliveryDate.ToString("dd MMMM yyyy");
                    purchaseOrder.OrderDate = purchaseLine.PurchaseOrder.OrderDate.ToString("dd MMMM yyyy");
                    purchaseOrder.PurchaseOrderName = purchaseLine.PurchaseOrder.PurchaseOrderName;
                    if(purchaseLine.PurchaseOrder.Vendor != null)
                    {
                        purchaseOrder.VendorName = purchaseLine.PurchaseOrder.Vendor.VendorName;
                    }
                }

                purchaseOrderLineVMs.Add(purchaseOrder);
            }
            List<GoodsRecievedNoteLineVM> goodsRecievedNoteLineVMs = new List<GoodsRecievedNoteLineVM>();

            foreach(GoodsRecievedNoteLine good in GoodsRecievedNoteLine)
            {
                GoodsRecievedNoteLineVM goods = new GoodsRecievedNoteLineVM
                {
                    BatchID = good.BatchID,
                    ExpiryDate = good.ExpiryDate.ToString("dd MMMM yyyy"),
                    Expired = good.Expired,
                    InStock = good.InStock,
                    Quantity = good.Quantity,
                    Sold =good.Sold
                };
                if(good.GoodsReceivedNote != null)
                {
                    goods.GRNDate = good.GoodsReceivedNote.GRNDate.ToString("dd MMMM yyyy");
                    goods.GoodsReceivedNoteName = good.GoodsReceivedNote.GoodsReceivedNoteName;
                    if(good.GoodsReceivedNote.purchaseOrder != null)
                    {
                        goods.PurchaseOrdername = good.GoodsReceivedNote.purchaseOrder.PurchaseOrderName;
                    } 
                }
                if(good.RFPDrugRecieve != null)
                {
                    goods.GRNDate = good.RFPDrugRecieve.GRNDate.ToString("dd MMMM yyyy");
                    goods.GoodsReceivedNoteName = good.RFPDrugRecieve.RFPDrugRecieveName;
                }
                goodsRecievedNoteLineVMs.Add(goods);
            }
            drugHistoryVM.goodsRecievedNoteLineVMs = goodsRecievedNoteLineVMs.Distinct().ToList();
            drugHistoryVM.purchaseOrderLineVMs = purchaseOrderLineVMs.Distinct().ToList();
            drugHistoryVM.saleHistories = Items.Distinct().ToList();
             return Ok(drugHistoryVM);
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetbyProductIdSaleToExcel([FromRoute] int id)
        {
            Product drug = await _context.Product
                    .Include(x => x.UnitOfMeasure)
                    .Include(x => x.ProductType)
                    .Where(x => x.ProductId == id)
                    .FirstOrDefaultAsync();
            DrugHistoryVM drugHistoryVM = new DrugHistoryVM();
            drugHistoryVM.ProductName = drug.ProductName;

            var salesOrderLine = from p in _context.SalesOrderLine
                                 select p;
            salesOrderLine = salesOrderLine.Where(x => x.ProductId == drug.ProductId);


            List<SalesOrderLine> SalesOrderLine = await salesOrderLine
                            .Include(x => x.SalesOrder.Customer)
                            .Include(x => x.SalesOrder.Invoice.PaymentReceive.PaymentType)
                            .Include(x => x.RFPSaleorder.RFPCustomer)
                            .ToListAsync();
     
            SalesOrderLine = SalesOrderLine.Distinct().ToList();
            List<SaleHistory> Items = new List<SaleHistory>();
            foreach (SalesOrderLine salesOrderLines in SalesOrderLine)
            {
                SaleHistory sales = new SaleHistory
                {
                    ProductName = drug.ProductName,
                    Quanity = salesOrderLines.Quantity,
                    Total = salesOrderLines.Total
                };
                if (salesOrderLines.SalesOrder != null)
                {
                    sales.CustomerName = salesOrderLines.SalesOrder.Customer.CustomerName;
                    sales.NHIF = salesOrderLines.SalesOrder.Customer.Address;
                    sales.saledate = salesOrderLines.SalesOrder.SaleDate.ToString("dd MMMM yyyy");
                    sales.SaleOrderName = salesOrderLines.SalesOrder.SalesOrderName;
                    if (salesOrderLines.SalesOrder.Invoice != null)
                    {
                        if (salesOrderLines.SalesOrder.Invoice.PaymentReceive != null)
                        {
                            sales.PaymentMode = salesOrderLines.SalesOrder.Invoice.PaymentReceive.PaymentType.PaymentTypeName;
                        }
                    }


                }
                if (salesOrderLines.RFPSaleorder != null)
                {
                    sales.CustomerName = salesOrderLines.RFPSaleorder.RFPCustomer.RFPCustomerName;
                    sales.saledate = salesOrderLines.RFPSaleorder.SaleDate.ToString("dd MMMM yyyy");
                    sales.SaleOrderName = salesOrderLines.RFPSaleorder.RFPSaleorderName;
                }

                Items.Add(sales);
            }
            drugHistoryVM.saleHistories = Items.Distinct().ToList();

            DateTime now = DateTime.Now;
            string name = drug.ProductName +"Sale" + now.ToString("M") + ".xlsx";

            var ExelByte = _functional.ExporttoExcel<SaleHistory>(drugHistoryVM.saleHistories);

            return File(
               fileContents: ExelByte,
               contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
               fileDownloadName: name
                );
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetbyProductIdPurchaseToExcel([FromRoute] int id)
        {
            Product drug = await _context.Product
                    .Include(x => x.UnitOfMeasure)
                    .Include(x => x.ProductType)
                    .Where(x => x.ProductId == id)
                    .FirstOrDefaultAsync();
            DrugHistoryVM drugHistoryVM = new DrugHistoryVM();
            drugHistoryVM.ProductName = drug.ProductName;

            var purchaseOrderLine = from p in _context.PurchaseOrderLine
                                    select p;
            purchaseOrderLine = purchaseOrderLine.Where(x => x.ProductId == drug.ProductId);

            List<PurchaseOrderLine> PurchaseOrderLine = await purchaseOrderLine
                .Include(x => x.PurchaseOrder.Vendor)
                .ToListAsync();

            PurchaseOrderLine = PurchaseOrderLine.Distinct().ToList();

            List<PurchaseOrderLineVM> purchaseOrderLineVMs = new List<PurchaseOrderLineVM>();
            foreach (PurchaseOrderLine purchaseLine in PurchaseOrderLine)
            {
                PurchaseOrderLineVM purchaseOrder = new PurchaseOrderLineVM
                {
                    Quantity = purchaseLine.Quantity,
                    Total = purchaseLine.Total,

                };
                if (purchaseLine.PurchaseOrder != null)
                {
                    purchaseOrder.DeliveryDate = purchaseLine.PurchaseOrder.DeliveryDate.ToString("dd MMMM yyyy");
                    purchaseOrder.OrderDate = purchaseLine.PurchaseOrder.OrderDate.ToString("dd MMMM yyyy");
                    purchaseOrder.PurchaseOrderName = purchaseLine.PurchaseOrder.PurchaseOrderName;
                    if (purchaseLine.PurchaseOrder.Vendor != null)
                    {
                        purchaseOrder.VendorName = purchaseLine.PurchaseOrder.Vendor.VendorName;
                    }
                }

                purchaseOrderLineVMs.Add(purchaseOrder);
            }

            drugHistoryVM.purchaseOrderLineVMs = purchaseOrderLineVMs.Distinct().ToList();

            DateTime now = DateTime.Now;
            string name = drug.ProductName + "Purchase" + now.ToString("M") + ".xlsx";

            var ExelByte = _functional.ExporttoExcel<PurchaseOrderLineVM>(drugHistoryVM.purchaseOrderLineVMs);

            return File(
               fileContents: ExelByte,
               contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
               fileDownloadName: name
                );
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetbyProductIdRecieveToExcel([FromRoute] int id)
        {
            Product drug = await _context.Product
                    .Include(x => x.UnitOfMeasure)
                    .Include(x => x.ProductType)
                    .Where(x => x.ProductId == id)
                    .FirstOrDefaultAsync();
            DrugHistoryVM drugHistoryVM = new DrugHistoryVM();
            drugHistoryVM.ProductName = drug.ProductName;

            var goodsRecievedNoteLine = from p in _context.GoodsRecievedNoteLine
                                        select p;

            goodsRecievedNoteLine = goodsRecievedNoteLine.Where(x => x.ProductId == drug.ProductId);

            List<GoodsRecievedNoteLine> GoodsRecievedNoteLine = await goodsRecievedNoteLine
                .Include(x => x.RFPDrugRecieve)
                .Include(x => x.GoodsReceivedNote.purchaseOrder)
                .ToListAsync();
            GoodsRecievedNoteLine = GoodsRecievedNoteLine.Distinct().ToList();

            List<GoodsRecievedNoteLineVM> goodsRecievedNoteLineVMs = new List<GoodsRecievedNoteLineVM>();

            foreach (GoodsRecievedNoteLine good in GoodsRecievedNoteLine)
            {
                GoodsRecievedNoteLineVM goods = new GoodsRecievedNoteLineVM
                {
                    BatchID = good.BatchID,
                    ExpiryDate = good.ExpiryDate.ToString("dd MMMM yyyy"),
                    Expired = good.Expired,
                    InStock = good.InStock,
                    Quantity = good.Quantity,
                    Sold = good.Sold
                };
                if (good.GoodsReceivedNote != null)
                {
                    goods.GRNDate = good.GoodsReceivedNote.GRNDate.ToString("dd MMMM yyyy");
                    goods.GoodsReceivedNoteName = good.GoodsReceivedNote.GoodsReceivedNoteName;
                    if (good.GoodsReceivedNote.purchaseOrder != null)
                    {
                        goods.PurchaseOrdername = good.GoodsReceivedNote.purchaseOrder.PurchaseOrderName;
                    }
                }
                if (good.RFPDrugRecieve != null)
                {
                    goods.GRNDate = good.RFPDrugRecieve.GRNDate.ToString("dd MMMM yyyy");
                    goods.GoodsReceivedNoteName = good.RFPDrugRecieve.RFPDrugRecieveName;
                }
                goodsRecievedNoteLineVMs.Add(goods);
            }
            drugHistoryVM.goodsRecievedNoteLineVMs = goodsRecievedNoteLineVMs.Distinct().ToList();

            DateTime now = DateTime.Now;
            string name = drug.ProductName + "Recieve" + now.ToString("M") + ".xlsx";

            var ExelByte = _functional.ExporttoExcel<GoodsRecievedNoteLineVM>(drugHistoryVM.goodsRecievedNoteLineVMs);

            return File(
               fileContents: ExelByte,
               contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
               fileDownloadName: name
                );
        }
    }
    
}
