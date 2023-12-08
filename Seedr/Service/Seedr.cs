using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ResilientClient.Intefaces;
using Seedr.Models;
using Seedr.Service.Interfaces;

namespace Seedr.Service
{
    public class Seedr : ISeedr
    {
        private readonly string _baseURL;
        private readonly IRequestClient _httpClientWrapper;
        //private readonly IStreamTape _streamTape;
        private readonly IConfiguration _configuration;
        public Seedr(IConfiguration configuration, IRequestClient httpClientWrapper)
        {
            _configuration = configuration;
            _baseURL = _configuration.GetValue<string>("Seedr:BaseURL");
            _httpClientWrapper = httpClientWrapper;
            //_streamTape = streamTape;

        }
        public async Task<string> AddFolder(string token, string name)
        {
            try
            {
                var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "add_folder" },
                    { "name",name },
                };
                var content = new FormUrlEncodedContent(formData);

                return await _httpClientWrapper.PostAsync(_baseURL, content);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<AddTorrentResponse> AddTorrent(string token, string magnet, string wishlistId = "", int folderId = -1)
        {
            try
            {
                var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "add_torrent" },
                    { "torrent_magnet", magnet },
                    { "wishlist_id",wishlistId},
                    { "folder_id", folderId.ToString() }
                };
                var content = new FormUrlEncodedContent(formData);

                var res = await _httpClientWrapper.PostAsync(_baseURL, content);

                return JsonConvert.DeserializeObject<AddTorrentResponse>(res);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<string> ChangeName(string token, string name, string password)
        {
            try
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

                return await _httpClientWrapper.PostAsync(_baseURL, content);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<string> ChangePassword(string token, string oldPassword, string newPassword)
        {
            try
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

                return await _httpClientWrapper.PostAsync(_baseURL, content);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<string> CreateArchive(string token, string folderId)
        {
            try
            {
                var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "create_empty_archive" },
                    { "archive_arr",$"[{{\"type\":\"folder\",\"id\":{folderId}}}]" }
                };
                var content = new FormUrlEncodedContent(formData);

                return await _httpClientWrapper.PostAsync(_baseURL, content);

                //return JsonConvert.DeserializeObject<string>(res);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<CommonResponse> DeleteFile(string token, string fileId)
        {
            try
            {
                var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "delete" },
                    { "delete_arr",$"[{{\"type\":\"file\",\"id\":{fileId}}}]" }
                };
                var content = new FormUrlEncodedContent(formData);

                var res = await _httpClientWrapper.PostAsync(_baseURL, content);

                return JsonConvert.DeserializeObject<CommonResponse>(res);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<CommonResponse> DeleteFolder(string token, string folderId)
        {
            try
            {
                var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "delete" },
                    { "delete_arr",$"[{{\"type\":\"folder\",\"id\":{folderId}}}]" }
                };
                var content = new FormUrlEncodedContent(formData);

                var res = await _httpClientWrapper.PostAsync(_baseURL, content);

                return JsonConvert.DeserializeObject<CommonResponse>(res);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<CommonResponse> DeleteTorrent(string token, string torrentId)
        {
            try
            {
                var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "delete" },
                    { "delete_arr",$"[{{\"type\":\"torrent\",\"id\":{torrentId}}}]" }
                };
                var content = new FormUrlEncodedContent(formData);

                var res = await _httpClientWrapper.PostAsync(_baseURL, content);
                return JsonConvert.DeserializeObject<CommonResponse>(res);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<CommonResponse> DeleteWishList(string token, string wishlistId)
        {
            try
            {
                var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "remove_wishlist" },
                    { "id",wishlistId },
                };
                var content = new FormUrlEncodedContent(formData);

                var res = await _httpClientWrapper.PostAsync(_baseURL, content);
                return JsonConvert.DeserializeObject<CommonResponse>(res);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<GenerateURL> FetchFile(string token, string fileId)
        {
            try
            {
                var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "fetch_file" },
                    { "folder_file_id",fileId },
                };
                var content = new FormUrlEncodedContent(formData);

                var res = await _httpClientWrapper.PostAsync(_baseURL, content);
                return JsonConvert.DeserializeObject<GenerateURL>(res);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<string> GetDevice(string token)
        {
            try
            {
                var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "get_devices" },
                };
                var content = new FormUrlEncodedContent(formData);

                return await _httpClientWrapper.PostAsync(_baseURL, content);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<MemBandwidthResponse> GetMemoryBandWidth(string token)
        {
            try
            {
                var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "get_memory_bandwidth" }
                };
                var content = new FormUrlEncodedContent(formData);

                var res = await _httpClientWrapper.PostAsync(_baseURL, content);
                return JsonConvert.DeserializeObject<MemBandwidthResponse>(res);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<string> GetSettings(string token)
        {
            try
            {
                var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "get_settings" },
                };
                var content = new FormUrlEncodedContent(formData);

                return await _httpClientWrapper.PostAsync(_baseURL, content);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ListContentResponse> ListContent(string token, int folderId = 0, string contentType = "folder")
        {
            try
            {
                var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "list_contents" },
                    { "content_type", contentType },
                    { "content_id", folderId.ToString() }
                };

                var content = new FormUrlEncodedContent(formData);
                var response = await _httpClientWrapper.PostAsync(_baseURL, content);
                return JsonConvert.DeserializeObject<ListContentResponse>(response);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<string> RenameFile(string token, string fileId, string renameTo)
        {
            try
            {
                var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "rename" },
                    { "rename_to", renameTo },
                    { "file_id", fileId }
                };
                var content = new FormUrlEncodedContent(formData);

                return await _httpClientWrapper.PostAsync(_baseURL, content);

                //return JsonConvert.DeserializeObject<string>(res);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<string> RenameFolder(string token, string folderId, string renameTo)
        {
            try
            {
                var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "rename" },
                    { "rename_to", renameTo },
                    { "file_id", folderId }
                };
                var content = new FormUrlEncodedContent(formData);

                return await _httpClientWrapper.PostAsync(_baseURL, content);

                //return JsonConvert.DeserializeObject<string>(res);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Scrape Magnet link form the WebPage URL
        public async Task<string> ScanPage(string token, string url)
        {
            try
            {
                var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "scan_page" },
                    { "url", url },
                };
                var content = new FormUrlEncodedContent(formData);

                return await _httpClientWrapper.PostAsync(_baseURL, content);

                //return JsonConvert.DeserializeObject<string>(res);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<string> SearchFile(string token, string query)
        {
            try
            {
                var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "search_files" },
                    { "search_query",query },
                };
                var content = new FormUrlEncodedContent(formData);

                return await _httpClientWrapper.PostAsync(_baseURL, content);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<string> TestToken(string token)
        {
            try
            {
                var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "test" },
                };
                var content = new FormUrlEncodedContent(formData);

                return await _httpClientWrapper.PostAsync(_baseURL, content);

                //return JsonConvert.DeserializeObject<string>(res);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
