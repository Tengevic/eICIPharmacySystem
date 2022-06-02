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

namespace coderush.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/RFPinvoices")]
    public class RFPinvoicesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INumberSequence _numberSequence;

        public RFPinvoicesController(ApplicationDbContext context,
                        INumberSequence numberSequence)
        {
            _context = context;
            _numberSequence = numberSequence;
        }

        // GET: api/Invoice
        [HttpGet]
        public async Task<IActionResult> GetInvoice()
        {
            List<RFPinvoice> Items = await _context.RFPinvoice.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetNotPaidYet()
        {
            List<RFPinvoice> invoices = new List<RFPinvoice>();
            try
            {
                List<RFPpaymentRecieved> receives = new List<RFPpaymentRecieved>();
                receives = await _context.RFPpaymentRecieved
                        .Where(x => x.IsFullPayment == true)
                        .ToListAsync();
                List<int> ids = new List<int>();

                foreach (var item in receives)
                {
                    ids.Add(item.RFPinvoiceId);
                }

                invoices = await _context.RFPinvoice
                    .Where(x => !ids.Contains(x.RFPinvoiceId))
                    .ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(invoices);
        }

        [HttpPost("[action]")]
        public IActionResult Insert([FromBody] CrudViewModel<RFPinvoice> payload)
        {
            RFPinvoice invoice = payload.value;

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
            RFPSaleorder sales = _context.RFPSaleorder.Where(x => x.RFPSaleorderId == invoice.RFPSaleorderId).Include(x => x.SalesOrderLines).FirstOrDefault();
            if (sales.SalesOrderLines.Count == 0)
            {
                Err err = new Err
                {
                    message = "Add drugs to continue"
                };
                string errMsg = JsonConvert.SerializeObject(err);

                return BadRequest(err);
            }

            invoice.RFPinvoiceName = _numberSequence.GetNumberSequence("RINV");
            _context.RFPinvoice.Add(invoice);
            _context.SaveChanges();
            return Ok(invoice);
        }
        //api/Invoice/Add
        [HttpPost("[action]")]
        public IActionResult Add([FromBody] RFPinvoice payload)
        {
            RFPinvoice invoice = payload;

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
            RFPSaleorder sales = _context.RFPSaleorder.Where(x => x.RFPSaleorderId == invoice.RFPSaleorderId).Include(x => x.SalesOrderLines).FirstOrDefault();
            if (sales.SalesOrderLines.Count == 0)
            {
                Err err = new Err
                {
                    message = "Add drugs to continue"
                };
                string errMsg = JsonConvert.SerializeObject(err);

                return BadRequest(err);
            }
            invoice.InvoiceDate = current;
            invoice.RFPinvoiceName = _numberSequence.GetNumberSequence("RINV");
            _context.RFPinvoice.Add(invoice);
            _context.SaveChanges();
            return Ok(invoice);
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody] CrudViewModel<RFPinvoice> payload)
        {
            RFPinvoice invoice = payload.value;
            _context.RFPinvoice.Update(invoice);
            _context.SaveChanges();
            return Ok(invoice);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody] CrudViewModel<RFPinvoice> payload)
        {
            RFPinvoice invoice = _context.RFPinvoice
                .Where(x => x.RFPinvoiceId == (int)payload.key)
                .Include(x => x.RFPpaymentRecieved)
                .FirstOrDefault();
            if (invoice.RFPpaymentRecieved != null)
            {
                Err err = new Err
                {
                    message = "This order has payment records"
                };
                string errMsg = JsonConvert.SerializeObject(err);

                return BadRequest(err);
            }
            _context.RFPinvoice.Remove(invoice);
            _context.SaveChanges();
            return Ok(invoice);

        }
    }
}