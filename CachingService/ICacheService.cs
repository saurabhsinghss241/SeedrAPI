namespace CachingService
{
    public interface ICacheService
    {
        //Return can be value or null.
        //T should be reference type only.
        Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class;
        Task SetAsync<T>(string key, T value, TimeSpan expiry, CancellationToken cancellationToken = default) where T : class;
        Task RemoveAsync<T>(string key, CancellationToken cancellationToken = default);
    }
}
