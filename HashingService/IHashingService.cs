namespace HashingService
{
    public interface IHashingService
    {
        string Encrypt(string key);
        bool Decrypt(string key, string hash);
    }
}
