namespace PXDemo.Infrastructure.Services
{
    public class UtcDateTimeResolver : IDateTimeResolver
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
