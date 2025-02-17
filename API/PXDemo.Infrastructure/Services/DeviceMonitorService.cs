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
                .Where(d => dateTimeResolver.Now - d.LastCommunication > lastCommunicationThreshold)
                .Where(d => d.IsOnline);

            foreach (var device in devices)
                    device.IsOnline = false;
            
            _deviceDbContext.SaveChanges();
        }
    }
}
