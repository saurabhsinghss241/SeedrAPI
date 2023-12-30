using Newtonsoft.Json;
using ResilientClient.Intefaces;
using Seedr.Models;
using Seedr.Service.Interfaces;

namespace Seedr.Service
{
    public class Seedr : ISeedr
    {
        private static string _baseURL;
        private readonly IRequestClient _httpClient;
        public Seedr(ISeedrConfig config, IRequestClient httpClientWrapper)
        {
            _baseURL = config.BaseURL;
            _httpClient = httpClientWrapper;
        }
        public async Task<string> AddFolder(string token, string name)
        {
            var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "add_folder" },
                    { "name",name },
                };
            var content = new FormUrlEncodedContent(formData);

            return await _httpClient.PostAsync(_baseURL, content);
        }
        public async Task<AddTorrentResponse> AddTorrent(string token, string magnet, string wishlistId = "", string folderId = "-1")
        {
            var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "add_torrent" },
                    { "torrent_magnet", magnet },
                    { "wishlist_id",wishlistId},
                    { "folder_id", folderId}
                };
            var content = new FormUrlEncodedContent(formData);

            var res = await _httpClient.PostAsync(_baseURL, content);

            return JsonConvert.DeserializeObject<AddTorrentResponse>(res);
        }
        public async Task<string> ChangeName(string token, string name, string password)
        {
            var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "user_account_modify" },
                    { "setting","fullname" },
                    { "password", password },
                    { "fullname", name },
                };
            var content = new FormUrlEncodedContent(formData);

            return await _httpClient.PostAsync(_baseURL, content);
        }
        public async Task<string> ChangePassword(string token, string oldPassword, string newPassword)
        {
            var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "user_account_modify" },
                    { "setting","password" },
                    { "password", oldPassword },
                    { "new_password", newPassword },
                    { "new_password_repeat",newPassword },
                };
            var content = new FormUrlEncodedContent(formData);

            return await _httpClient.PostAsync(_baseURL, content);
        }
        public async Task<string> CreateArchive(string token, string folderId)
        {
            var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "create_empty_archive" },
                    { "archive_arr",$"[{{\"type\":\"folder\",\"id\":{folderId}}}]" }
                };
            var content = new FormUrlEncodedContent(formData);

            return await _httpClient.PostAsync(_baseURL, content);
        }
        public async Task<CommonResponse> DeleteFile(string token, string fileId)
        {
            var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "delete" },
                    { "delete_arr",$"[{{\"type\":\"file\",\"id\":{fileId}}}]" }
                };
            var content = new FormUrlEncodedContent(formData);

            var res = await _httpClient.PostAsync(_baseURL, content);

            return JsonConvert.DeserializeObject<CommonResponse>(res);
        }
        public async Task<CommonResponse> DeleteFolder(string token, string folderId)
        {
            var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "delete" },
                    { "delete_arr",$"[{{\"type\":\"folder\",\"id\":{folderId}}}]" }
                };
            var content = new FormUrlEncodedContent(formData);

            var res = await _httpClient.PostAsync(_baseURL, content);

            return JsonConvert.DeserializeObject<CommonResponse>(res);
        }
        public async Task<CommonResponse> DeleteTorrent(string token, string torrentId)
        {
            var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "delete" },
                    { "delete_arr",$"[{{\"type\":\"torrent\",\"id\":{torrentId}}}]" }
                };
            var content = new FormUrlEncodedContent(formData);

            var res = await _httpClient.PostAsync(_baseURL, content);
            return JsonConvert.DeserializeObject<CommonResponse>(res);
        }
        public async Task<CommonResponse> DeleteWishList(string token, string wishlistId)
        {
            var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "remove_wishlist" },
                    { "id",wishlistId },
                };
            var content = new FormUrlEncodedContent(formData);

            var res = await _httpClient.PostAsync(_baseURL, content);
            return JsonConvert.DeserializeObject<CommonResponse>(res);
        }
        public async Task<GenerateURL> FetchFileLink(string token, string fileId)
        {
            var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "fetch_file" },
                    { "folder_file_id",fileId },
                };
            var content = new FormUrlEncodedContent(formData);

            var res = await _httpClient.PostAsync(_baseURL, content);
            return JsonConvert.DeserializeObject<GenerateURL>(res);
        }
        public async Task<string> GetDevice(string token)
        {
            var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "get_devices" },
                };
            var content = new FormUrlEncodedContent(formData);

            return await _httpClient.PostAsync(_baseURL, content);
        }
        public async Task<MemBandwidthResponse> GetMemoryBandWidth(string token)
        {
            var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "get_memory_bandwidth" }
                };
            var content = new FormUrlEncodedContent(formData);

            var res = await _httpClient.PostAsync(_baseURL, content);
            return JsonConvert.DeserializeObject<MemBandwidthResponse>(res);
        }
        public async Task<string> GetSettings(string token)
        {
            var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "get_settings" },
                };
            var content = new FormUrlEncodedContent(formData);

            return await _httpClient.PostAsync(_baseURL, content);
        }
        public async Task<ListContentResponse> ListContent(string token, string folderId = "0", string contentType = "folder")
        {
            var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "list_contents" },
                    { "content_type", contentType },
                    { "content_id", folderId }
                };

            var content = new FormUrlEncodedContent(formData);
            var response = await _httpClient.PostAsync(_baseURL, content);
            return JsonConvert.DeserializeObject<ListContentResponse>(response);
        }
        public async Task<string> RenameFile(string token, string fileId, string renameTo)
        {
            var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "rename" },
                    { "rename_to", renameTo },
                    { "file_id", fileId }
                };
            var content = new FormUrlEncodedContent(formData);

            return await _httpClient.PostAsync(_baseURL, content);
        }
        public async Task<string> RenameFolder(string token, string folderId, string renameTo)
        {
            var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "rename" },
                    { "rename_to", renameTo },
                    { "file_id", folderId }
                };
            var content = new FormUrlEncodedContent(formData);

            return await _httpClient.PostAsync(_baseURL, content);
        }

        //Scrape Magnet link form the WebPage URL
        public async Task<string> ScanPage(string token, string url)
        {
            var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "scan_page" },
                    { "url", url },
                };
            var content = new FormUrlEncodedContent(formData);

            return await _httpClient.PostAsync(_baseURL, content);
        }
        public async Task<string> SearchFile(string token, string query)
        {
            var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "search_files" },
                    { "search_query",query },
                };
            var content = new FormUrlEncodedContent(formData);

            return await _httpClient.PostAsync(_baseURL, content);
        }
        public async Task<bool> TestToken(string token)
        {
            var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "test" },
                };
            var content = new FormUrlEncodedContent(formData);
            var response = await _httpClient.PostAsync(_baseURL, content);

            var res = JsonConvert.DeserializeObject<TestTokenResponse>(response);
            return res != null && res.Result;
        }
    }
}
