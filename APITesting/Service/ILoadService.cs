namespace APITesting.Service
{
    public interface ILoadService
    {
        Task<string> GetData(int statusCode);
    }
}
