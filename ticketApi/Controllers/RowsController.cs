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
    public class RowsController : ControllerBase
    {
        private readonly dotnetapiContext _context;

        public RowsController(dotnetapiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Return all Rows.
        /// </summary>
        // GET: api/Rows
        // GET: api/Rows
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Row>>> GetRow()
        {
            return await _context.Row.ToListAsync();
        }

        /// <summary>
        /// Get a specific Row by id.
        /// </summary> 
        /// <param name="id"></param>
        // GET: api/Rows/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Row>> GetRow(int id)
        {
            var row = await _context.Row.FindAsync(id);

            if (row == null)
            {
                return NotFound();
            }

            return row;
        }

        /// <summary>
        /// Update a single Row by id.
        /// </summary> 
        /// <remarks>
        /// Format:
        ///
        ///     PUT /Rows
        ///     {
        ///        "RowId": 1,
        ///        "RowName": "Row1",
        ///        "SectionId": 1
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="row"></param>
        // PUT: api/Rows/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRow(int id, Row row)
        {
            if (id != row.RowId)
            {
                return BadRequest();
            }

            _context.Entry(row).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RowExists(id))
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

        private bool RowExists(int id)
        {
            return _context.Row.Any(e => e.RowId == id);
        }
    }
}
