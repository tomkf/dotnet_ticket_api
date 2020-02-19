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
    public class EventsController : ControllerBase
    {
        private readonly dotnetapiContext _context;

        public EventsController(dotnetapiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Return all Events.
        /// </summary>
        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvent()
        {
            return await _context.Event.ToListAsync();
        }


        /// <summary>
        /// Return single event by id.
        /// </summary> 
        /// <param name="id"></param>
        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            var @event = await _context.Event.FindAsync(id);

            if (@event == null)
            {
                return NotFound();
            }

            return @event;
        }

        /// <summary>
        /// Update a single event.
        /// </summary>
        /// <remarks>
        ///  Format:
        ///
        ///     PUT /Event
        ///     {
        ///        "EventId": 1,
        ///        "EventName": "Event1",
        ///        "VenueName": "Venue1"
        ///     }
        /// </remarks> 
        /// <param name="id"></param>
        /// <param name="event"></param>
        // PUT: api/Events/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(int id, Event @event)
        {
            if (id != @event.EventId)
            {
                return BadRequest();
            }

            _context.Entry(@event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
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
        /// Create new Event.
        /// </summary>
        /// <remarks>
        ///  Format:
        ///
        ///     POST /Seat
        ///     {
        ///        "EventId": 1,
        ///        "EventName": "Event1",
        ///        "VenueName": "Venue1"
        ///     }
        /// </remarks> 
        /// <param name="event"></param>
        /// <returns>New Event item.</returns>
        /// <response code="201">Returns the newly created Event item.</response>
        /// <response code="400">Error, event is not saved</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // POST: api/Events
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event @event)
        {
            _context.Event.Add(@event);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvent", new { id = @event.EventId }, @event);
        }


        /// <summary>
        /// Deletes Event item by id.
        /// </summary> 
        /// <param name="id"></param>
        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Event>> DeleteEvent(int id)
        {
            var @event = await _context.Event.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            _context.Event.Remove(@event);
            await _context.SaveChangesAsync();

            return @event;
        }

        private bool EventExists(int id)
        {
            return _context.Event.Any(e => e.EventId == id);
        }
    }
}
