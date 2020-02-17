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

        // GET: api/EventSeats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventSeat>>> GetEventSeat()
        {
            return await _context.EventSeat.ToListAsync();
        }

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

        // POST: api/EventSeats
        [HttpPost]
        public async Task<ActionResult<EventSeat>> PostEventSeat(EventSeat eventSeat)
        {
            _context.EventSeat.Add(eventSeat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEventSeat", new { id = eventSeat.EventSeatId }, eventSeat);
        }

        // DELETE: api/EventSeats/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<EventSeat>> DeleteEventSeat(int id)
        //{
        //    var eventSeat = await _context.EventSeat.FindAsync(id);
        //    if (eventSeat == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.EventSeat.Remove(eventSeat);
        //    await _context.SaveChangesAsync();

        //    return eventSeat;
        //}

        private bool EventSeatExists(int id)
        {
            return _context.EventSeat.Any(e => e.EventSeatId == id);
        }
    }
}
