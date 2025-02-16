using PXDemo.Infrastructure.Models;

namespace PXDemo.Infrastructure.Features.Ordering
{
    public class DeviceOrderByName : IOrderStrategy<Device>
    {
        public IOrderedEnumerable<Device> Order(IEnumerable<Device> items)
        {
            return items.OrderBy(i => i.Name);
        }
    }
}
