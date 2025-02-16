namespace PXDemo.Infrastructure.Models
{
    public class Device
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public int DeviceTypeId { get; set; }
        
        public DeviceType DeviceType { get; set; }

        public bool IsOnline { get; set; }
        
        public DateTime? LastCommunication { get; set; }
        
        public double SignalStrength { get; set; }
    }
}
