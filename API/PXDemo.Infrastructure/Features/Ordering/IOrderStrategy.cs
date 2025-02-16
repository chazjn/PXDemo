namespace PXDemo.Infrastructure.Features.Ordering
{
    public interface IOrderStrategy<T>
    {
        IOrderedEnumerable<T> Order(IEnumerable<T> items);
    }
}
