namespace CachingService
{
    public interface ICaching
    {
        Task<string> GetCache<T>(string key);
        Task<bool> SaveCache<T>(string key, T data, TimeSpan expiry);
    }
}
