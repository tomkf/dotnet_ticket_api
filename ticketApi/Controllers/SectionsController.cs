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
    public class SectionsController : ControllerBase
    {
        private readonly dotnetapiContext _context;

        public SectionsController(dotnetapiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retunr all Section items.
        /// </summary>
        // GET: api/Sections
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Section>>> GetSection()
        {
            return await _context.Section.ToListAsync();
        }



        /// <summary>
        /// Get a single Section item by id.
        /// </summary> 
        /// <param name="id"></param>
        // GET: api/Sections/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Section>> GetSection(int id)
        {
            var section = await _context.Section.FindAsync(id);

            if (section == null)
            {
                return NotFound();
            }

            return section;
        }


        /// <summary>
        /// Update a single Section item by id.
        /// </summary> 
        /// <remarks>
        /// Format:
        ///
        ///     PUT /Sections
        ///     {
        ///        "SectionId": 1,
        ///        "SectionName": "Section1",
        ///        "VenueName": "Venue1"
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="section"></param>
        // PUT: api/Sections/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSection(int id, Section section)
        {
            if (id != section.SectionId)
            {
                return BadRequest();
            }

            _context.Entry(section).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SectionExists(id))
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

        private bool SectionExists(int id)
        {
            return _context.Section.Any(e => e.SectionId == id);
        }
    }
}
