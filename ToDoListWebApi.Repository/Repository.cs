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
        protected TContext Context { get; }

        public Repository(TContext context)
        {
            Context = context;
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Delete(int id)
        {
            var entity = await Context.Set<TEntity>().FindAsync(id);
            if (entity is null) return null;

            Context.Set<TEntity>().Remove(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Get(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> Update(int id, TEntity entity)
        {
            var match = await Context.Set<TEntity>().FindAsync(id);
            if (entity is null) return null;

            match = entity;
            await Context.SaveChangesAsync();
            return match;
        }
    }
}
