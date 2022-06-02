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
    [Route("api/PaymentVoucher")]
    public class PaymentVoucherController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INumberSequence _numberSequence;

        public PaymentVoucherController(ApplicationDbContext context,
                        INumberSequence numberSequence)
        {
            _context = context;
            _numberSequence = numberSequence;
        }

        // GET: api/PaymentVoucher
        [HttpGet]
        public async Task<IActionResult> GetPaymentVoucher()
        {
            List<PaymentVoucher> Items = await _context.PaymentVoucher.OrderByDescending(x => x.PaymentvoucherId).ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetbyBillId([FromRoute] int id)
        {
            List<PaymentVoucher> Items = await _context.PaymentVoucher
                .Where(x => x.BillId == id)
                .Include(x => x.PaymentType)
                .Include(x => x.Bill)
                .ToListAsync();
            int Count = Items.Count();

            return Ok(new { Items, Count });
        }
        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<PaymentVoucher> payload)
        {
            PaymentVoucher paymentVoucher = payload.value;
            if (paymentVoucher.IsFullPayment)
            {
                Bill bill = _context.Bill.Find(paymentVoucher.BillId);
                bill.fullPaid = paymentVoucher.IsFullPayment;
                _context.Bill.Update(bill);
                _context.SaveChanges();
            }
            paymentVoucher.PaymentVoucherName = _numberSequence.GetNumberSequence("PAYVCH");
            _context.PaymentVoucher.Add(paymentVoucher);
            _context.SaveChanges();
            return Ok(paymentVoucher);
        }
        [HttpPost("[action]")]
        public IActionResult Add([FromBody] PaymentVoucher payload)
        {
            PaymentVoucher paymentVoucher = payload;
            if (paymentVoucher.IsFullPayment)
            {
                Bill bill = _context.Bill.Find(paymentVoucher.BillId);
                bill.fullPaid = paymentVoucher.IsFullPayment;
                _context.Bill.Update(bill);
                _context.SaveChanges();
            }

            paymentVoucher.PaymentVoucherName = _numberSequence.GetNumberSequence("PAYVCH");
            _context.PaymentVoucher.Add(paymentVoucher);
            _context.SaveChanges();
            return Ok(paymentVoucher);
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<PaymentVoucher> payload)
        {
            PaymentVoucher paymentVoucher = payload.value;
            if (paymentVoucher.IsFullPayment)
            {
                Bill bill = _context.Bill.Find(paymentVoucher.BillId);
                bill.fullPaid = paymentVoucher.IsFullPayment;
                _context.Bill.Update(bill);
                _context.SaveChanges();
            }
            _context.PaymentVoucher.Update(paymentVoucher);
            _context.SaveChanges();
            return Ok(paymentVoucher);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<PaymentVoucher> payload)
        {
            PaymentVoucher paymentVoucher = _context.PaymentVoucher
                .Where(x => x.PaymentvoucherId == (int)payload.key)
                .FirstOrDefault();
            if (paymentVoucher.IsFullPayment)
            {
                Bill bill = _context.Bill.Find(paymentVoucher.BillId);
                bill.fullPaid = !paymentVoucher.IsFullPayment;
                _context.Bill.Update(bill);
                _context.SaveChanges();
            }
            _context.PaymentVoucher.Remove(paymentVoucher);
            _context.SaveChanges();
            return Ok(paymentVoucher);

        }
    }
}