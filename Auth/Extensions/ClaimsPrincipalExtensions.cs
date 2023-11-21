using System.Security.Claims;

namespace Net.Server.Libraries.Auth.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static bool TryGetId(this ClaimsPrincipal user, out Guid userId)
    {
        userId = default;
        var claim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        return default != claim && Guid.TryParse(claim.Value, out userId);
    }
}