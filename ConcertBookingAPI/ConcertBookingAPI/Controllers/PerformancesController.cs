using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConcertBookingAPI.Data;
using ConcertBookingAPI.Models;

namespace ConcertBookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerformancesController : ControllerBase
    {
        private readonly BookingContext _context;

        public PerformancesController(BookingContext context)
        {
            _context = context;
        }

        // GET: api/Performances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Performance>>> GetPerformances()
        {
            return await _context.Performances.ToListAsync();  // Return all performances directly
        }

        // GET: api/Performances/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Performance>> GetPerformance(int id)
        {
            var performance = await _context.Performances.FindAsync(id);

            if (performance == null)
            {
                return NotFound();
            }

            return performance;  // Return the performance directly
        }

        // POST: api/Performances
        [HttpPost]
        public async Task<ActionResult<Performance>> PostPerformance(Performance performance)
        {
            _context.Performances.Add(performance);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPerformance), new { id = performance.Id }, performance);
        }

        // DELETE: api/Performances/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerformance(int id)
        {
            var performance = await _context.Performances.FindAsync(id);

            if (performance == null)
            {
                return NotFound();
            }

            _context.Performances.Remove(performance);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
