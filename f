[33mcommit b21ec45f7b49e5c05159053c707e7f6482d640a6[m[33m ([m[1;36mHEAD[m[33m -> [m[1;32mfeature/web-api-migration[m[33m, [m[1;31morigin/feature/web-api-migration[m[33m)[m
Author: emptyzz <radaew2019@Yandex.ru>
Date:   Wed May 27 06:21:00 2026 +0500

    feat(api): replace hardcoded data with EF Core queries in CarsController

[1mdiff --git a/GarageManager.Api/CarsController/CarsController.cs b/GarageManager.Api/CarsController/CarsController.cs[m
[1mindex 2b13937..78effa9 100644[m
[1m--- a/GarageManager.Api/CarsController/CarsController.cs[m
[1m+++ b/GarageManager.Api/CarsController/CarsController.cs[m
[36m@@ -1,31 +1,35 @@[m
[31m-using Microsoft.AspNetCore.Mvc;[m
[32m+[m[32musing GarageManager.Api.Data;[m
 using GarageManager.Api.Models;[m
[32m+[m[32musing Microsoft.AspNetCore.Mvc;[m
[32m+[m[32musing Microsoft.EntityFrameworkCore;[m
 [m
[31m-namespace GarageManager.Api.Controllers;[m
[32m+[m[32mnamespace GarageManager.Api.CarsController;[m
 [m
 [ApiController][m
[31m-[Route("api/cars")][m
[31m-[m
[32m+[m[32m[Route("api/[controller]")][m
 public class CarsController : ControllerBase[m
 {[m
[31m-    private static readonly List<Car> _cars = new()[m
[32m+[m[32m    private readonly GarageDbContext _context;[m
[32m+[m
[32m+[m[32m    public CarsController(GarageDbContext context)[m
     {[m
[31m-        new Car { Id = 1, Name = "Toyota Crown", MileageKm = 999 },[m
[31m-        new Car { Id = 2, Name = "Toyota Mark II GX81", MileageKm = 320000 },[m
[31m-        new Car { Id = 3, Name = "BMW E39 530i", MileageKm = 250000 }[m
[31m-    };[m
[32m+[m[32m        _context = context;[m
[32m+[m[32m    }[m
 [m
     [HttpGet][m
[31m-    public IActionResult Get()[m
[32m+[m[32m    public async Task<ActionResult<IEnumerable<Car>>> GetCars()[m
     {[m
[31m-        return Ok(_cars);[m
[32m+[m[32m        return await _context.Cars.ToListAsync();[m
     }[m
 [m
     [HttpGet("{id}")][m
[31m-    public IActionResult GetById(int id)[m
[32m+[m[32m    public async Task<ActionResult<Car>> GetCar(int id)[m
     {[m
[31m-        var car = _cars.FirstOrDefault( c => c.Id == id);[m
[31m-        if (car == null) return NotFound();[m
[31m-        else { return Ok(car); }[m
[32m+[m[32m        var car = await _context.Cars.FindAsync(id);[m
[32m+[m
[32m+[m[32m        if (car == null)[m
[32m+[m[32m            return NotFound();[m
[32m+[m
[32m+[m[32m        return car;[m
     }[m
 }[m
\ No newline at end of file[m
