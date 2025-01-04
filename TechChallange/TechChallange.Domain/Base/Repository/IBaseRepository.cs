using System.Linq.Expressions;
using TechChallange.Domain.Base.Entity;

namespace TechChallange.Domain.Base.Repository
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task AddAsync(T entity);
        Task RemoveByIdAsync(Guid id);

        Task UpdateAsync(T entity);

        Task<T> GetByIdAsync(Guid id);

        public Task<T> GetAsync(Expression<Func<T, bool>> search);

        public Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> search);
        Task<T> GetOneWithIncludeAsync(Expression<Func<T, bool>> search, params Expression<Func<T, object>>[] includes);

    }
}
