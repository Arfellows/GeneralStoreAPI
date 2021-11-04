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
    public class CustomerController : ApiController
    {
        private readonly GeneralStoreDbContext _context = new GeneralStoreDbContext();
        //POST
        [HttpPost]
        public async Task<IHttpActionResult> CreateCustomer([FromBody]Customer model)
        {
            if(ModelState.IsValid)
            {
                _context.Customers.Add(model);
                await _context.SaveChangesAsync();
                return Ok("Customer was added.");
            }
            return BadRequest(ModelState);
        }

        //GET ALL CUSTOMERS
        [HttpGet]
        public async Task<IHttpActionResult> GetAllCustomers()
        {
            List<Customer> customers = await _context.Customers.ToListAsync();
            return Ok(customers);
        }

        //GET CUSTOMER BY ID
        [HttpGet]
        public async Task<IHttpActionResult> GetCustomerById([FromUri]int id)
        {
            Customer customer = await _context.Customers.FindAsync(id);
            if(customer != null)
            {
                return Ok(customer);
            }
            return NotFound();
        }

        //UPDATE BY ID
        [HttpPut]
        public async Task<IHttpActionResult> UpdateCustomer([FromUri] int id, [FromBody] Customer updatedCustomer)
        {
            if(id != updatedCustomer?.Id)
            {
                return BadRequest("The IDs do not match.");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Customer customer = await _context.Customers.FindAsync(id);

            if (customer is null)
                return NotFound();

            customer.FirstName = updatedCustomer.FirstName;
            customer.LastName = updatedCustomer.LastName;

            await _context.SaveChangesAsync();
            return Ok("Your customer was successfully updated!");
        }

        //DELETE BY ID
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCustomer([FromUri] int id)
        {
            Customer customer = await _context.Customers.FindAsync(id);

            if (customer is null)
                return NotFound();

            _context.Customers.Remove(customer);

            if(await _context.SaveChangesAsync() == 1)
            {
                return Ok("Your customer was removed!");
            }
            return InternalServerError();
        }
    }
}
