using Net.Server.Libraries.Auth.Interfaces;
using System.Security.Cryptography;
using System.Text;


namespace Net.Server.Libraries.Auth.Implementations;

public class CryptographyService : ICryptographyService
{
    public string CreateSalt(int size)
    {
        //Generate a cryptographic random number.
        var buff = new byte[size];
        var generator = RandomNumberGenerator.Create();
        generator.GetBytes(buff);
        return Convert.ToBase64String(buff);
    }

    public string GenerateHash(string input, string salt)
    {
        var bytes = Encoding.UTF8.GetBytes(input + salt);
        var hash = MD5.Create().ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    public bool ConfirmPassword(string plainTextInput, string hashedInput, string salt)
    {
        var newHashedPin = GenerateHash(plainTextInput, salt);
        return newHashedPin.Equals(hashedInput);
    }

    public string GeneratePassword(int length)
    {
        using var generator = RandomNumberGenerator.Create();
        var tokenBuffer = new byte[length];
        generator.GetBytes(tokenBuffer);
        return Convert.ToBase64String(tokenBuffer);
    }
}