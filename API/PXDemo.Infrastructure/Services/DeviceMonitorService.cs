using Microsoft.EntityFrameworkCore;
using PXDemo.Infrastructure.Features.DateTimeResolver;
using PXDemo.Infrastructure.Persistance;

namespace PXDemo.Infrastructure.Services
{
    public class DeviceMonitorService(
        IDbContextFactory<DeviceDbContext> dbContextFactory,
        IDateTimeResolver dateTimeResolver
        ) : IDeviceMonitorService
    {
        readonly DeviceDbContext _deviceDbContext = dbContextFactory.CreateDbContext();

        public void ProcessOnlineStatus(TimeSpan lastCommunicationThreshold)
        {
            var devices = _deviceDbContext.Devices
                .Where(d => d.IsOnline);

            var now = dateTimeResolver.Now;
            foreach (var device in devices)
            {
                if (now - device.LastCommunication > lastCommunicationThreshold)
                    device.IsOnline = false;
            }

            _deviceDbContext.SaveChanges();
        }
    }
}
