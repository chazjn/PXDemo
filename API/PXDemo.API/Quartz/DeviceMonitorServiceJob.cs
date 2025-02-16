using PXDemo.Infrastructure.Services;
using Quartz;

namespace PXDemo.API.Quartz
{
    [DisallowConcurrentExecution]
    public class DeviceMonitorServiceJob(IDeviceMonitorService deviceMonitorService) : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            //TODO: inject TimeSpan via config
            deviceMonitorService.ProcessOnlineStatus(TimeSpan.FromMinutes(2));
            return Task.CompletedTask;
        }
    }
}
