namespace PXDemo.Infrastructure.Services
{
    public interface IDeviceMonitorService
    {
        void ProcessOnlineStatus(TimeSpan lastCommunicationThreshold);
    }
}
