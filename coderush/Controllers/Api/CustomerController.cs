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
using Microsoft.AspNetCore.Authorization;

namespace coderush.Controllers.Api
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Customer")]
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<IActionResult> GetCustomer()
        {
            List<Customer> Items = await _context.Customer.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetbyId([FromRoute] int id)
        {
            Customer Items = await _context.Customer
                .Where(x => x.CustomerId == id)
                .FirstOrDefaultAsync();

            return Ok(Items);
        }
        [HttpGet("[action]/{name}")]
        public async Task<IActionResult> GetByName([FromRoute] string name)
        {
            var Customer = from p in _context.Customer
                           select p;

            if (!String.IsNullOrEmpty(name))
            {
                Customer = Customer.Where(x => x.CustomerName.Contains(name));
            }

            List<Customer> Items = await Customer.ToListAsync();

            return Ok(new { Items });
        }



        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<Customer> payload)
        {
            Customer customer = payload.value;
            _context.Customer.Add(customer);
            _context.SaveChanges();
            return Ok(customer);
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<Customer> payload)
        {
            Customer customer = payload.value;
            _context.Customer.Update(customer);
            _context.SaveChanges();
            return Ok(customer);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<Customer> payload)
        {
            Customer customer = _context.Customer
                .Where(x => x.CustomerId == (int)payload.key)
                .FirstOrDefault();
            _context.Customer.Remove(customer);
            _context.SaveChanges();
            return Ok(customer);

        }
    }
}