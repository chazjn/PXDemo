using Microsoft.EntityFrameworkCore;
using PXDemo.Infrastructure.Dtos;
using PXDemo.Infrastructure.Features.DateTimeResolver;
using PXDemo.Infrastructure.Features.Ordering;
using PXDemo.Infrastructure.Models;
using PXDemo.Infrastructure.Persistance;

namespace PXDemo.Infrastructure.Services
{
    public class DeviceService(
        IDbContextFactory<DeviceDbContext> dbContextFactory,
        IDateTimeResolver dateTimeResolver,
        IOrderStrategy<Device> deviceOrderStrategy
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

            var orderedDevices = deviceOrderStrategy.Order(devices);

            return [.. orderedDevices];
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
                SignalStrength = 0
            };
            
            _deviceDbContext.Devices.Add(device);
            _deviceDbContext.SaveChanges();
        }

        public void UpdateDevice(Guid deviceId, DeviceUpdateDto deviceUpdate)
        {
            var device = _deviceDbContext.Devices.FirstOrDefault(d => d.Id == deviceId);
            if (device == null)
                return;

            device.LastCommunication = dateTimeResolver.Now;
            device.SignalStrength = deviceUpdate.SignalStrength;
            device.IsOnline = true;

            _deviceDbContext.SaveChanges();
        }

        public IEnumerable<DeviceType> GetDeviceTypes()
            => [.. _deviceDbContext.DeviceTypes.OrderBy(d => d.Name)];
    }
}
