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

namespace coderush.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/RFPpaymentRecieveds")]
    public class RFPpaymentRecievedsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INumberSequence _numberSequence;

        public RFPpaymentRecievedsController(ApplicationDbContext context,
                        INumberSequence numberSequence)
        {
            _context = context;
            _numberSequence = numberSequence;
        }

        // GET: api/PaymentReceive
        [HttpGet]
        public async Task<IActionResult> GetPaymentReceive()
        {
            List<RFPpaymentRecieved> Items = await _context.RFPpaymentRecieved.OrderByDescending(x => x.RFPpaymentRecievedId).ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> NotRecieved()
        {   
            List<RFPpaymentRecieved> Items = new List<RFPpaymentRecieved>();
            try
            {
                List<RFPDrugRecieve> receives = new List<RFPDrugRecieve>();
                receives = await _context.RFPDrugRecieve
                        .Where(x => x.IsFullReceive == true)
                        .ToListAsync();
                List<int> ids = new List<int>();

                foreach (var item in receives)
                {
                    ids.Add(item.RFPpaymentRecievedId);
                }

                Items = Items = await _context.RFPpaymentRecieved
                    .Where(x => !ids.Contains(x.RFPpaymentRecievedId))
                    .Where(x => x.PaymentType.PaymentTypeName == "Drug Payment")
                    .ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(Items);
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetbyInvoiceId([FromRoute] int id)
        {
            List<RFPpaymentRecieved> Items = await _context.RFPpaymentRecieved
                .Where(x => x.RFPpaymentRecievedId == id)
                .Include(x => x.PaymentType)
                .Include(x => x.RFPinvoice)
                .Include(x => x.RFPDrugRecieve)
                .ToListAsync();
            int Count = Items.Count();

            return Ok(new { Items, Count });
        }
        [HttpPost("[action]")]
        public IActionResult Insert([FromBody] CrudViewModel<RFPpaymentRecieved> payload)
        {
            RFPpaymentRecieved paymentReceive = payload.value;
            if (paymentReceive.IsFullPayment)
            {
                RFPinvoice invoice = _context.RFPinvoice.Find(paymentReceive.RFPinvoiceId);
                invoice.fullyPaid = paymentReceive.IsFullPayment;
                _context.RFPinvoice.Update(invoice);
                _context.SaveChanges();
            }

            paymentReceive.RFPpaymentRecievedName = _numberSequence.GetNumberSequence("RFPPAY");
            _context.RFPpaymentRecieved.Add(paymentReceive);
            _context.SaveChanges();
            paymentReceive.PaymentType = _context.PaymentType.Find(paymentReceive.PaymentTypeId);
            return Ok(paymentReceive);
        }
        [HttpPost("[action]")]
        public IActionResult Add([FromBody] RFPpaymentRecieved payload)
        {
            RFPpaymentRecieved paymentReceive = payload;
            if (paymentReceive.IsFullPayment)
            {
                RFPinvoice invoice = _context.RFPinvoice.Find(paymentReceive.RFPinvoiceId);
                invoice.fullyPaid = paymentReceive.IsFullPayment;
                _context.RFPinvoice.Update(invoice);
                _context.SaveChanges();
            }
            paymentReceive.RFPpaymentRecievedName = _numberSequence.GetNumberSequence("RFPPAY");
            _context.RFPpaymentRecieved.Add(paymentReceive);
            _context.SaveChanges();
            return Ok(paymentReceive);
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody] CrudViewModel<RFPpaymentRecieved> payload)
        {
            RFPpaymentRecieved paymentReceive = payload.value;
            if (paymentReceive.IsFullPayment)
            {
                RFPinvoice invoice = _context.RFPinvoice.Find(paymentReceive.RFPinvoiceId);
                invoice.fullyPaid = paymentReceive.IsFullPayment;
                _context.RFPinvoice.Update(invoice);
                _context.SaveChanges();
            }
            _context.RFPpaymentRecieved.Update(paymentReceive);
            _context.SaveChanges();
            return Ok(paymentReceive);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody] CrudViewModel<RFPpaymentRecieved> payload)
        {
            RFPpaymentRecieved paymentReceive = _context.RFPpaymentRecieved
                .Where(x => x.RFPpaymentRecievedId == (int)payload.key)
                .FirstOrDefault();
            if (paymentReceive.IsFullPayment)
            {
                RFPinvoice invoice = _context.RFPinvoice.Find(paymentReceive.RFPinvoiceId);
                invoice.fullyPaid = !paymentReceive.IsFullPayment;
                _context.RFPinvoice.Update(invoice);
                _context.SaveChanges();
            }
            _context.RFPpaymentRecieved.Remove(paymentReceive);
            _context.SaveChanges();
            return Ok(paymentReceive);

        }
    }
}