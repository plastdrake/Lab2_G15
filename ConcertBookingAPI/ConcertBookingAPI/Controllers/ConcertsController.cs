using Microsoft.AspNetCore.Mvc;
using ConcertBookingAPI.Repositories;
using ConcertBookingAPI.Models;

namespace ConcertBookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConcertsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConcertsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Concert>>> GetConcerts()
        {
            var concerts = await _unitOfWork.Concerts.GetAllAsync();
            return Ok(concerts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Concert>> GetConcert(int id)
        {
            var concert = await _unitOfWork.Concerts.GetByIdAsync(id);
            if (concert == null) return NotFound();
            return Ok(concert);
        }
    }
}
