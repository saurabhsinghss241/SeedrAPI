namespace APITesting.Service
{
    public interface ILoadService
    {
        Task<string> GetDataNew(int statusCode);
        Task<string> GetDataOld(int statusCode);
    }
}
