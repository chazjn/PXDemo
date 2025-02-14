namespace PXDemo.Infrastructure.Models
{
    public class Device
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DeviceType DeviceType { get; set; }
        public int DeviceTypeId { get; set; }
        public DateTime LastCommunication { get; set; }
        public double SignalStrength { get; set; }
    }
}
