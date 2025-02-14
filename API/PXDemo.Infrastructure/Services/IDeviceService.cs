using PXDemo.Infrastructure.Models;

namespace PXDemo.Infrastructure.Services
{
    public interface IDeviceService
    {
        Device? GetDeviceById(Guid id);
        IEnumerable<Device> GetDevices();
        void AddDevice(Device device);
    }
}