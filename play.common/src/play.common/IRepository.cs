using System.Linq.Expressions;

namespace play.common
{
    public interface IRepository<T> where T : IEntity
    {
        Task CreateAsync(T entity);
        Task<IReadOnlyCollection<T>> GetAllItemsAsync();
        Task<IReadOnlyCollection<T>> GetAllItemsAsync(Expression<Func<T, bool>> filter);
        Task<T> GetItemsAsync(Guid id);
        Task<T> GetItemsAsync(Expression<Func<T, bool>> filter);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(T entity);
    }
}