using Moq;
using PXDemo.Infrastructure.Features.DateTimeResolver;
using PXDemo.Infrastructure.Features.Ordering;
using PXDemo.Infrastructure.Models;

namespace PXDemo.UnitTests.Infrastructure.Features.Ordering
{
    [TestClass]
    public class DeviceOrderBySignalStrengthAndLastCommunicationTests
    {
        [TestMethod]
        public void A()
        {
            var dateTimeResolver = new Mock<IDateTimeResolver>();
            dateTimeResolver.Setup(s => s.Now).Returns(DateTime.Parse("2010-01-01 10:00"));

            var orderer = new DeviceOrderBySignalStrengthAndLastCommunication(dateTimeResolver.Object, TimeSpan.FromMinutes(2));

            var items = new List<Device>
            {
                new () { Name = "Device 3", LastCommunication = DateTime.Parse("2010-01-01 09:57:00"), SignalStrength = 3 },
                new () { Name = "Device 2", LastCommunication = DateTime.Parse("2010-01-01 10:00:00"), SignalStrength = 2 },
                new () { Name = "Device 1", LastCommunication = DateTime.Parse("2010-01-01 10:00:00"), SignalStrength = 1 }
            };

            var orderedItems = orderer.Order(items);

            Assert.IsTrue(items.First() == orderedItems.Last());
        }
    }
}
