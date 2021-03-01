using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ToDoListWebApi.Repository
{
    public abstract class Repository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class
        where TContext : DbContext
    {
        private readonly TContext _context;

        public Repository(TContext context)
        {
            _context = context;
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Delete(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity is null) return null;

            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Get(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> Update(int id, TEntity entity)
        {
            var match = await _context.Set<TEntity>().FindAsync(id);
            if (entity is null) return null;

            match = entity;
            await _context.SaveChangesAsync();
            return match;
        }
    }
}
