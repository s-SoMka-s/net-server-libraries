using Microsoft.EntityFrameworkCore;

namespace Net.Server.Libraries.Db.Repository.Interfaces;

public interface IRawRepository
{
    DbContext Context { get; }
}
