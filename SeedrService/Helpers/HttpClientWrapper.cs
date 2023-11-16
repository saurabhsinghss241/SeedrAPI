namespace SeedrService.Helpers
{
    public class HttpClientWrapper
    {
        private readonly HttpClient _httpClient;
        public HttpClientWrapper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetAsync(string url)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(new Uri(url));
            response.EnsureSuccessStatusCode(); // Ensure a successful response

            return await response.Content.ReadAsStringAsync();
        }
        public async Task<T> GetJSONAsync<T>(string url)
        {
            return await _httpClient.GetFromJsonAsync<T>(url);
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
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
            //if (response.IsSuccessStatusCode)
            //{
            //    string responseBody = await response.Content.ReadAsStringAsync();
            //    return responseBody;
            //}
            //else
            //{
            //    return "Request Failed";
            //}
        }

    }
}
