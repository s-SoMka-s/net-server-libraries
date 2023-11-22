using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Net.Server.Libraries.Db.Types;
using Net.Server.Libraries.Db.Repository.Interfaces;

namespace Net.Server.Libraries.Db.Repository.Implementations;

public class CrudRepository<TEntity> : ICrudRepository<TEntity> where TEntity : class, IBaseDataType
{
    protected DbContext Context { get; }
    protected DbSet<TEntity> Set { get; }

    private readonly ICrudRepository<TEntity> self;

    public CrudRepository(DbContext context)
    {
        Context = context;
        Set = context.Set<TEntity>();

        self = this;
    }


    #region IRawRepository

    DbContext IRawRepository.Context => Context;

    #endregion IRawRepository

    #region ICreateRepository

    async Task<TEntity> ICreateRepository<TEntity>.AddAsync(TEntity entity)
    {
        var result = await self.AddAsync(new[] { entity });

        return result.First();
    }

    async Task<IEnumerable<TEntity>> ICreateRepository<TEntity>.AddAsync(ICollection<TEntity> entities)
    {
        await Context.AddRangeAsync(entities);
        await Context.SaveChangesAsync();

        return entities;
    }

    #endregion

    #region IReadRepository 

    async Task<TEntity?> IReadRepository<TEntity>.FindAsync(long id)
    {
        return await Set.SingleOrDefaultAsync(e => e.Id == id);
    }

    async Task<TEntity?> IReadRepository<TEntity>.FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await Set.SingleOrDefaultAsync(predicate);
    }

    #endregion

    IQueryable<TEntity> ICrudRepository<TEntity>.Query()
    {
        return Set.AsNoTracking();
    }

    #region IUpdateRepository

    async Task<TEntity?> IUpdateRepository<TEntity>.UpdateAsync(TEntity updated)
    {
        var entity = Set.Update(updated).Entity;
        await Context.SaveChangesAsync();

        return entity;
    }

    async Task<TEntity?> IUpdateRepository<TEntity>.UpdateAsync(long id, Action<TEntity> patch)
    {
        var entity = await self.FindAsync(id);
        if (entity == default)
        {
            return default;
        }

        patch(entity);
        Context.Entry(entity).State = EntityState.Modified;

        await Context.SaveChangesAsync();

        return entity;
    }

    #endregion

    #region IDeleteRepository 

    async Task<bool> IDeleteRepository<TEntity>.DeleteAsync(TEntity entity)
    {
        return await self.DeleteAsync(entity.Id);
    }

    async Task<bool> IDeleteRepository<TEntity>.DeleteAsync(long id)
    {
        return await self.DeleteAsync(new[] { id });
    }

    async Task<bool> IDeleteRepository<TEntity>.DeleteAsync(IEnumerable<long> ids)
    {
        var forRemove = ids.ToArray();

        return await self.DeleteAsync(e => forRemove.Contains(e.Id));
    }

    async Task<bool> IDeleteRepository<TEntity>.DeleteAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var forRemove = await Set.Where(predicate).ToArrayAsync();

        Set.RemoveRange(forRemove);
        await Context.SaveChangesAsync();

        return !await Set.AnyAsync(predicate);
    }

    #endregion
}
