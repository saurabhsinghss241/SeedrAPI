namespace HashingService
{
    public interface IHashingService
    {
        string Hash(string key);
        bool IsValidHash(string key, string hash);
    }
}
