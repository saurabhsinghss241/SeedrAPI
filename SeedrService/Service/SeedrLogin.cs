using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using SeedrService.Helpers;
using SeedrService.Models;

namespace SeedrService.Service
{
    public class SeedrLogin : ISeedrLogin
    {
        private readonly string _authUsingUsernamePassURL;
        private readonly HttpClientWrapper _httpClientWrapper;
        private readonly IMemoryCache _memoryCache;
        public SeedrLogin(HttpClientWrapper httpClientWrapper, IConfiguration configuration, IMemoryCache memoryCache)
        {
            _authUsingUsernamePassURL = configuration.GetValue<string>("Seedr:AuthUsingUsernamePassURL");
            _httpClientWrapper = httpClientWrapper;
            _memoryCache = memoryCache;
        }
        public async Task<AuthResponse> LoginUsingUsernamePassword(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                throw new Exception("Username or Password empty.");

            AuthResponse response = null;

            if (_memoryCache.TryGetValue($"{username}::AccessToken", out response))
            {
                return response;
            }

            try
            {
                var formData = new Dictionary<string, string>
                {
                    { "username", username },
                    { "password", password },
                    { "grant_type","password"},
                    { "client_id","seedr_chrome" },
                    { "type","login" }
                    // Add more key-value pairs for additional form fields as needed
                };
                var content = new FormUrlEncodedContent(formData);

                var res = await _httpClientWrapper.PostAsync(_authUsingUsernamePassURL, content);
                var result = JsonConvert.DeserializeObject<AuthResponse>(res);

                //var encryptedAccessToken = TokenEncryptionHelper.Encrypt(result.Access_token);
                //var encryptedRefreshToken = TokenEncryptionHelper.Encrypt(result.Refresh_token);

                //result.Access_token = encryptedAccessToken;
                //result.Refresh_token = encryptedRefreshToken;

                _memoryCache.Set($"{username}::AccessToken", result, TimeSpan.FromSeconds(result.Expires_in - 60));

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<AuthResponse> RefreshAccessToken(string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
                throw new ArgumentNullException("Provide Valid RefreshToken");

            try
            {
                var formData = new Dictionary<string, string>
                {
                    { "grant_type","refresh_token"},
                    { "client_id","seedr_chrome" },
                    { "refresh_token" , refreshToken}  
                    // Add more key-value pairs for additional form fields as needed
                };
                var content = new FormUrlEncodedContent(formData);

                var res = await _httpClientWrapper.PostAsync(_authUsingUsernamePassURL, content);
                return JsonConvert.DeserializeObject<AuthResponse>(res);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
