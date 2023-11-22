namespace Net.Server.Libraries.Auth.Implementations;

using System.Text;

public class TokenGenerator
{
    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
    private const int TokenLength = 25;
    private static readonly Random Random = new(Environment.TickCount);

    public static string RandomString(int needLength)
    {
        var builder = new StringBuilder(needLength);

        for (var i = 0; i < needLength; ++i)
        {
            builder.Append(Chars[Random.Next(Chars.Length)]);
        }

        return builder.ToString();
    }
}