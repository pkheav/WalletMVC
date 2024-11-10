using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WalletMVC.Models;

namespace WalletMVC.Controllers
{
    [Route("api/Transactions")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly WalletContext _context;

        public TransactionsController(WalletContext context)
        {
            _context = context;
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionDTO>>> GetTransactions()
        {
            return await _context.Transactions
                .Select(x => TransactionToDTO(x))
                .ToListAsync();
        }

        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDTO>> GetTransaction(long id)
        {
            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return TransactionToDTO(transaction);
        }

        // PUT: api/Transactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(long id, TransactionDTO txnDTO)
        {
            if (id != txnDTO.Id)
            {
                return BadRequest();
            }

            _context.Entry(txnDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Transactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(TransactionDTO txnDTO)
        {
            var transaction = new Transaction
            {
                Amount = txnDTO.Amount,
                Description = txnDTO.Description
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            // return CreatedAtAction("GetTransaction", new { id = transaction.Id }, transaction);
            return CreatedAtAction(nameof(GetTransaction), new { id = transaction.Id }, transaction);
        }

        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(long id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransactionExists(long id)
        {
            return _context.Transactions.Any(e => e.Id == id);
        }

        private static TransactionDTO TransactionToDTO(Transaction transaction) => new TransactionDTO
        {
            Id = transaction.Id,
            Amount = transaction.Amount,
            Description = transaction.Description
        };
    }
}
