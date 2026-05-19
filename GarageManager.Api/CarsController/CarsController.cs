using Microsoft.AspNetCore.Mvc;
using GarageManager.Api.Models;

namespace GarageManager.Api.Controllers;

[ApiController]
[Route("api/cars")]

public class CarsController : ControllerBase
{
    private static readonly List<Car> _cars = new()
    {
        new Car { Id = 1, Name = "Toyota Crown", MileageKm = 999 },
        new Car { Id = 2, Name = "Toyota Mark II GX81", MileageKm = 320000 },
        new Car { Id = 3, Name = "BMW E39 530i", MileageKm = 250000 }
    };

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_cars);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var car = _cars.FirstOrDefault( c => c.Id == id);
        if (car == null) return NotFound();
        else { return Ok(car); }
    }
}