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
    [Route("api/RFPCustomers")]
    public class RFPCustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RFPCustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/RFPCustomers
        [HttpGet]
        public async Task<IActionResult> GetCustomer()
        {
            List<RFPCustomer> Items = await _context.RFPCustomer.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetbyId([FromRoute] int id)
        {
            RFPCustomer Items = await _context.RFPCustomer
                .Where(x => x.RFPCustomerId == id)
                .FirstOrDefaultAsync();

            return Ok(Items);
        }
        [HttpGet("[action]/{name}")]
        public async Task<IActionResult> GetByName([FromRoute] string name)
        {
            var RFPCustomer = from p in _context.RFPCustomer
                              select p;

            if (!String.IsNullOrEmpty(name))
            {
                RFPCustomer = RFPCustomer.Where(x => x.RFPCustomerName.Contains(name));
            }

            List<RFPCustomer> Items = await RFPCustomer.ToListAsync();

            return Ok(new { Items });
        }



        [HttpPost("[action]")]
        public IActionResult Insert([FromBody] CrudViewModel<RFPCustomer> payload)
        {
            RFPCustomer customer = payload.value;
            _context.RFPCustomer.Add(customer);
            _context.SaveChanges();
            return Ok(customer);
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody] CrudViewModel<RFPCustomer> payload)
        {
            RFPCustomer customer = payload.value;
            _context.RFPCustomer.Update(customer);
            _context.SaveChanges();
            return Ok(customer);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody] CrudViewModel<RFPCustomer> payload)
        {
            RFPCustomer customer = _context.RFPCustomer
                .Where(x => x.RFPCustomerId == (int)payload.key)
                .FirstOrDefault();
            _context.RFPCustomer.Remove(customer);
            _context.SaveChanges();
            return Ok(customer);

        }
    }
}