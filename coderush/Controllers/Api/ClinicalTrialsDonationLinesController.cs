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
using Newtonsoft.Json;

namespace coderush.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/ClinicalTrialsDonationLines")]
    public class ClinicalTrialsDonationLinesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClinicalTrialsDonationLinesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ClinicalTrialsDonationLines
        [HttpGet]
        public async Task<IActionResult> GetGoodsRecievedNoteLines()
        {
            var headers = Request.Headers["ClinicalTrialsDonationId"];
            int ClinicalTrialsDonationId = Convert.ToInt32(headers);
            List<ClinicalTrialsDonationLine> Items = await _context.ClinicalTrialsDonationLine
            .Where(x => x.ClinicalTrialsDonationId.Equals(ClinicalTrialsDonationId))
            .ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        // GET: api/ClinicalTrialsDonationLines/5
        public async Task<IActionResult> GetById(int id)
        {
            ClinicalTrialsDonationLine result = await _context.ClinicalTrialsDonationLine
                .Where(x => x.ClinicalTrialsProductsId.Equals(id))
                .FirstOrDefaultAsync();

            return Ok(result);
        }
        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<ClinicalTrialsDonationLine> payload)
        {
            ClinicalTrialsDonationLine clinicalTrialsDonationLine = payload.value;
            DateTime current = DateTime.Now;
            double totaldays = (clinicalTrialsDonationLine.ExpiryDate - current).TotalDays;

           // if (totaldays > 360)
           // {
                _context.ClinicalTrialsDonationLine.Add(clinicalTrialsDonationLine);
                _context.SaveChanges();
            this.UpdateStock(clinicalTrialsDonationLine.ClinicalTrialsProductsId);
            //}
            //else if (totaldays < 360)
            //{
            //    Err err = new Err
            //    {

            //        message = "Drug will expire less than one year"
            //    };
            //    string errMsg = JsonConvert.SerializeObject(err);

            //    return BadRequest(err);

            //}
            return Ok(clinicalTrialsDonationLine);
        }

        // POST: api/ClinicalTrialsDonationLines
        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<ClinicalTrialsDonationLine> payload)
        {
            ClinicalTrialsDonationLine clinicalTrialsDonationLine = payload.value;
            DateTime current = DateTime.Now;
            double totaldays = (clinicalTrialsDonationLine.ExpiryDate - current).TotalDays;

            //if (totaldays > 360)
            //{
                _context.ClinicalTrialsDonationLine.Update(clinicalTrialsDonationLine);
                _context.SaveChanges();
            this.UpdateStock(clinicalTrialsDonationLine.ClinicalTrialsProductsId);
            //}
            //else if (totaldays < 360)
            //{
            //    Err err = new Err
            //    {
            //        message = "Drug will expire less than one year"
            //    };
            //    string errMsg = JsonConvert.SerializeObject(err);

            //    return BadRequest(err);

            //}

            return Ok(clinicalTrialsDonationLine);
        }

        // DELETE: api/ClinicalTrialsDonationLines/5
        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<ClinicalTrialsDonationLine> payload)
        {
            ClinicalTrialsDonationLine clinicalTrialsDonationLine = _context.ClinicalTrialsDonationLine
                .Where(x => x.ClinicalTrialsDonationId == (int)payload.key)
                .FirstOrDefault();
            _context.ClinicalTrialsDonationLine.Remove(clinicalTrialsDonationLine);
            _context.SaveChanges();
           this.UpdateStock(clinicalTrialsDonationLine.ClinicalTrialsProductsId);
            return Ok(clinicalTrialsDonationLine);

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