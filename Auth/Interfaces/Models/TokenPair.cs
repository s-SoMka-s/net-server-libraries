namespace Net.Server.Libraries.Auth.Interfaces.Models;

public class TokenPair
{
    public string Access { get; set; }
    public string Refresh { get; set; }

    public long AccessExpiryDate { get; set; }
    public long RefreshExpiryDate { get; set; }
}