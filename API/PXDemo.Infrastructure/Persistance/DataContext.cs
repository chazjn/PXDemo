using Microsoft.EntityFrameworkCore;
using PXDemo.Infrastructure.Models;

namespace PXDemo.Infrastructure.Persistance
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceStatus> DeviceStatuses { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }
    }
}
