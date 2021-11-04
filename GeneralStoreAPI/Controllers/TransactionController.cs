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
    public class TransactionController : ApiController
    {
        private readonly GeneralStoreDbContext _context = new GeneralStoreDbContext();
        //POST
        //[HttpPost]
        //public async Task<IHttpActionResult> CreateTransaction([FromBody]Transaction model)
        //{
        //    //verify product is in stock

        //    //check that there is enough product for transaction

        //    //remove products that were bought
        //}

        //GET ALL TRANSACTIONS
        [HttpGet]
        public async Task<IHttpActionResult> GetAllTransactions()
        {
            List<Transaction> transactions = await _context.Transactions.ToListAsync();
            return Ok(transactions);
        }

        //GET TRANSACTIONS BY CUSTOMER ID
        [HttpGet]
        public async Task<IHttpActionResult> GetTransactionByCustomerId([FromBody] int customerId)
        {
            Transaction transaction = await _context.Transactions.FindAsync(customerId);
            if(transaction != null)
            {
                return Ok(transaction);
            }
            return NotFound();
        }

        //GET TRANSACTIONS BY ITS ID
        [HttpGet]
        public async Task<IHttpActionResult> GetTransactionByItsId([FromUri] int id)
        {
            Transaction transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                return Ok(transaction);
            }
            return NotFound();
        }

        //UPDATE BY ID
        [HttpPut]
        public async Task<IHttpActionResult> UpdateTransaction([FromUri] int id, [FromBody] Transaction updatedTransaction)
        {
            if (id != updatedTransaction?.Id)
            {
                return BadRequest("The IDs do not match.");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Transaction transaction = await _context.Transactions.FindAsync(id);

            if (transaction is null)
                return NotFound();

            transaction.ProductSKU = updatedTransaction.ProductSKU;
            transaction.ItemCount = updatedTransaction.ItemCount;
            transaction.DateOfTransaction = updatedTransaction.DateOfTransaction;
            transaction.CustomerId = updatedTransaction.CustomerId;

            await _context.SaveChangesAsync();
            return Ok("Your transaction was successfully updated!");
        }

        //DELETE BY ID
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteTransaction([FromUri] int id)
        {
            Transaction transaction = await _context.Transactions.FindAsync(id);

            if (transaction is null)
                return NotFound();

            _context.Transactions.Remove(transaction);

            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok("Your transaction was removed!");
            }
            return InternalServerError();
        }
    }
}
