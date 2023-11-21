using Microsoft.EntityFrameworkCore;
using Net.Server.Libraries.Db.Types;

namespace Net.Server.Libraries.Db.Repository.Interfaces;

public interface ICrudFactory
{
    DbContext Context { get; }

    ICrudRepository<TEntity> Get<TEntity>() where TEntity : class, IBaseDataType;
}
