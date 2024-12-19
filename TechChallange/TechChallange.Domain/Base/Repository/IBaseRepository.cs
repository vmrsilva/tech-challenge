using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
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

    }
}
