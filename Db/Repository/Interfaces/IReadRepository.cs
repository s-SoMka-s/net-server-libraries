using Db.Types;
using System.Linq.Expressions;

namespace Db.Repository.Interfaces;

public interface IReadRepository<TEntity> : IRawRepository
    where TEntity : class, IBaseDataType
{
    Task<TEntity?> FindAsync(long id);
    Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate);
}