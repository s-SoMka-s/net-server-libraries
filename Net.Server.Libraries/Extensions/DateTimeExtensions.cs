namespace Net.Server.Libraries.Extensions;

public static class DateTimeExtensions
{
    public static long ToTimestamp(this DateTimeOffset date)
    {
        var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        return (long)Math.Floor((date.ToUniversalTime() - dateTime).TotalSeconds);
    }

    public static long ToTimestamp(this DateTime date)
    {
        var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        return (long)Math.Floor((date.ToUniversalTime() - dateTime).TotalSeconds);
    }

    public static DateTime ParseTimestamp(long timestamp) => new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(timestamp).ToUniversalTime();
}