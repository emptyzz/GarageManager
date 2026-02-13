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
}
