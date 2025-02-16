namespace PXDemo.Infrastructure.Features.DateTimeResolver
{
    public class UtcDateTimeResolver : IDateTimeResolver
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
