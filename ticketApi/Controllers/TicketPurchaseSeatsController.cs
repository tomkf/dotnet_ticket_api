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
    public class TicketPurchaseSeatsController : ControllerBase
    {
        private readonly dotnetapiContext _context;

        public TicketPurchaseSeatsController(dotnetapiContext context)
        {
            _context = context;
        }

        /// Return all TicketPurchaseSeat items.
        /// <summary>       
        /// </summary>
        // GET: api/TicketPurchaseSeats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketPurchaseSeat>>> GetTicketPurchaseSeat()
        {
            return await _context.TicketPurchaseSeat.ToListAsync();
        }

        /// <summary>
        /// Get a single TicketPurchaseSeat by id.
        /// </summary> 
        /// <param name="id"></param>
        // GET: api/TicketPurchaseSeats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketPurchaseSeat>> GetTicketPurchaseSeat(int id)
        {
            var ticketPurchaseSeat = await _context.TicketPurchaseSeat.FindAsync(id);

            if (ticketPurchaseSeat == null)
            {
                return NotFound();
            }

            return ticketPurchaseSeat;
        }


        /// <summary>
        /// Update a  single TicketPurchaseSeat item by id.
        /// </summary>
        /// <remarks>
        /// Format:
        ///
        ///     PUT /TicketPurchaseSeat
        ///     {
        ///        "PurchaseId": 1,
        ///        "EventSeatId": 1,
        ///        "SeatSubtotal": 0.00
        ///     }
        /// </remarks> 
        /// <param name="id"></param>
        /// <param name="ticketPurchaseSeat"></param>
        // PUT: api/TicketPurchaseSeats/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicketPurchaseSeat(int id, TicketPurchaseSeat ticketPurchaseSeat)
        {
            if (id != ticketPurchaseSeat.EventSeatId)
            {
                return BadRequest();
            }

            _context.Entry(ticketPurchaseSeat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketPurchaseSeatExists(id))
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
        /// Creates a new TicketPurchaseSeat item.
        /// </summary>
        /// <remarks>
        /// Format:
        ///
        ///     POST /TicketPurchaseSeat
        ///     {
        ///        "PurchaseId": 1,
        ///        "EventSeatId": 1,
        ///        "SeatSubtotal": 0.0
        ///     }
        /// </remarks>
        /// <param name="ticketPurchaseSeat"></param>
        /// <returns>A newly created ticketputrchaseseatitem</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">Error, item not saved.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // POST: api/TicketPurchaseSeats
        [HttpPost]
        public async Task<ActionResult<TicketPurchaseSeat>> PostTicketPurchaseSeat(TicketPurchaseSeat ticketPurchaseSeat)
        {
            _context.TicketPurchaseSeat.Add(ticketPurchaseSeat);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TicketPurchaseSeatExists(ticketPurchaseSeat.EventSeatId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTicketPurchaseSeat", new { id = ticketPurchaseSeat.EventSeatId }, ticketPurchaseSeat);
        }

        /// <summary>
        /// Deletes a single TicketPurchaseSeat item by id.
        /// </summary> 
        /// <param name="id"></param>
        // DELETE: api/TicketPurchaseSeats/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TicketPurchaseSeat>> DeleteTicketPurchaseSeat(int id)
        {
            var ticketPurchaseSeat = await _context.TicketPurchaseSeat.FindAsync(id);
            if (ticketPurchaseSeat == null)
            {
                return NotFound();
            }

            _context.TicketPurchaseSeat.Remove(ticketPurchaseSeat);
            await _context.SaveChangesAsync();

            return ticketPurchaseSeat;
        }

        private bool TicketPurchaseSeatExists(int id)
        {
            return _context.TicketPurchaseSeat.Any(e => e.EventSeatId == id);
        }
    }
}
