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
    public class PerformancesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PerformancesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Performances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Performance>>> GetPerformances()
        {
            var performances = await _unitOfWork.Performances.GetAllAsync();
            return Ok(performances);
        }

        // GET: api/Performances/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Performance>> GetPerformance(int id)
        {
            var performance = await _unitOfWork.Performances.GetByIdAsync(id);
            if (performance == null)
            {
                return NotFound();
            }
            return Ok(performance);
        }

        // GET: api/Performances/ByConcert/1
        [HttpGet("ByConcert/{concertId}")]
        public async Task<ActionResult<IEnumerable<Performance>>> GetPerformancesByConcert(int concertId)
        {
            var performances = await _unitOfWork.Performances.GetPerformancesByConcertIdAsync(concertId);
            return Ok(performances);
        }

        // POST: api/Performances
        [HttpPost]
        public async Task<ActionResult<Performance>> PostPerformance(Performance performance)
        {
            await _unitOfWork.Performances.AddAsync(performance);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPerformance), new { id = performance.Id }, performance);
        }

        // DELETE: api/Performances/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerformance(int id)
        {
            var performance = await _unitOfWork.Performances.GetByIdAsync(id);
            if (performance == null)
            {
                return NotFound();
            }

            _unitOfWork.Performances.Delete(performance);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/Performances/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerformance(int id, Performance performance)
        {
            if (id != performance.Id)
            {
                return BadRequest();
            }

            _unitOfWork.Performances.Update(performance);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _unitOfWork.Performances.GetByIdAsync(id) == null)
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
