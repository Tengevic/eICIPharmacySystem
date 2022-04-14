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
    [Route("api/ClinicalTrialsReturnedLines")]
    public class ClinicalTrialsReturnedLinesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClinicalTrialsReturnedLinesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ClinicalTrialsReturnedLines
        [HttpGet]
        public async Task<IActionResult> ClinicalTrialsSalesLines()
        {
            var headers = Request.Headers["ClinicalTrialsReturnedId"];
            int ClinicalTrialsReturnedId = Convert.ToInt32(headers);
            List<ClinicalTrialsReturnedLine> Items = await _context.ClinicalTrialsReturnedLine
            .Where(x => x.ClinicalTrialsReturnedId.Equals(ClinicalTrialsReturnedId))
            .ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });


        }

        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<ClinicalTrialsReturnedLine> payload)
        {
            ClinicalTrialsReturnedLine clinicalTrialsReturnedLine = payload.value;
            ClinicalTrialsDonationLine batch = _context.ClinicalTrialsDonationLine.Find(clinicalTrialsReturnedLine.ClinicalTrialsDonationLineId);

            if (batch.ClinicalTrialsProductsId != clinicalTrialsReturnedLine.ClinicalTrialsProductsId)
            {
                Err err = new Err
                {
                    message = "Product doesn't have the batchId"
                };
                string errMsg = JsonConvert.SerializeObject(err);

                return BadRequest(err);
            }
            if (batch.InStock < clinicalTrialsReturnedLine.Quantity)
            {
                Err err = new Err
                {
                    message = $"Stock available for this Batch is {batch.InStock} ",
                };
                string errMsg = JsonConvert.SerializeObject(err);

                return BadRequest(err);
            }
            _context.ClinicalTrialsReturnedLine.Add(clinicalTrialsReturnedLine);
            _context.SaveChanges();
            this.UpdateStock(clinicalTrialsReturnedLine.ClinicalTrialsProductsId);
            this.UpdateBatch(clinicalTrialsReturnedLine.ClinicalTrialsDonationLineId);
            return Ok(clinicalTrialsReturnedLine);

        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<ClinicalTrialsReturnedLine> payload)
        {
            ClinicalTrialsReturnedLine clinicalTrialsReturnedLine = payload.value;
            ClinicalTrialsDonationLine batch = _context.ClinicalTrialsDonationLine.Find(clinicalTrialsReturnedLine.ClinicalTrialsDonationLineId);

            if (batch.ClinicalTrialsProductsId != clinicalTrialsReturnedLine.ClinicalTrialsProductsId)
            {
                Err err = new Err
                {
                    message = "Product doesn't have the batchId"
                };
                string errMsg = JsonConvert.SerializeObject(err);

                return BadRequest(err);
            }
            if (batch.InStock < clinicalTrialsReturnedLine.Quantity)
            {
                Err err = new Err
                {
                    message = $"Stock available for this Batch is {batch.InStock} ",
                };
                string errMsg = JsonConvert.SerializeObject(err);

                return BadRequest(err);
            }
            _context.ClinicalTrialsReturnedLine.Update(clinicalTrialsReturnedLine);
            _context.SaveChanges();
            this.UpdateStock(clinicalTrialsReturnedLine.ClinicalTrialsProductsId);
            this.UpdateBatch(clinicalTrialsReturnedLine.ClinicalTrialsDonationLineId);
            return Ok(clinicalTrialsReturnedLine);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<ClinicalTrialsReturnedLine> payload)
        {
            ClinicalTrialsReturnedLine clinicalTrialsReturnedLine = _context.ClinicalTrialsReturnedLine
                .Where(x => x.ClinicalTrialsReturnedId == (int)payload.key)
                .FirstOrDefault();
            _context.ClinicalTrialsReturnedLine.Remove(clinicalTrialsReturnedLine);
            _context.SaveChanges();
            this.UpdateStock(clinicalTrialsReturnedLine.ClinicalTrialsProductsId);
            this.UpdateBatch(clinicalTrialsReturnedLine.ClinicalTrialsDonationLineId);
            return Ok(clinicalTrialsReturnedLine);

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
                        stock.Deficit = stock.TotalSales - stock.TotalRecieved;
                        stock.InStock = 0;
                    }
                    else
                    {
                        stock.InStock = stock.TotalRecieved - stock.TotalSales - stock.Expired - stock.Returned;
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
        private void UpdateBatch(int batchId)
        {
            try
            {
                ClinicalTrialsDonationLine batch = _context.ClinicalTrialsDonationLine.Where(x => x.ClinicalTrialsDonationLineId.Equals(batchId))
                    .FirstOrDefault();
                if (batch != null)
                {
                    List<ClinicalTrialsSalesLine> lines = new List<ClinicalTrialsSalesLine>();
                    lines = _context.ClinicalTrialsSalesLine.Where(x => x.ClinicalTrialsDonationLineId.Equals(batch.ClinicalTrialsDonationLineId)).ToList();

                    List<ClinicalTrialsReturnedLine> returnedLines = new List<ClinicalTrialsReturnedLine>();
                    returnedLines = _context.ClinicalTrialsReturnedLine.Where(x => x.ClinicalTrialsDonationLineId.Equals(batch.ClinicalTrialsDonationLineId)).ToList();
                    batch.Returned = returnedLines.Sum(x => x.Quantity);

                    batch.Sold = lines.Sum(x => x.Quantity);
                    batch.InStock = batch.Quantity - batch.Sold - batch.Expired - batch.Returned;

                    _context.Update(batch);

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