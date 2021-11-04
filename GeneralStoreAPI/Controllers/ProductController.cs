using GeneralStore.Data;
using GeneralStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GeneralStoreAPI.Controllers
{
    public class ProductController : ApiController
    {
        private readonly GeneralStoreDbContext _context = new GeneralStoreDbContext();
        //POST
        [HttpPost]
        public async Task<IHttpActionResult> CreateProduct([FromBody] Product model)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(model);
                await _context.SaveChangesAsync();
                return Ok("Product was added.");
            }
            return BadRequest(ModelState);
        }

        //GET ALL PRODUCTS
        [HttpGet]
        public async Task<IHttpActionResult> GetAllProducts()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        //GET PRODUCT BY ID
        [HttpGet]
        public async Task<IHttpActionResult> GetProductById([FromUri] int id)
        {
            Product product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                return Ok(product);
            }
            return NotFound();
        }

        //UPDATE BY SKU
        [HttpPut]
        public async Task<IHttpActionResult> UpdateProduct([FromUri] string SKU, [FromBody] Product updatedProduct)
        {
            if (SKU != updatedProduct?.SKU)
            {
                return BadRequest("The SKUs do not match.");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Product product = await _context.Products.FindAsync(SKU);

            if (product is null)
                return NotFound();

            product.Name = updatedProduct.Name;
            product.Cost = updatedProduct.Cost;
            product.NumberInInventory = updatedProduct.NumberInInventory;

            await _context.SaveChangesAsync();
            return Ok("Your product was successfully updated!");
        }

        //DELETE BY SKU
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteProduct([FromUri] string SKU)
        {
            Product product = await _context.Products.FindAsync(SKU);

            if (product is null)
                return NotFound();

            _context.Products.Remove(product);

            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok("Your product was removed!");
            }
            return InternalServerError();
        }
    }
}
