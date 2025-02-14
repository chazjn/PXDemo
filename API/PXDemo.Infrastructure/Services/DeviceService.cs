using Microsoft.EntityFrameworkCore;
using PXDemo.Infrastructure.Models;
using PXDemo.Infrastructure.Persistance;

namespace PXDemo.Infrastructure.Services
{
    public class DeviceService(IDbContextFactory<DeviceDbContext> dbContextFactory) : IDeviceService
    {
        readonly DeviceDbContext _deviceDbContext = dbContextFactory.CreateDbContext();

        public Device? GetDeviceById(Guid id)
        {
            var device = _deviceDbContext.Devices
                .FirstOrDefault(d => d.Id == id);

            return device;
        }

        public IEnumerable<Device> GetDevices()
        {
            return [.. _deviceDbContext.Devices];
        }

        public void AddDevice(Device device) 
        { 
            _deviceDbContext.Devices.Add(device);
            _deviceDbContext.SaveChanges();
        }
    }
}
