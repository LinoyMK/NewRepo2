namespace Search.Infrastructure.Security.Abstract.Cryptography
{
    public interface ICryptographicProvider
    {
        string Encrypt(string data, string password);

        string Decrypt(string encryptedData, string password);
    }
}
