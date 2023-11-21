namespace Net.Server.Libraries.Auth.Interfaces;

public interface ICryptographyService
{
    public string CreateSalt(int size);

    public string GenerateHash(string input, string salt);

    public bool ConfirmPassword(string plainTextInput, string hashedInput, string salt);

    public string GeneratePassword(int length);
}