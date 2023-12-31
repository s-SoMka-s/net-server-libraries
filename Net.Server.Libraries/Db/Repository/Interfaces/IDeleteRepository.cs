﻿using Net.Server.Libraries.Db.Types;
using System.Linq.Expressions;

namespace Net.Server.Libraries.Db.Repository.Interfaces;

public interface IDeleteRepository<TEntity>
    where TEntity : class, IBaseDataType
{
    Task<bool> DeleteAsync(TEntity entity);
    Task<bool> DeleteAsync(long id);
    Task<bool> DeleteAsync(IEnumerable<long> ids);
    Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> predicate);
}
