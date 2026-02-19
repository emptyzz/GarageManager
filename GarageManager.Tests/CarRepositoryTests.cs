using GarageManager.Services;
using Xunit;

public class CarRepositoryTests
{
    [Fact]
    public void AddCar_ThenGetCars_ReturnsAddedCar()
    {
        var dbPath = $"test_{Guid.NewGuid():N}.db";
        var cs = $"Data Source={dbPath}";

        DatabaseInitializer.Initialize(cs);
        var repo = new CarRepository(cs);

        repo.AddCar("Test Car", 123);

        var cars = repo.GetCars();

        Assert.Contains(cars, c => c.Name == "Test Car" && c.MileageKm == 123);
    }
    [Fact]
    public void DeleteCarById_RemovesCar()
    {
        var dbPath = $"test_{Guid.NewGuid():N}.db";
        var cs = $"Data Source={dbPath}";

        DatabaseInitializer.Initialize(cs);
        var repo = new CarRepository(cs);

        repo.AddCar("Test Car", 123);

        var cars = repo.GetCars();

        var deleted = repo.DeleteCarById(cars[0].Id);
        Assert.True(deleted);

        var carsAfter = repo.GetCars();

        Assert.Empty(carsAfter);
    }
}