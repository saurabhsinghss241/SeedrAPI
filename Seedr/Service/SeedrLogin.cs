using Newtonsoft.Json;
using ResilientClient.Intefaces;
using Seedr.Models;
using Seedr.Service.Interfaces;

namespace Seedr.Service
{
    public class SeedrLogin : ISeedrLogin
    {
        private static string _baseURL;
        private readonly IRequestClient _httpclient;
        public SeedrLogin(ISeedrLoginConfig config, IRequestClient httpClientWrapper)
        {
            _baseURL = config.AuthUsingUsernamePassURL;
            _httpclient = httpClientWrapper;
        }
        public async Task<AuthResponse> LoginUsingUsernamePassword(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                throw new Exception("Username or Password empty.");

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

                var res = await _httpclient.PostAsync(_baseURL, content);
                var result = JsonConvert.DeserializeObject<AuthResponse>(res);

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

                var res = await _httpclient.PostAsync(_baseURL, content);
                return JsonConvert.DeserializeObject<AuthResponse>(res);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
