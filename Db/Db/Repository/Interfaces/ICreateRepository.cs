﻿using Db.Types;

namespace Db.Repository.Interfaces;

public interface ICreateRepository<TEntity> : IRawRepository
    where TEntity : class, IBaseDataType
{
    Task<TEntity> AddAsync(TEntity entity);
    Task<IEnumerable<TEntity>> AddAsync(ICollection<TEntity> entities);
}
