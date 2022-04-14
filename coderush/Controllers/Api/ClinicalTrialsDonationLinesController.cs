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
            var productHeader = Request.Headers["product"];
            var monthsHeader = Request.Headers["months"];
            int ClinicalTrialsDonationId = Convert.ToInt32(headers);
            int ClinicalTrialsProductId = Convert.ToInt32(productHeader);
            int months = Convert.ToInt32(monthsHeader);

            if (months != 0)
            {
                DateTime current = DateTime.Now;
                List<ClinicalTrialsDonationLine> drugs = await _context.ClinicalTrialsDonationLine
                                .Where(x => x.InStock > 0)
                                .ToListAsync();
                List<ClinicalTrialsDonationLine> Items = new List<ClinicalTrialsDonationLine>();

                foreach (ClinicalTrialsDonationLine drug in drugs)
                {
                    Double TotalDays = (drug.ExpiryDate - current).TotalDays;

                    if (TotalDays <= 90)
                    {
                        Items.Add(drug);
                    }
                }
                int Count = Items.Count();
                return Ok(new { Items, Count });
            }
            else if (ClinicalTrialsProductId != 0)
            {
                List<ClinicalTrialsDonationLine> Items = await _context.ClinicalTrialsDonationLine
                .Where(x => x.ClinicalTrialsProductsId.Equals(ClinicalTrialsProductId))
                .ToListAsync();
                int Count = Items.Count();
                return Ok(new { Items, Count });
            }
            else if (ClinicalTrialsDonationId != 0)
            {
                List<ClinicalTrialsDonationLine> Items = await _context.ClinicalTrialsDonationLine
                .Where(x => x.ClinicalTrialsDonationId.Equals(ClinicalTrialsDonationId))
                .ToListAsync();
                int Count = Items.Count();
                return Ok(new { Items, Count });
            }
            else
            {
                List<ClinicalTrialsDonationLine> Items = await _context.ClinicalTrialsDonationLine
                .ToListAsync();
                int Count = Items.Count();
                return Ok(new { Items, Count });
            }
        }

        // GET: api/ClinicalTrialsDonationLines/5
        public async Task<IActionResult> GetById(int id)
        {
            ClinicalTrialsDonationLine result = await _context.ClinicalTrialsDonationLine
                .Where(x => x.ClinicalTrialsProductsId.Equals(id))
                .FirstOrDefaultAsync();

            return Ok(result);
        }
        // /api/ClinicalTrialsDonationLines/GetInstock
        [HttpGet("[action]")]
        public async Task<IActionResult> GetInstock()
        {
            List<ClinicalTrialsDonationLine> result = await _context.ClinicalTrialsDonationLine
                .Where(x => x.InStock != 0)
                .ToListAsync();

            return Ok(result);
        }
        [HttpPost("[action]")]
        public IActionResult Insert([FromBody] CrudViewModel<ClinicalTrialsDonationLine> payload)
        {
            ClinicalTrialsDonationLine clinicalTrialsDonationLine = payload.value;
            DateTime current = DateTime.Now;
            double totaldays = (clinicalTrialsDonationLine.ExpiryDate - current).TotalDays;

            // if (totaldays > 360)
            // {
            clinicalTrialsDonationLine.InStock = clinicalTrialsDonationLine.Quantity;
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
        public IActionResult Update([FromBody] CrudViewModel<ClinicalTrialsDonationLine> payload)
        {
            ClinicalTrialsDonationLine clinicalTrialsDonationLine = payload.value;
            DateTime current = DateTime.Now;
            double totaldays = (clinicalTrialsDonationLine.ExpiryDate - current).TotalDays;

            //if (totaldays > 360)
            //{
            List<ClinicalTrialsSalesLine> lines = new List<ClinicalTrialsSalesLine>();
            lines = _context.ClinicalTrialsSalesLine.Where(x => x.ClinicalTrialsDonationLineId.Equals(clinicalTrialsDonationLine.ClinicalTrialsDonationLineId)).ToList();

            clinicalTrialsDonationLine.Sold = lines.Sum(x => x.Quantity);
            clinicalTrialsDonationLine.InStock = clinicalTrialsDonationLine.Quantity - clinicalTrialsDonationLine.Sold - clinicalTrialsDonationLine.Expired;
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
        public IActionResult Remove([FromBody] CrudViewModel<ClinicalTrialsDonationLine> payload)
        {
            ClinicalTrialsDonationLine clinicalTrialsDonationLine = _context.ClinicalTrialsDonationLine
                .Where(x => x.ClinicalTrialsDonationLineId == (int)payload.key)
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
                ClinicalTrialsProduct stock = new ClinicalTrialsProduct();
                stock = _context.ClinicalTrialsProducts
                    .Where(x => x.ClinicalTrialsProductId.Equals(productId))
                    .FirstOrDefault();

                if (stock != null)
                {
                    List<ClinicalTrialsDonationLine> line = new List<ClinicalTrialsDonationLine>();
                    line = _context.ClinicalTrialsDonationLine.Where(x => x.ClinicalTrialsProductsId.Equals(productId)).ToList();

                    stock.TotalRecieved = line.Sum(x => x.Quantity);
                    stock.Expired = line.Sum(x => x.Expired);

                    List<ClinicalTrialsSalesLine> lines = new List<ClinicalTrialsSalesLine>();
                    lines = _context.ClinicalTrialsSalesLine.Where(x => x.ClinicalTrialsProductsId.Equals(productId)).ToList();

                    List<ClinicalTrialsReturnedLine> returnedLines = new List<ClinicalTrialsReturnedLine>();
                    returnedLines = _context.ClinicalTrialsReturnedLine.Where(x => x.ClinicalTrialsProductsId.Equals(productId)).ToList();
                    stock.Returned = returnedLines.Sum(x => x.Quantity);

                    stock.TotalSales = lines.Sum(x => x.Quantity);


                    if (stock.TotalRecieved < stock.TotalSales)
                    {
                        stock.Deficit = stock.TotalSales - stock.TotalRecieved - stock.Returned;
                        stock.InStock = 0;
                    }
                    else
                    {
                        stock.InStock =  stock.TotalRecieved - stock.TotalSales - stock.Expired - stock.Returned;
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
        [HttpPost("[action]")]
        public IActionResult Expired([FromBody] CrudViewModel<ClinicalTrialsDonationLine> payload)
        {
            ClinicalTrialsDonationLine clinicalTrialsDonationLine = payload.value;

            if (clinicalTrialsDonationLine.InStock < clinicalTrialsDonationLine.Expired)
            {
                Err err = new Err
                {
                    message = "Expired Stock cannot be more than stock"
                };
                string errMsg = JsonConvert.SerializeObject(err);

                return BadRequest(err);
            }
            if (clinicalTrialsDonationLine.Dispose)
            {
                clinicalTrialsDonationLine.Expired = clinicalTrialsDonationLine.InStock;
                clinicalTrialsDonationLine.InStock = 0;
            }
           
            _context.ClinicalTrialsDonationLine.Update(clinicalTrialsDonationLine);
            _context.SaveChanges();
            this.UpdateStock(clinicalTrialsDonationLine.ClinicalTrialsProductsId);
          

            return Ok(clinicalTrialsDonationLine);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetExpired()
        {
            List<ClinicalTrialsDonationLine> clinicalTrialsDonationLine = await _context.ClinicalTrialsDonationLine
                                                            .Where(x => x.InStock > 0)
                                                            .ToListAsync();
            DateTime current = DateTime.Now;
            ExpiredDrugs expiredDrugs = new ExpiredDrugs()
            {
                one = 0,
                two = 0,
                three = 0
            };

            foreach (ClinicalTrialsDonationLine drug in clinicalTrialsDonationLine)
            {
                Double months = (drug.ExpiryDate - current).TotalDays;

                if (months < 30)
                {
                    expiredDrugs.one = expiredDrugs.one + 1;
                }
                else if (months < 60 )
                {
                    expiredDrugs.two = expiredDrugs.two + 1;
                }
                else if (months < 90)
                {
                    expiredDrugs.three = expiredDrugs.three + 1;

                }
            }

            return Ok(expiredDrugs);
        }
    }
}