using Newtonsoft.Json;
using ResilientClient.Intefaces;
using StreamTape.Models;
using StreamTape.Service.Interfaces;

namespace StreamTape.Service
{
    public class StreamTape : IStreamTape
    {
        private readonly IRequestClient _httpClient;
        public StreamTape(IRequestClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<AddRUResponse> AddRemoteUpload(string login, string key, string url, string folder_id, string name)
        {
            try
            {
                string requestURL = _httpClient.BaseUrl + $"/remotedl/add?login={login}&key={key}&url={Uri.EscapeDataString(url)}&folder={folder_id}amp;name={Uri.EscapeDataString(name)}";
                var response = await _httpClient.GetAsync(requestURL);
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
                string requestURL = $"{_httpClient.BaseUrl}/remotedl/status?login={login}&key={key}&id={uploadId}";
                var response = await _httpClient.GetAsync(requestURL);
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
                string requestURL = $"{_httpClient.BaseUrl}/remotedl/remove?login={login}&key={key}&id={uploadId}";
                var response = await _httpClient.GetAsync(requestURL);
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
                string requestURL = $"{_httpClient.BaseUrl}/file/rename?login={login}&key={key}&file={linkid}&name={name}";
                var response = await _httpClient.GetAsync(requestURL);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
