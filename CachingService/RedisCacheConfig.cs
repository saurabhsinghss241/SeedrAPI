namespace CachingService.Interfaces
{
    public class RedisCacheConfig : IRedisCacheConfig
    {
        public string HostName { get; set; }
        public string Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
