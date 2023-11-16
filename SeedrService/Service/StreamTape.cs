using Newtonsoft.Json;
using SeedrService.Helpers;
using SeedrService.Models;

namespace SeedrService.Service
{
    public class StreamTape : IStreamTape
    {
        private readonly string _baseURL;
        private readonly HttpClientWrapper _httpClientWrapper;
        public StreamTape(IConfiguration configuration, HttpClientWrapper httpClientWrapper)
        {
            _baseURL = configuration.GetValue<string>("StreamTape:BaseURL");
            _httpClientWrapper = httpClientWrapper;
        }
        public async Task<AddRUResponse> AddRemoteUpload(string login, string key, string url, string folder_id, string name)
        {
            try
            {
                string requestURL = _baseURL + $"/remotedl/add?login={login}&key={key}&url={Uri.EscapeDataString(url)}&folder={folder_id}amp;name={Uri.EscapeDataString(name)}";
                var response = await _httpClientWrapper.GetAsync(requestURL);
                return JsonConvert.DeserializeObject<AddRUResponse>(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UploadInfo> CheckRemoteUploadProgress(string login, string key, string uploadId)
        {
            try
            {
                string requestURL = $"{_baseURL}/remotedl/status?login={login}&key={key}&id={uploadId}";
                var response = await _httpClientWrapper.GetAsync(requestURL);
                var jsonresponse = JsonConvert.DeserializeObject<ProgressRUResponse>(response);
                UploadInfo result = JsonConvert.DeserializeObject<UploadInfo>(JsonConvert.SerializeObject(jsonresponse.result[$"{uploadId}"]));

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> RemoveRemoteUpload(string login, string key, string uploadId)
        {
            try
            {
                string requestURL = $"{_baseURL}/remotedl/remove?login={login}&key={key}&id={uploadId}";
                var response = await _httpClientWrapper.GetAsync(requestURL);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> RenameFile(string login, string key, string linkid, string name)
        {
            try
            {
                string requestURL = $"{_baseURL}/file/rename?login={login}&key={key}&file={linkid}&name={name}";
                var response = await _httpClientWrapper.GetAsync(requestURL);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
