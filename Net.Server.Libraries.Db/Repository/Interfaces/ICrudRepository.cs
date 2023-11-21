using Net.Server.Libraries.Db.Types;

namespace Net.Server.Libraries.Db.Repository.Interfaces;

public interface ICrudRepository<TEntity> : ICreateRepository<TEntity>, IReadRepository<TEntity>, IUpdateRepository<TEntity>, IDeleteRepository<TEntity>
    where TEntity : class, IBaseDataType
{
    IQueryable<TEntity> Query();
}
