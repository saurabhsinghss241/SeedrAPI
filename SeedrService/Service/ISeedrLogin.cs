using SeedrService.Models;

namespace SeedrService.Service
{
    public interface ISeedrLogin
    {
        Task<AuthResponse> LoginUsingUsernamePassword(string username, string password);
        Task<AuthResponse> RefreshAccessToken(string refreshToken);
    }
}
