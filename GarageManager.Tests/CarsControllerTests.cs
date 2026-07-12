using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

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
    }
}
