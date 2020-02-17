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
    public class EventSeatsController : ControllerBase
    {
        private readonly dotnetapiContext _context;

        public EventSeatsController(dotnetapiContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Return all EventSeats.
        /// </summary>
        // GET: api/EventSeats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventSeat>>> GetEventSeat()
        {
            return await _context.EventSeat.ToListAsync();
        }


        /// <summary>
        /// Get single EventSeat by id.
        /// </summary> 
        /// <param name="id"></param>
        // GET: api/EventSeats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventSeat>> GetEventSeat(int id)
        {
            var eventSeat = await _context.EventSeat.FindAsync(id);

            if (eventSeat == null)
            {
                return NotFound();
            }

            return eventSeat;
        }

        /// <summary>
        /// Update a single EventSeat by Id.
        /// </summary>
        /// <remarks>
        /// Format:
        ///
        ///     PUT /EventSeat
        ///     {
        ///        "EventSeatId": 1,
        ///        "SeatId": 1,
        ///        "EventId": 1,
        ///        "EventSeatPrice": 0.00
        ///     }
        /// </remarks> 
        /// <param name="id"></param>
        /// <param name="eventSeat"></param>
        // PUT: api/EventSeats/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventSeat(int id, EventSeat eventSeat)
        {
            if (id != eventSeat.EventSeatId)
            {
                return BadRequest();
            }

            _context.Entry(eventSeat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventSeatExists(id))
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
        /// Create new EventSeat.
        /// </summary>
        /// <remarks>
        /// Format:
        ///
        ///     POST /EventSeat
        ///     {
        ///        "EventSeatId": 1,
        ///        "SeatId": 1,
        ///        "EventId": 1,
        ///        "EventSeatPrice": 0.00
        ///     }
        /// </remarks>
        /// <param name="eventSeat"></param>
        /// <returns>New EventSeat Item</returns>
        /// <response code="201">Returns new EventSeat item </response>
        /// <response code="400">Error EventSeat not saved.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // POST: api/EventSeats
        [HttpPost]
        public async Task<ActionResult<EventSeat>> PostEventSeat(EventSeat eventSeat)
        {
            _context.EventSeat.Add(eventSeat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEventSeat", new { id = eventSeat.EventSeatId }, eventSeat);
        }

        private bool EventSeatExists(int id)
        {
            return _context.EventSeat.Any(e => e.EventSeatId == id);
        }
    }
}
