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

namespace coderush.Controllers.Api
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/PaymentReceive")]
    public class PaymentReceiveController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INumberSequence _numberSequence;

        public PaymentReceiveController(ApplicationDbContext context,
                        INumberSequence numberSequence)
        {
            _context = context;
            _numberSequence = numberSequence;
        }

        // GET: api/PaymentReceive
        [HttpGet]
        public async Task<IActionResult> GetPaymentReceive()
        {
            List<PaymentReceive> Items = await _context.PaymentReceive.OrderByDescending(x =>x.InvoiceId).ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetbyInvoiceId([FromRoute] int id)
        {
            List<PaymentReceive> Items = await _context.PaymentReceive
                .Where(x => x.InvoiceId == id)
                .Include(x => x.PaymentType)
                .Include(x => x.Invoice)
                .ToListAsync();
            int Count = Items.Count();

            return Ok(new { Items, Count });
        }
        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<PaymentReceive> payload)
        {
            PaymentReceive paymentReceive = payload.value;
            if (paymentReceive.IsFullPayment)
            {
                Invoice invoice = _context.Invoice.Find(paymentReceive.Invoice);
                invoice.fullyPaid = paymentReceive.IsFullPayment;
                _context.Invoice.Update(invoice);
                _context.SaveChanges();
            }

            paymentReceive.PaymentReceiveName = _numberSequence.GetNumberSequence("PAYRCV");
            _context.PaymentReceive.Add(paymentReceive);
            _context.SaveChanges();
            return Ok(paymentReceive);
        }
        [HttpPost("[action]")]
        public IActionResult Add([FromBody] PaymentReceive payload)
        {
            PaymentReceive paymentReceive = payload;
            if (paymentReceive.IsFullPayment)
            {
                Invoice invoice = _context.Invoice.Find(paymentReceive.InvoiceId);
                invoice.fullyPaid = paymentReceive.IsFullPayment;
                _context.Invoice.Update(invoice);
                _context.SaveChanges();
            }
            paymentReceive.PaymentReceiveName = _numberSequence.GetNumberSequence("PAYRCV");
            _context.PaymentReceive.Add(paymentReceive);
            _context.SaveChanges();
            return Ok(paymentReceive);
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<PaymentReceive> payload)
        {
            PaymentReceive paymentReceive = payload.value;
            if (paymentReceive.IsFullPayment)
            {
                Invoice invoice = _context.Invoice.Find(paymentReceive.InvoiceId);
                invoice.fullyPaid = paymentReceive.IsFullPayment;
                _context.Invoice.Update(invoice);
                _context.SaveChanges();
            }
            _context.PaymentReceive.Update(paymentReceive);
            _context.SaveChanges();
            return Ok(paymentReceive);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<PaymentReceive> payload)
        {
            PaymentReceive paymentReceive = _context.PaymentReceive
                .Where(x => x.PaymentReceiveId == (int)payload.key)
                .FirstOrDefault();
            if (paymentReceive.IsFullPayment)
            {
                Invoice invoice = _context.Invoice.Find(paymentReceive.InvoiceId);
                invoice.fullyPaid = !paymentReceive.IsFullPayment;
                _context.Invoice.Update(invoice);
                _context.SaveChanges();
            }
            _context.PaymentReceive.Remove(paymentReceive);
            _context.SaveChanges();
            return Ok(paymentReceive);

        }
        [HttpPost("[action]")]
        public IActionResult Upload([FromBody]CrudViewModel<IFormFile> payload)
        {

            return Ok();
       }
    }
}