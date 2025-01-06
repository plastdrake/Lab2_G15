using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConcertBookingAPI.Data;
using ConcertBookingAPI.Models;

namespace ConcertBookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConcertsController : ControllerBase
    {
        private readonly BookingContext _context;

        public ConcertsController(BookingContext context)
        {
            _context = context;
        }

        // GET: api/Concerts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Concert>>> GetConcerts()
        {
            return await _context.Concerts.ToListAsync();  // Return all concerts directly
        }

        // GET: api/Concerts/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Concert>> GetConcert(int id)
        {
            var concert = await _context.Concerts.FindAsync(id);

            if (concert == null)
            {
                return NotFound();
            }

            return concert;  // Return the concert directly
        }

        // POST: api/Concerts
        [HttpPost]
        public async Task<ActionResult<Concert>> PostConcert(Concert concert)
        {
            _context.Concerts.Add(concert);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetConcert), new { id = concert.Id }, concert);
        }

        // DELETE: api/Concerts/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConcert(int id)
        {
            var concert = await _context.Concerts.FindAsync(id);

            if (concert == null)
            {
                return NotFound();
            }

            _context.Concerts.Remove(concert);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
