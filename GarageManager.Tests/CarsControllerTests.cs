using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using GarageManager.Api.Models;

namespace GarageManager.Tests
{
    public class CarsControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;

        public CarsControllerTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetCars_ReturnsSuccessStatusCode()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/cars");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CreateCar_ReturnsCreatedCar()
        {
            var client = _factory.CreateClient();
            var newCar = new { name = "Test Car", mileageKm = 12345 };
            var response = await client.PostAsJsonAsync("/api/cars", newCar);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var created = await response.Content.ReadFromJsonAsync<Car>();
            Assert.Equal("Test Car", created?.Name);
        }
    }
}
