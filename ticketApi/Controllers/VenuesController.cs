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
    public class VenuesController : ControllerBase
    {
        private readonly dotnetapiContext _context;

        public VenuesController(dotnetapiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Return all Venue item.
        /// </summary>
        // GET: api/Venues
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Venue>>> GetVenue()
        {
            return await _context.Venue.ToListAsync();
        }

        /// <summary>
        /// Get a single Venue item by id.
        /// </summary> 
        // GET: api/Venues/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Venue>> GetVenue(string id)
        {
            var venue = await _context.Venue.FindAsync(id);

            if (venue == null)
            {
                return NotFound();
            }

            return venue;
        }

        /// <summary>
        /// Update a single Venue item by id.
        /// </summary> 
        /// <remarks>
        /// Format:
        ///
        ///     PUT /Venue
        ///     {
        ///        "VenueName": "Venue1",
        ///        "Capacity": 200
        ///     }
        /// </remarks>
        /// <param name="venue"></param>
        // PUT: api/Venues/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVenue(string id, Venue venue)
        {
            if (id != venue.VenueName)
            {
                return BadRequest();
            }

            _context.Entry(venue).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VenueExists(id))
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
        /// Creates a Venue item.
        /// </summary>
        /// <remarks>
        /// Format:
        ///
        ///     POST /Venue
        ///     {
        ///        "VenueName": "Venue1",
        ///        "Capacity": 200
        ///     }
        /// </remarks> 
        /// <param name="venue"></param>
        /// <returns>Creates a new Venue item</returns>
        /// <response code="201">Returns the newly created venue item</response>
        /// <response code="400">Error, item not saved</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // POST: api/Venues
        [HttpPost]
        public async Task<ActionResult<Venue>> PostVenue(Venue venue)
        {
            _context.Venue.Add(venue);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VenueExists(venue.VenueName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetVenue", new { id = venue.VenueName }, venue);
        }


        /// <summary>
        /// Deletes a single Venue item by id.
        /// </summary> 
        // DELETE: api/Venues/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Venue>> DeleteVenue(string id)
        {
            var venue = await _context.Venue.FindAsync(id);
            if (venue == null)
            {
                return NotFound();
            }

            _context.Venue.Remove(venue);
            await _context.SaveChangesAsync();

            return venue;
        }

        private bool VenueExists(string id)
        {
            return _context.Venue.Any(e => e.VenueName == id);
        }
    }
}
