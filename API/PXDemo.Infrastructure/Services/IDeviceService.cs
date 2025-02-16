using PXDemo.Infrastructure.Dtos;
using PXDemo.Infrastructure.Models;

namespace PXDemo.Infrastructure.Services
{
    public interface IDeviceService
    {
        Device? GetDeviceById(Guid id);
        IEnumerable<Device> GetDevices();
        void AddDevice(DeviceInputDto device);
        void UpdateDevice(Guid deviceId, DeviceUpdateDto deviceUpdate);
        IEnumerable<DeviceType> GetDeviceTypes();
    }
}