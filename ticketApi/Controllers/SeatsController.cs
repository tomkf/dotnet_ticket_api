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

        /// <summary>
        /// Retuns all Seat items.
        /// </summary>
        // GET: api/Seats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seat>>> GetSeat()
        {
            return await _context.Seat.ToListAsync();
        }

        /// <summary>
        /// Get a single Seat item by id.
        /// </summary> 
        /// <param name="id"></param>
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


        /// <summary>
        /// Update a single Seat item by id.
        /// </summary>
        /// <remarks>
        /// Format:
        ///
        ///     PUT /Seat
        ///     {
        ///        "SeatId": 1,
        ///        "Price": 0.00
        ///        "RowId": 1
        ///     }
        /// </remarks> 
        /// <param name="id"></param>
        /// <param name="seat"></param>
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

        private bool SeatExists(int id)
        {
            return _context.Seat.Any(e => e.SeatId == id);
        }
    }
}
