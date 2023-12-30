using CachingService.Interfaces;
using HashingService;
using Seedr.Models;
using Seedr.Service.Interfaces;

namespace TorrentEngine.Service
{
    public class Login : ILogin
    {
        private readonly ISeedrLogin _seedrLogin;
        private readonly ICacheService _cache;
        private readonly IHashingService _hashingService;
        public Login(ISeedrLogin seedrLogin, ICacheService cache, IHashingService hashingService)
        {
            _seedrLogin = seedrLogin;
            _cache = cache;
            _hashingService = hashingService;
        }
        public async Task<AuthResponse> Authenticate(AuthRequest request)
        {
            string key = $"{request.Username}::AccessToken";
            AuthResponse response = await _cache.GetAsync<AuthResponse>(key);

            if (response != null) return response;

            var res = await _seedrLogin.LoginUsingUsernamePassword(request.Username, request.Password);
            await _cache.SetAsync<AuthResponse>(key, res, TimeSpan.FromSeconds(res.Expires_in - 300));

            res.Access_token = _hashingService.Hash(res.Access_token);
            res.Refresh_token = _hashingService.Hash(res.Refresh_token);
            return res;
        }
        public async Task<string> GetToken(string username, string encryptedToken)
        {
            if (string.IsNullOrEmpty(username)) return null;

            string key = $"{username}::AccessToken";
            AuthResponse response = await _cache.GetAsync<AuthResponse>(key);
            return _hashingService.IsValidHash(response.Access_token, encryptedToken) ? response.Access_token : null;
        }
    }
}
