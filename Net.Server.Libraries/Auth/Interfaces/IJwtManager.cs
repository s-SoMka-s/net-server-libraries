using Net.Server.Libraries.Auth.Interfaces.Models;

namespace Net.Server.Libraries.Auth.Interfaces;

public interface IJwtManager
{
    Task<TokenPair> GeneratePairAsync(Guid userId);
}