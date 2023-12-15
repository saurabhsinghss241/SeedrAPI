namespace CachingService.Interfaces
{
    public interface IRedisCacheConfig
    {
        string HostName { get; set; }
        string Port { get; set; }
        string Username { get; set; }
        string Password { get; set; }
    }
}
