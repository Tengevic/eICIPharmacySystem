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
    [Route("api/ClinicalTrialsProducts")]
    public class ClinicalTrialsProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClinicalTrialsProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ClinicalTrialsProducts
        [HttpGet]
        public async Task<IActionResult> GetClinicalTrialsProducts()
        {
           List<ClinicalTrialsProduct> Items = await _context.ClinicalTrialsProducts.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }

        // GET: api/ClinicalTrialsProducts/5
        [HttpGet("[action]/{name}")]
        public async Task<IActionResult> GetByClinicalTrialProductName([FromRoute] string name)
        {
            var product = from p in _context.ClinicalTrialsProducts
                          select p;

            if (!String.IsNullOrEmpty(name))
            {
                product = product.Where(x => x.ProductName.Contains(name));
            }

            List<ClinicalTrialsProduct> Items = await product.ToListAsync();

            return Ok(new { Items });
        }

        // PUT: api/ClinicalTrialsProducts/5
        [HttpPost("[action]")]
        public IActionResult Insert([FromBody] CrudViewModel<ClinicalTrialsProduct> payload)
        {
            ClinicalTrialsProduct products = payload.value;
            _context.ClinicalTrialsProducts.Add(products);
                   _context.SaveChanges();
            return Ok(products);
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody] CrudViewModel<ClinicalTrialsProduct> payload)
        {
            ClinicalTrialsProduct product = payload.value;
            _context.ClinicalTrialsProducts.Update(product);
            _context.SaveChanges();
            return Ok(product);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody] CrudViewModel<ClinicalTrialsProduct> payload)
        {
            ClinicalTrialsProduct product = _context.ClinicalTrialsProducts
                .Where(x => x.ClinicalTrialsProductId == (int)payload.key)
                .FirstOrDefault();
            _context.ClinicalTrialsProducts.Remove(product);
            _context.SaveChanges();
            return Ok(product);

        }
    }
}