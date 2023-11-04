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
                _memoryCache.Set($"{username}::AccessToken", result, TimeSpan.FromSeconds(result.Expires_in - 60));

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //public async Task<string> Authorize(string deviceCode)
        //{
        //    var response = string.Empty;
        //    if (!string.IsNullOrWhiteSpace(deviceCode))
        //    {
        //        var queryParameters = new Dictionary<string, string>
        //        {
        //            { "client_id", "seedr_xbmc" },
        //            { "device_code", deviceCode }
        //        };

        //        string queryString = string.Join("&", queryParameters.Select(kvp => $"{HttpUtility.UrlEncode(kvp.Key)}={HttpUtility.UrlEncode(kvp.Value)}"));

        //        // Combine the base URL and the query string
        //        string urlWithQueryParams = _authUsingDeviceCodeURL + "?" + queryString;
        //        response = await _httpClientWrapper.GetAsync(urlWithQueryParams);

        //    }
        //    else if (!string.IsNullOrWhiteSpace(_username) && !string.IsNullOrWhiteSpace(_password)) {
        //        var formData = new Dictionary<string, string>
        //        {
        //            { "username", _username },
        //            { "password", _password },
        //            { "grant_type","password"},
        //            { "client_id","seedr_chrome" },
        //            { "type","login" }
        //            // Add more key-value pairs for additional form fields as needed
        //        };
        //        var content = new FormUrlEncodedContent(formData);


        //        response = await _httpClientWrapper.PostAsync(_authUsingUsernamePassURL, content);              
        //    }
        //    else
        //        throw new Exception("No device code or email/password provided");

        //    if (response.Contains("access_token"))
        //    {
        //        //_token = await CreateToken(response,deviceCode);
        //    }
        //    return response;
        //}


        public Task<string> CreateToken(string refreshToken, string deviceCode)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetDeviceCode()
        {
            throw new NotImplementedException();
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
