using PXDemo.Infrastructure.Features.DateTimeResolver;
using PXDemo.Infrastructure.Models;

namespace PXDemo.Infrastructure.Features.Ordering
{
    public class DeviceOrderByStatusLastCommunicationAndSignalStrength(
        IDateTimeResolver dateTimeResolver,
        TimeSpan penaltyThreshold) 
        : IOrderStrategy<Device>
    {
        public IOrderedEnumerable<Device> Order(IEnumerable<Device> items)
        {
            var now = dateTimeResolver.Now;

            return items
                .OrderByDescending(d => d.IsOnline == false ? 0 : 1)
                .ThenByDescending(d => now - d.LastCommunication > penaltyThreshold ? 0 : 1)
                .ThenByDescending(d => d.SignalStrength)
                .ThenBy(d => d.LastCommunication);
        }
    }
}
