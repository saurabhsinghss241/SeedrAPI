using Newtonsoft.Json;
using SeedrService.Helpers;
using SeedrService.Models;
using System.Net.Mime;
using System.Xml.Linq;

namespace SeedrService.Service
{
    public class Seedr : ISeedr
    {
        private readonly string _baseURL;
        private readonly HttpClientWrapper _httpClientWrapper;
        public Seedr(IConfiguration configuration, HttpClientWrapper httpClientWrapper)
        {
            _baseURL = configuration.GetValue<string>("Seedr:BaseURL");
            _httpClientWrapper = httpClientWrapper;
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

        public async Task<string> AddTorrent(string token, string magnet)
        {
            throw new NotImplementedException();
        }

        public async Task<string> ChangeName(string token, string name, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<string> ChangePassword(string token, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
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
        public async Task<string> Delete(string token, int id)
        {
            try
            {
                var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "delete" },
                    { "delete_arr",$"[{{\"type\":\"folder\",\"id\":{id}}}]" }
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

        public async Task<string> DeleteFile(string token, string fileId)
        {
            try
            {
                var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "delete" },
                    { "delete_arr",$"[{{\"type\":\"folder\",\"id\":{fileId}}}]" }
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

        public async Task<string> DeleteFolder(string token, string folderId)
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

                return await _httpClientWrapper.PostAsync(_baseURL, content);
                
                //return JsonConvert.DeserializeObject<string>(res);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> DeleteTorrent(string token, string torrentId)
        {
            try
            {
                var formData = new Dictionary<string, string>
                {
                    { "access_token", token },
                    { "func", "delete" },
                    { "delete_arr",$"[{{\"type\":\"folder\",\"id\":{torrentId}}}]" }
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

        public async Task<string> DeleteWishList(string token, string wishlistId)
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

                return await _httpClientWrapper.PostAsync(_baseURL, content);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> FetchFile(string token, string fileId)
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

                return await _httpClientWrapper.PostAsync(_baseURL, content);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> GetDevice(string token)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public async Task<string> ListContent(string token, int folderId = 0, string contentType = "folder")
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

                return await _httpClientWrapper.PostAsync(_baseURL, content);

                //return JsonConvert.DeserializeObject<string>(res);
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

        public async Task<string> ScanPage(string token, string url)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
