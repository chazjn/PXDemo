using Microsoft.EntityFrameworkCore;
using PXDemo.Infrastructure.Models;

namespace PXDemo.Infrastructure.Persistance
{
    public class DeviceDbContext(DbContextOptions<DeviceDbContext> options) : DbContext(options)
    {
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }
    }
}
