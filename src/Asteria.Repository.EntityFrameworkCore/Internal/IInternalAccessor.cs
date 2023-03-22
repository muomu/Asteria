namespace Asteria.Repository.EntityFrameworkCore
{
    interface IInternalAccessor<out T>
    {
        T Value { get; }
    }
}
