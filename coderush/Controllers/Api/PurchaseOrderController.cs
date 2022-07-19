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
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Identity;

namespace coderush.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/PurchaseOrder")]
    public class PurchaseOrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INumberSequence _numberSequence;
        private IHostingEnvironment _environment;
        private readonly UserManager<ApplicationUser> _userManager;

        public PurchaseOrderController(ApplicationDbContext context, IHostingEnvironment environment, UserManager<ApplicationUser> userManager,
                        INumberSequence numberSequence)
        {
            _context = context;
            _numberSequence = numberSequence;
            _environment = environment;
            _userManager = userManager;
        }

        // GET: api/PurchaseOrder
        [HttpGet]
        public async Task<IActionResult> GetPurchaseOrder()
        {
            List<PurchaseOrder> Items = await _context.PurchaseOrder.OrderByDescending(x => x.PurchaseOrderId).ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetNotReceivedYet()
        {
            List<PurchaseOrder> purchaseOrders = new List<PurchaseOrder>();
            try
            {
                List<GoodsReceivedNote> grns = new List<GoodsReceivedNote>();
                grns = await _context.GoodsReceivedNote.Where(x => x.IsFullReceive == true).ToListAsync();
                List<int> ids = new List<int>();

                foreach (var item in grns)
                {
                    ids.Add(item.PurchaseOrderId);
                }

                purchaseOrders = await _context.PurchaseOrder
                    .Where(x => !ids.Contains(x.PurchaseOrderId))
                    .ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(purchaseOrders);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            PurchaseOrder result = await _context.PurchaseOrder
                .Where(x => x.PurchaseOrderId.Equals(id))
                .Include(x => x.PurchaseOrderLines)
                .FirstOrDefaultAsync();

            return Ok(result);
        }

        private void UpdatePurchaseOrder(int purchaseOrderId)
        {
            try
            {
                PurchaseOrder purchaseOrder = new PurchaseOrder();
                purchaseOrder = _context.PurchaseOrder
                    .Where(x => x.PurchaseOrderId.Equals(purchaseOrderId))
                    .FirstOrDefault();

                if (purchaseOrder != null)
                {
                    List<PurchaseOrderLine> lines = new List<PurchaseOrderLine>();
                    lines = _context.PurchaseOrderLine.Where(x => x.PurchaseOrderId.Equals(purchaseOrderId)).ToList();

                    //update master data by its lines
                    purchaseOrder.Amount = lines.Sum(x => x.Amount);
                    purchaseOrder.SubTotal = lines.Sum(x => x.SubTotal);

                    purchaseOrder.Discount = lines.Sum(x => x.DiscountAmount);
                    purchaseOrder.Tax = lines.Sum(x => x.TaxAmount);

                    purchaseOrder.Total = purchaseOrder.Freight + lines.Sum(x => x.Total);

                    _context.Update(purchaseOrder);

                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Insert([FromBody] CrudViewModel<PurchaseOrder> payload)
        {
            PurchaseOrder purchaseOrder = payload.value;
            purchaseOrder.PurchaseOrderName = _numberSequence.GetNumberSequence("PO");
            var user = await _userManager.GetUserAsync(HttpContext.User);
            purchaseOrder.UserId = user.Id;
            _context.PurchaseOrder.Add(purchaseOrder);
            _context.SaveChanges();
            this.UpdatePurchaseOrder(purchaseOrder.PurchaseOrderId);
            return Ok(purchaseOrder);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Update([FromBody] CrudViewModel<PurchaseOrder> payload)
        {
            PurchaseOrder purchaseOrder = payload.value;
            var user = await _userManager.GetUserAsync(HttpContext.User);
            purchaseOrder.UserId = user.Id;
            _context.PurchaseOrder.Update(purchaseOrder);
            _context.SaveChanges();
            this.UpdatePurchaseOrder(purchaseOrder.PurchaseOrderId);
            return Ok(purchaseOrder);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody] CrudViewModel<PurchaseOrder> payload)
        {
            PurchaseOrder purchaseOrder = _context.PurchaseOrder
                .Where(x => x.PurchaseOrderId == (int)payload.key)
                .Include(x => x.PurchaseOrderLines)
                .FirstOrDefault();
            if(purchaseOrder.PurchaseOrderLines.Count > 0)
            {
                Err err = new Err
                {
                    message = "Record has orders"
                };
                string errMsg = JsonConvert.SerializeObject(err);

                return BadRequest(err);
            }
            _context.PurchaseOrder.Remove(purchaseOrder);
            _context.SaveChanges();
            this.UpdatePurchaseOrder(purchaseOrder.PurchaseOrderId);
            return Ok(purchaseOrder);

        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Print([FromRoute]int id)
        {
            PurchaseOrder purchaseOrder = await _context.PurchaseOrder
                .Where(x => x.PurchaseOrderId.Equals(id))
                .Include(x => x.PurchaseOrderLines)
                    .ThenInclude(x => x.Product)
                .Include(x => x.Vendor)
                .FirstOrDefaultAsync();

            string path = "./Views/EmailViewModels/PurchaseRequest.html";


            string body = string.Empty;
            using (StreamReader reader = new StreamReader(path))
            {
                body = reader.ReadToEnd();
            }
            string drugrow = "";
            int num = 1;
            foreach (var batch in purchaseOrder.PurchaseOrderLines)
            {
                string drug = "<tr>" +
                                   "<td class='text-center'>" + num + " </td>" +
                                   "<td class='text-center'>" + batch.Product.ProductName + "</td>" +
                                   "<td class='text-center'>" + batch.Quantity + "</td>" +
                                   "<td class='text-center'>" + batch.Price + "</td>" +
                                   "<td class='text-center'>" + batch.Amount + "</td>" +
                               "</tr>";
                drugrow = drugrow + drug;
                num++;
            }
            body = body.Replace("{PurchaseOrderName}", purchaseOrder.PurchaseOrderName);
            body = body.Replace("{Vname}", purchaseOrder.Vendor.VendorName);
            body = body.Replace("{Vaddress}", purchaseOrder.Vendor.Address);
            body = body.Replace("{Vcity}", purchaseOrder.Vendor.City);
            body = body.Replace("{OrderDate}", purchaseOrder.OrderDate.ToString("dd MMMM yyyy"));
            body = body.Replace("{DeliveryDate}", purchaseOrder.DeliveryDate.ToString("dd MMMM yyyy"));
            body = body.Replace("{SubTotal}", purchaseOrder.SubTotal.ToString());
            body = body.Replace("{Discount}", purchaseOrder.Discount.ToString());
            body = body.Replace("{Tax}", purchaseOrder.Tax.ToString());
            body = body.Replace("{Total}", purchaseOrder.Total.ToString());
            body = body.Replace("{List}", drugrow);

            return Ok(body);
        }
    }
}