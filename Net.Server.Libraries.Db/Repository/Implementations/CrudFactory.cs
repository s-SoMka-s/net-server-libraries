using Microsoft.EntityFrameworkCore;
using Net.Server.Libraries.Db.Repository.Interfaces;
using Net.Server.Libraries.Db.Types;

namespace Net.Server.Libraries.Db.Repository.Implementations;

public class CrudFactory<TContext> : ICrudFactory where TContext : DbContext
{
    private readonly IServiceProvider provider;
    private readonly Dictionary<Type, Type> typesСompliance;

    public CrudFactory(IServiceProvider provider)
    {
        this.provider = provider;
        typesСompliance = new Dictionary<Type, Type>();
    }

    public ICrudRepository<TEntity> Get<TEntity>() where TEntity : class, IBaseDataType
    {
        var type = typeof(TEntity);

        if (typesСompliance.ContainsKey(type))
        {
            return provider.GetService(typesСompliance[type]) as ICrudRepository<TEntity>;
        }

        return new CrudRepository<TEntity>(DefaultContext);
    }

    public DbContext Context => DefaultContext;

    private TContext DefaultContext => provider.GetService(typeof(TContext)) as TContext ?? throw new InvalidOperationException();
}
