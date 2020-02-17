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
    public class SeatsController : ControllerBase
    {
        private readonly dotnetapiContext _context;

        public SeatsController(dotnetapiContext context)
        {
            _context = context;
        }

        // GET: api/Seats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seat>>> GetSeat()
        {
            return await _context.Seat.ToListAsync();
        }

        // GET: api/Seats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Seat>> GetSeat(int id)
        {
            var seat = await _context.Seat.FindAsync(id);

            if (seat == null)
            {
                return NotFound();
            }

            return seat;
        }

        // PUT: api/Seats/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeat(int id, Seat seat)
        {
            if (id != seat.SeatId)
            {
                return BadRequest();
            }

            _context.Entry(seat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeatExists(id))
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

        // POST: api/Seats
        //[HttpPost]
        //public async Task<ActionResult<Seat>> PostSeat(Seat seat)
        //{
        //    _context.Seat.Add(seat);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetSeat", new { id = seat.SeatId }, seat);
        //}

        // DELETE: api/Seats/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Seat>> DeleteSeat(int id)
        //{
        //    var seat = await _context.Seat.FindAsync(id);
        //    if (seat == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Seat.Remove(seat);
        //    await _context.SaveChangesAsync();

        //    return seat;
        //}

        private bool SeatExists(int id)
        {
            return _context.Seat.Any(e => e.SeatId == id);
        }
    }
}
