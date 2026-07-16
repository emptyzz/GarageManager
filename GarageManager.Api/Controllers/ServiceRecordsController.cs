using GarageManager.Api.Data;
using GarageManager.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GarageManager.Api.DTOs;

namespace GarageManager.Api.Controllers
{
    [ApiController]
    [Route("api/cars/{carId}/records")]
    public class ServiceRecordsController : ControllerBase
    {
        private readonly GarageDbContext _context;

        public ServiceRecordsController(GarageDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceRecord>>> GetRecords(int carId)
        {
            return await _context.ServiceRecords.Where(r => r.CarId == carId).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<ServiceRecord>> CreateRecord(int carId, [FromBody] CreateServiceRecordDto dto)
        {
            var carExists = await _context.Cars.AnyAsync(c => c.Id == carId);
            if (!carExists) return NotFound();

            var record = new ServiceRecord
            {
                CarId = carId,
                Date = DateTime.SpecifyKind(dto.Date, DateTimeKind.Utc),
                Description = dto.Description,
                Cost = dto.Cost,
                MileageKm = dto.MileageKm
            };

            _context.ServiceRecords.Add(record);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRecords), new { carId = carId }, record);
        }

        [HttpGet("/api/cars/{carId}/total-cost")]
        public async Task<ActionResult<decimal>> TotalCost(int carId)
        {
            var carExists = await _context.Cars.AnyAsync(c => c.Id == carId);
            if (!carExists) return NotFound();

            return await _context.ServiceRecords.Where(r => r.CarId == carId).SumAsync(r => r.Cost);
        }
    }
}