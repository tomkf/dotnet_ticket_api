using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ticketApi.Models.Tickets;

namespace ticketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketPurchasesController : ControllerBase
    {
        private readonly dotnetapiContext _context;

        public TicketPurchasesController(dotnetapiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Return all TicketPurchase items.
        /// </summary>
        // GET: api/TicketPurchases
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketPurchase>>> GetTicketPurchase()
        {
            return await _context.TicketPurchase.ToListAsync();
        }

        /// <summary>
        /// Get a single TicketPurchase itme by id.
        /// </summary> 
        /// <param name="id"></param>
        // GET: api/TicketPurchases/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketPurchase>> GetTicketPurchase(int id)
        {
            var ticketPurchase = await _context.TicketPurchase.FindAsync(id);

            if (ticketPurchase == null)
            {
                return NotFound();
            }

            return ticketPurchase;
        }

        /// <summary>
        /// Update a single TicketPurchase item by id.
        /// </summary>
        /// <remarks>
        /// Format:
        ///
        ///     PUT /TicketPurchase
        ///     {
        ///        "PurchaseId": 1,
        ///        "PaymentMethod": "card type",
        ///        "PaymentAmount": 0.00,
        ///        "ConfirmationCode": "comfirmcode1"
        ///     }
        /// </remarks> 
        /// <param name="id"></param>
        /// <param name="ticketPurchase"></param>
        // PUT: api/TicketPurchases/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicketPurchase(int id, TicketPurchase ticketPurchase)
        {
            if (id != ticketPurchase.PurchaseId)
            {
                return BadRequest();
            }

            _context.Entry(ticketPurchase).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketPurchaseExists(id))
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

        /// <summary>
        /// Creates a new TicketPurchase item.
        /// </summary>
        /// <remarks>
        /// Format:
        ///
        ///     POST /TicketPurchase
        ///     {
        ///        "PurchaseId": 1,
        ///        "PaymentMethod": "card type",
        ///        "PaymentAmount": 0.00,
        ///        "ConfirmationCode": "comfirmcode1"
        ///     }
        /// </remarks>
        /// <param name="ticketPurchase"></param>
        /// <returns>A newly created item</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is not saved</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // POST: api/TicketPurchases
        [HttpPost]
        public async Task<ActionResult<TicketPurchase>> PostTicketPurchase(TicketPurchase ticketPurchase)
        {
            _context.TicketPurchase.Add(ticketPurchase);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TicketPurchaseExists(ticketPurchase.PurchaseId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTicketPurchase", new { id = ticketPurchase.PurchaseId }, ticketPurchase);
        }

        /// <summary>
        /// Deletes a TicketPurchase item by id.
        /// </summary> 
        /// <param name="id"></param>
        // DELETE: api/TicketPurchases/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TicketPurchase>> DeleteTicketPurchase(int id)
        {
            var ticketPurchase = await _context.TicketPurchase.FindAsync(id);
            if (ticketPurchase == null)
            {
                return NotFound();
            }

            _context.TicketPurchase.Remove(ticketPurchase);
            await _context.SaveChangesAsync();

            return ticketPurchase;
        }

        private bool TicketPurchaseExists(int id)
        {
            return _context.TicketPurchase.Any(e => e.PurchaseId == id);
        }
    }
}
