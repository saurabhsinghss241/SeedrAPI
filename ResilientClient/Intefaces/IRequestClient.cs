namespace ResilientClient.Intefaces
{
    public interface IRequestClient
    {
        string BaseUrl { get; }
        Task<string> GetAsync(string url);
        Task<string> PostAsync(string url, string content);
        Task<string> PostAsync(string url, FormUrlEncodedContent content);
    }
}
