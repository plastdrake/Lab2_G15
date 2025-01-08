using Microsoft.AspNetCore.Mvc;
using ConcertBookingAPI.Models;
using ConcertBookingAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ConcertBookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            var bookings = await _unitOfWork.Bookings.GetAllAsync();
            return Ok(bookings);
        }

        // GET: api/Bookings/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        // GET: api/Bookings/ByPerformance/1
        [HttpGet("ByPerformance/{performanceId}")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookingsByPerformance(int performanceId)
        {
            var bookings = await _unitOfWork.Bookings.GetBookingsByPerformanceIdAsync(performanceId);
            return Ok(bookings);
        }

        // POST: api/Bookings
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        {
            await _unitOfWork.Bookings.AddAsync(booking);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBooking), new { id = booking.Id }, booking);
        }

        // DELETE: api/Bookings/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _unitOfWork.Bookings.Delete(booking);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/Bookings/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, Booking booking)
        {
            if (id != booking.Id)
            {
                return BadRequest();
            }

            _unitOfWork.Bookings.Update(booking);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _unitOfWork.Bookings.GetByIdAsync(id) == null)
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
    }
}
