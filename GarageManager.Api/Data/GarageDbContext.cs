using GarageManager.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GarageManager.Api.Data;

public class GarageDbContext : DbContext
{
    public GarageDbContext(DbContextOptions<GarageDbContext> options) : base(options)
    {

    }
    public DbSet<Car> Cars => Set<Car>();
    public DbSet<ServiceRecord> ServiceRecords => Set<ServiceRecord>();
}