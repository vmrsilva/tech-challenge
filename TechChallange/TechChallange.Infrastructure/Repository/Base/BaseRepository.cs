using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TechChallange.Domain.Base.Entity;
using TechChallange.Domain.Base.Repository;
using TechChallange.Infrastructure.Context;

namespace TechChallange.Infrastructure.Repository.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly TechChallangeContext _context;
        public BaseRepository(TechChallangeContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            var dbSet = _context.Set<T>();
            dbSet.Add(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> search)
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .Where(search)
                .FirstOrDefaultAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var dbSet = _context.Set<T>();
            var entity = await dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            return entity;
        }

        public async Task RemoveByIdAsync(Guid id)
        {
            var dbSet = _context.Set<T>();
            var entity = await GetByIdAsync(id).ConfigureAwait(false);

            if (entity == null)
                return;

            dbSet.Remove(entity);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateAsync(T entity)
        {
            var dbSet = _context.Set<T>();
            dbSet.Update(entity);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
