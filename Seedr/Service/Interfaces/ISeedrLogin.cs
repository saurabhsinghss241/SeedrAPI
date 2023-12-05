using Seedr.Models;

namespace Seedr.Service.Interfaces
{
    public interface ISeedrLogin
    {
        Task<AuthResponse> LoginUsingUsernamePassword(string username, string password);
        Task<AuthResponse> RefreshAccessToken(string refreshToken);
    }
}
