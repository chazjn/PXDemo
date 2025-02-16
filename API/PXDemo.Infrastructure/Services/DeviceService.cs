using Microsoft.EntityFrameworkCore;
using PXDemo.Infrastructure.Dtos;
using PXDemo.Infrastructure.Models;
using PXDemo.Infrastructure.Persistance;

namespace PXDemo.Infrastructure.Services
{
    public class DeviceService(
        IDbContextFactory<DeviceDbContext> dbContextFactory,
        IDateTimeResolver dateTimeResolver
        ) : IDeviceService
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
            var devices = _deviceDbContext.Devices
                .Include(d => d.DeviceType);
            
            return [.. devices];
        }

        public void AddDevice(DeviceInputDto deviceInput) 
        {
            var device = new Device
            {
                Id = Guid.NewGuid(),
                Name = deviceInput.Name,
                DeviceTypeId = deviceInput.DeviceTypeId,
                IsOnline = false,
                LastCommunication = null,
                SignalStrength = null
            };
            
            _deviceDbContext.Devices.Add(device);
            _deviceDbContext.SaveChanges();
        }

        public void UpdateSignalStrength(Guid deviceId, double signalStrength)
        {
            var device = _deviceDbContext.Devices.FirstOrDefault(d => d.Id == deviceId);
            if (device == null)
                return;

            device.SignalStrength = signalStrength;
            device.IsOnline = true;
            device.LastCommunication = dateTimeResolver.Now;
            
            _deviceDbContext.SaveChanges();
        }

        public IEnumerable<DeviceType> GetDeviceTypes()
            => [.. _deviceDbContext.DeviceTypes];
    }
}
