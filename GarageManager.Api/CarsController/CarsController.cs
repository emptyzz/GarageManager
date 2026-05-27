using GarageManager.Api.Data;
using GarageManager.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GarageManager.Api.CarsController;

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
}