using SeedrService.Models;

namespace SeedrService.Service
{
    public interface ISeedrLogin
    {
        Task<AuthResponse> LoginUsingUsernamePassword(string username, string password);
        //Task<string> Authorize(string deviceCode);
        Task<string> GetDeviceCode();
        Task<string> CreateToken(string refreshToken, string deviceCode);
        Task<AuthResponse> RefreshAccessToken(string refreshToken);
    }
}
