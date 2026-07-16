using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using GarageManager.Api.Data;
using Microsoft.Extensions.DependencyInjection;

namespace GarageManager.Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<GarageDbContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<GarageDbContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDb");
                });
            });
        }
    }
}
