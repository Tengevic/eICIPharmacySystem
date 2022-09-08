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
using coderush.Models.AtMViewModels;

namespace coderush.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Product")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            List<Product> Items = await _context.Product.OrderBy(x =>x.ProductName).ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAtMProduct()
        {
            List<Product> Items = await _context.Product
                .Where(x => x.IsAtM == true)
                .OrderBy(x => x.ProductName)
                .ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductbyId([FromRoute] int id)
        {
            Product Items = await _context.Product
                .Include(x => x.UnitOfMeasure)
                .Include(x =>x.ProductType)
                .Where(x => x.ProductId == id)
                .FirstOrDefaultAsync();
           
            return Ok(Items);
        }
        [HttpGet("[action]/{name}")]
        public async Task<IActionResult> GetByProductName([FromRoute] string name)
        {
            var product = from p in _context.Product
                          select p;
            product = product.Include(x => x.UnitOfMeasure).Where(x => x.IsAtM == true);
            if (!String.IsNullOrEmpty(name))
            {
                product = product.Where(x => x.ProductName.Contains(name));
            }

            List<Product> items = await product.ToListAsync();
            List<ProductAtMVM> Items = new List<ProductAtMVM>();

            foreach(Product item in items)
            {
                ProductAtMVM Item = new ProductAtMVM
                {
                    ProductName = item.ProductName,
                    BrandName = item.BrandName,
                    DefaultSellingPrice = item.DefaultSellingPrice,
                    InStock = item.InStock,
                    UnitOfMeasureName = item.UnitOfMeasure.UnitOfMeasureName
                };
                var GoodsRecievedNoteLine = from p in _context.GoodsRecievedNoteLine
                                            select p;
                int id = item.ProductId;
                GoodsRecievedNoteLine = GoodsRecievedNoteLine.Where(x => x.ProductId.Equals(id));
                GoodsRecievedNoteLine[] batch = GoodsRecievedNoteLine
                         .Where(x => x.InStock > 0)
                         .OrderBy(x => x.ExpiryDate)
                         .ToArray();
                if(batch.Length != 0)
                {
                    Item.ExpiryDate = batch[0].ExpiryDate;
                }
                Items.Add(Item);
            }


            return Ok(new {Items});
        }


        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<Product> payload)
        {
            Product product = payload.value;
            _context.Product.Add(product);
            _context.SaveChanges();
            return Ok(product);
        }
        [HttpPost("[action]")]
        public IActionResult Add([FromBody] Product payload)
        {
            Product product = payload;
            _context.Product.Add(product);
            _context.SaveChanges();
            return Ok(product);
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<Product> payload)
        {
            Product product = payload.value;
            _context.Product.Update(product);
            _context.SaveChanges();
            return Ok(product);
        }
        [HttpPost("[action]")]
        public IActionResult Put([FromBody] Product payload)
        {
            Product product = payload;
            _context.Product.Update(product);
            _context.SaveChanges();
            return Ok(product);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<Product> payload)
        {
            Product product = _context.Product
                .Where(x => x.ProductId == (int)payload.key)
                .FirstOrDefault();
            _context.Product.Remove(product);
            _context.SaveChanges();
            return Ok(product);

        }
        [HttpPost("[action]/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            Product product = _context.Product
                .Where(x => x.ProductId == id)
                .FirstOrDefault();
            _context.Product.Remove(product);
            _context.SaveChanges();
            return Ok(product);

        }
        // api/Product/GetLow
        [HttpGet("[action]")]
        public async Task<IActionResult> GetLow()
        {
            var product = from p in _context.Product
                          select p;

            product = product.Where(x => x.InStock < 10);


            List<Product> Items = await product.ToListAsync();
            int Count = Items.Count();

            return Ok(new { Items, Count });
        }
        [HttpPost("[action]/{id}")]
        public IActionResult UpdateStock([FromRoute] int id)
        {
            Product stock = new Product();
            stock = _context.Product
                .Where(x => x.ProductId.Equals(id))
                .FirstOrDefault();

            if (stock != null)
            {
                List<GoodsRecievedNoteLine> batch = new List<GoodsRecievedNoteLine>();
                batch = _context.GoodsRecievedNoteLine.Where(x => x.ProductId.Equals(id)).ToList();
                stock.TotalRecieved = batch.Sum(x => x.Quantity);
                stock.ExpiredStock = batch.Sum(x => x.Expired);
                stock.TotalSales = batch.Sum(x => x.Sold);
                stock.InStock = batch.Sum(x => x.InStock);
                stock.Deficit = batch.Sum(x => x.changestock);

                _context.Update(stock);

                _context.SaveChanges();
            }
            return Ok(stock);
        }
    }

}