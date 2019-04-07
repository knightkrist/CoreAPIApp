using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoreAPIApp.Data;
using CoreAPIApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace CoreAPIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SegmentsController : ControllerBase
    {
        private readonly TripContext _context;

        public SegmentsController(TripContext context)
        {
            _context = context;
        }

        // GET: api/Segments
        //[HttpGet]
        //public IEnumerable<Segment> GetSegments()
        //{
        //    return _context.Segments;
        //}

        [HttpGet]
        public IEnumerable<OutputResult> GetSegments()
        {

            var items = _context.Segments.AsEnumerable().Select(c => {

                string name = _context.Trips.Where(i => i.Id == c.TripId).Select(i => i.Name).Single();

                return new OutputResult()
                {
                    Name = c.Name,
                    TripName = name,
                    Description = c.Description
                };
            }).ToArray();

            return items;
        }

        // GET: api/Segments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSegment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int tripId = _context.Segments.Where(i => i.Id == id).Select(i => i.TripId).First();

            string name = _context.Trips.Where(i => i.Id == tripId).Select(i => i.Name).Single();

            var segment = await _context.Segments.FindAsync(id);
            var item = new OutputResult()
            {
                Name = segment.Name,
                TripName = name,
                Description = segment.Description
            };
            

            if (segment == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // PUT: api/Segments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSegment([FromRoute] int id, [FromBody] Segment segment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != segment.Id)
            {
                return BadRequest();
            }

            _context.Entry(segment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SegmentExists(id))
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

        // POST: api/Segments
        [HttpPost]
        public async Task<IActionResult> PostSegment([FromBody] Segment segment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Segments.Add(segment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSegment", new { id = segment.Id }, segment);
        }

        // DELETE: api/Segments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSegment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var segment = await _context.Segments.FindAsync(id);
            if (segment == null)
            {
                return NotFound();
            }

            _context.Segments.Remove(segment);
            await _context.SaveChangesAsync();

            return Ok(segment);
        }

        private bool SegmentExists(int id)
        {
            return _context.Segments.Any(e => e.Id == id);
        }
    }
}