using System.Net.Http;

namespace SeedrService.Helpers
{
    public class HttpClientWrapper
    {
        private readonly HttpClient _httpClient;
        public HttpClientWrapper()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetAsync(string url)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Ensure a successful response

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PostAsync(string url, string content)
        {
            HttpContent httpContent = new StringContent(content);
            HttpResponseMessage response = await _httpClient.PostAsync(url, httpContent);
            response.EnsureSuccessStatusCode(); // Ensure a successful response

            return await response.Content.ReadAsStringAsync();
        }
        public async Task<string> PostAsync(string url, FormUrlEncodedContent content)
        {
            HttpResponseMessage response = await _httpClient.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            else
            {
                return "Request Failed";
            }
        }
    }
}
