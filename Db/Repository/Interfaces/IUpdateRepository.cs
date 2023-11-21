using Db.Types;

namespace Db.Repository.Interfaces;

public interface IUpdateRepository<TEntity> : IRawRepository
    where TEntity : class, IBaseDataType
{
    Task<TEntity?> UpdateAsync(TEntity updated);

    Task<TEntity?> UpdateAsync(long id, Action<TEntity> patch);
}