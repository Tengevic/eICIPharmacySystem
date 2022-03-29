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

namespace coderush.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/ClinicalTrialsSalesLines")]
    public class ClinicalTrialsSalesLinesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClinicalTrialsSalesLinesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ClinicalTrialsSalesLines
        [HttpGet]
        public async Task<IActionResult> GetSalesOrderLine()
        {
            var headers = Request.Headers["ClinicalTrialsSalesId"];
            int ClinicalTrialsSalesId = Convert.ToInt32(headers);
            List<ClinicalTrialsSalesLine> Items = await _context.ClinicalTrialsSalesLine
            .Where(x => x.ClinicalTrialsSalesId.Equals(ClinicalTrialsSalesId))
            .ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });


        }

        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<ClinicalTrialsSalesLine> payload)
        {
            ClinicalTrialsSalesLine clinicalTrialsSalesLine = payload.value;
            _context.ClinicalTrialsSalesLine.Add(clinicalTrialsSalesLine);
            _context.SaveChanges();
            this.UpdateStock(clinicalTrialsSalesLine.ClinicalTrialsProductsId);
            return Ok(clinicalTrialsSalesLine);

        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<ClinicalTrialsSalesLine> payload)
        {
            ClinicalTrialsSalesLine clinicalTrialsSalesLine = payload.value;
           
            _context.ClinicalTrialsSalesLine.Update(clinicalTrialsSalesLine);
            _context.SaveChanges();
            this.UpdateStock(clinicalTrialsSalesLine.ClinicalTrialsProductsId);
            return Ok(clinicalTrialsSalesLine);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<ClinicalTrialsSalesLine> payload)
        {
            ClinicalTrialsSalesLine clinicalTrialsSalesLine = _context.ClinicalTrialsSalesLine
                .Where(x => x.ClinicalTrialsSalesLineId == (int)payload.key)
                .FirstOrDefault();
            _context.ClinicalTrialsSalesLine.Remove(clinicalTrialsSalesLine);
            _context.SaveChanges();
           this.UpdateStock(clinicalTrialsSalesLine.ClinicalTrialsProductsId);
            return Ok(clinicalTrialsSalesLine);

        }
        private void UpdateStock(int productId)
        {
            try
            {
                ClinicalTrialsStock stock = new ClinicalTrialsStock();
                stock = _context.ClinicalTrialsStock
                    .Where(x => x.ClinicalTrialsProductsId.Equals(productId))
                    .FirstOrDefault();

                if (stock != null)
                {
                    List<ClinicalTrialsDonationLine> line = new List<ClinicalTrialsDonationLine>();
                    line = _context.ClinicalTrialsDonationLine.Where(x => x.ClinicalTrialsProductsId.Equals(productId)).ToList();

                    stock.TotalRecieved = line.Sum(x => x.Quantity);

                    List<ClinicalTrialsSalesLine> lines = new List<ClinicalTrialsSalesLine>();
                    lines = _context.ClinicalTrialsSalesLine.Where(x => x.ClinicalTrialsProductsId.Equals(productId)).ToList();

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
    }
}