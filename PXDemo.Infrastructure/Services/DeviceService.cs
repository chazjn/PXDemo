using Microsoft.EntityFrameworkCore;
using PXDemo.Infrastructure.Models;
using PXDemo.Infrastructure.Persistance;

namespace PXDemo.Infrastructure.Services
{
    public class DeviceService(IDbContextFactory<DataContext> dbContextFactory) : IDeviceService
    {
        readonly DataContext _dataContext = dbContextFactory.CreateDbContext();

        public Device? GetDeviceById(Guid id)
        {
            var device = _dataContext.Devices
                .FirstOrDefault(d => d.Id == id);

            return device;
        }

        public IEnumerable<Device> GetDevices()
        {
            return [.. _dataContext.Devices];
        }

        public void AddDevice(Device device) 
        { 
            _dataContext.Devices.Add(device);
            _dataContext.SaveChanges();
        }
    }
}
