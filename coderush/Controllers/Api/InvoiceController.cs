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
using System.IO;

namespace coderush.Controllers.Api
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Invoice")]
    public class InvoiceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INumberSequence _numberSequence;

        public InvoiceController(ApplicationDbContext context,
                        INumberSequence numberSequence)
        {
            _context = context;
            _numberSequence = numberSequence;
        }

        // GET: api/Invoice
        [HttpGet]
        public async Task<IActionResult> GetInvoice()
        {
            List<Invoice> Items = await _context.Invoice.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetNotPaidYet()
        {
            List<Invoice> invoices = new List<Invoice>();
            try
            {
                List<PaymentReceive> receives = new List<PaymentReceive>();
                receives = await _context.PaymentReceive
                        .Where(x => x.IsFullPayment == true)    
                        .ToListAsync();
                List<int> ids = new List<int>();

                foreach (var item in receives)
                {
                    ids.Add(item.InvoiceId);
                }

                invoices = await _context.Invoice
                    .Where(x => !ids.Contains(x.InvoiceId))
                    .ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(invoices);
        }

        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<Invoice> payload)
        {
            Invoice invoice = payload.value;

            DateTime current = DateTime.Now;
          
            double Totaldays = (invoice.InvoiceDueDate - current).TotalDays;

            if (!(Totaldays < 90))
            {
                Err err = new Err
                {
                    message = "Invoice should not be more than  90 days"
                };
                string errMsg = JsonConvert.SerializeObject(err);

                return BadRequest(err);

            }

            invoice.InvoiceName = _numberSequence.GetNumberSequence("INV");
            _context.Invoice.Add(invoice);
            _context.SaveChanges();
            return Ok(invoice);
        }
        //api/Invoice/Add
        [HttpPost("[action]")]
        public IActionResult Add([FromBody] Invoice payload)
        {
            Invoice invoice = payload;

            DateTime current = DateTime.Now;

            double Totaldays = (invoice.InvoiceDueDate - current).TotalDays;

            if (!(Totaldays < 90))
            {
                Err err = new Err
                {
                    message = "Invoice should not be more than  90 days"
                };
                string errMsg = JsonConvert.SerializeObject(err);

                return BadRequest(err);

            }
            SalesOrder sales = _context.SalesOrder.Where(x =>x.SalesOrderId == invoice.SalesOrderId).Include(x => x.SalesOrderLines).FirstOrDefault();
            if(sales.SalesOrderLines.Count == 0)
            {
                Err err = new Err
                {
                    message = "Add drugs to continue"
                };
                string errMsg = JsonConvert.SerializeObject(err);

                return BadRequest(err);
            }
            invoice.InvoiceDate = current;
            invoice.InvoiceName = _numberSequence.GetNumberSequence("INV");
            _context.Invoice.Add(invoice);
            _context.SaveChanges();
            return Ok(invoice);
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<Invoice> payload)
        {
            Invoice invoice = payload.value;
            _context.Invoice.Update(invoice);
            _context.SaveChanges();
            return Ok(invoice);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<Invoice> payload)
        {
            Invoice invoice = _context.Invoice
                .Where(x => x.InvoiceId == (int)payload.key)
                .Include(x => x.PaymentReceive)
                .FirstOrDefault();
            if (invoice.PaymentReceive != null)
            {
                Err err = new Err
                {
                    message = "This order has payment records"
                };
                string errMsg = JsonConvert.SerializeObject(err);

                return BadRequest(err);
            }
            _context.Invoice.Remove(invoice);
            _context.SaveChanges();
            return Ok(invoice);

        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Print([FromRoute] int id)
        {
            Invoice invoice = await _context.Invoice
                .Where(x => x.InvoiceId.Equals(id))
                .Include(x => x.SalesOrder.SalesOrderLines)
                    .ThenInclude(x => x.Product)
                .Include(x => x.SalesOrder.Customer)
                .FirstOrDefaultAsync();

            string path = "./Views/EmailViewModels/Invoice.html";


            string body = string.Empty;
            using (StreamReader reader = new StreamReader(path))
            {
                body = reader.ReadToEnd();
            }
            string drugrow = "";
            int num = 1;
            foreach (var batch in invoice.SalesOrder.SalesOrderLines)
            {
                string drug = "<tr>" +
                                   "<td>" + num + " </td>" +
                                   "<td>" + batch.Product.ProductName + "</td>" +
                                   "<td>" + batch.Quantity + "</td>" +
                                   "<td class='text-95'>" + batch.Price + "</td>" +
                                   "<td class='text-secondary-d3'>" + batch.Amount + "</td>" +
                               "</tr>";
                drugrow = drugrow + drug;
                num++;
            }
            body = body.Replace("{InvoiceName}", invoice.InvoiceName);
            body = body.Replace("{CustomerName}", invoice.SalesOrder.Customer.CustomerName);
            body = body.Replace("{InvoiceDate}", invoice.InvoiceDate.ToString("dd MMMM yyyy"));
            body = body.Replace("{InvoiceDueDate}", invoice.InvoiceDueDate.ToString("dd MMMM yyyy"));
            body = body.Replace("{subTotal}", invoice.SalesOrder.SubTotal.ToString());
            body = body.Replace("{Discount}", invoice.SalesOrder.Discount.ToString());
            body = body.Replace("{Tax}", invoice.SalesOrder.Tax.ToString());
            body = body.Replace("{Total}", invoice.SalesOrder.Total.ToString());
            body = body.Replace("{List}", drugrow);

            return Ok(body);
        }
    }
}