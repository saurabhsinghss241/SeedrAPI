using Seedr.Models;

namespace TorrentEngine.Service
{
    public interface ILogin
    {
        Task<AuthResponse> Authenticate(AuthRequest request);
        Task<string> GetToken(string username, string encryptedToken);
    }
}
