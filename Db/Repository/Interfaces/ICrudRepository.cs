using Db.Types;

namespace Db.Repository.Interfaces;

public interface ICrudRepository<TEntity> : ICreateRepository<TEntity>, IReadRepository<TEntity>, IUpdateRepository<TEntity>, IDeleteRepository<TEntity>
    where TEntity : class, IBaseDataType
{
    IQueryable<TEntity> Query();
}
