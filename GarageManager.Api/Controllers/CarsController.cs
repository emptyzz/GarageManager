using GarageManager.Api.Data;
using GarageManager.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GarageManager.Api.DTOs;

namespace GarageManager.Api.Controllers 
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly GarageDbContext _context;

        public CarsController(GarageDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            return await _context.Cars.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
                return NotFound();

            return car;
        }
        [HttpPost]
        public async Task<ActionResult<Car>> CreateCar([FromBody] CreateCarDto dto)
        {
            var car = new Car
            {
                Name = dto.Name,
                MileageKm = dto.MileageKm
            };

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCar), new { id = car.Id }, car);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar(int id, [FromBody] UpdateCarDto dto)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
                return NotFound();

            car.Name = dto.Name;
            car.MileageKm = dto.MileageKm;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
                return NotFound();

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}