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
    }
}