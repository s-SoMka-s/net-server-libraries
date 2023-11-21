namespace Net.Server.Libraries.Auth.Implementations;

public class Cryptographer
{
    private const string SecretKeyString = "djksfy78s%FGDSG$YG@";
    private const int CryptKeyForChar = 214;

    public static string Crypt(string data)
    {
        if (string.IsNullOrWhiteSpace(data))
        {
            return string.Empty;
        }
        var t = Reverse(data);
        t += SecretKeyString;
        return ToXorAllCharacters(t);
    }

    private static string Reverse(string s)
    {
        var charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    private static string ToXorAllCharacters(string data)
    {
        var result = string.Empty;
        foreach (var t in data)
        {
            result += (char)(CryptKeyForChar ^ t);
        }

        return result;
    }
}