using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SeedrService.Helpers;
using SeedrService.Models;
using System.Diagnostics;

namespace SeedrService.Service
{
    public class Seedr : ISeedr
    {
        private readonly string _baseURL;
        private readonly HttpClientWrapper _httpClientWrapper;
        private readonly IStreamTape _streamTape;
        private readonly IConfiguration _configuration;
        public Seedr(IConfiguration configuration, HttpClientWrapper httpClientWrapper, IStreamTape streamTape)
        {
            _baseURL = configuration.GetValue<string>("Seedr:BaseURL");
            _httpClientWrapper = httpClientWrapper;
            _streamTape = streamTape;
            _configuration = configuration;
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

        public async Task<GenerateURL> MagnetToDirectLink(string token, string magnet)
        {
            try
            {
                var addMagnetResponse = await AddTorrent(token, magnet);

                if (addMagnetResponse.Code != 200 || addMagnetResponse.Wt != null)
                    return new GenerateURL() { Error = "Provide Valid and Healthy Torrent. Max Size 2.5 GB" };

                (bool success, string folderId, string message) = await ProgressStatus(token, addMagnetResponse.User_torrent_id);
                if (success && !string.IsNullOrEmpty(folderId))
                {
                    //Using FolderId List all Files and Pick the biggest and then generate link
                    var files = await ListContent(token, int.Parse(folderId));

                    if (files.Files != null)
                    {
                        var maxFile = files.Files.OrderByDescending(x => x.Size).First();
                        var directLink = await FetchFile(token, maxFile.Folder_file_id.ToString());
                        (bool streamStatus, string streamUrl) = await DirectLinkToStream(directLink.Url);
                        if (streamStatus)
                        {
                            await this.DeleteFolder(token, folderId);
                            return new GenerateURL() { Result = true, Url = streamUrl };
                        }
                    }
                }
                return new GenerateURL() { Error = message };
            }
            catch (Exception ex)
            {
                return new GenerateURL() { Error = ex.Message };
            }
        }
        private async Task<(bool, string, string)> ProgressStatus(string token, int torrentId)
        {
            var getList = await ListContent(token);

            var progressCheckURL = string.Empty;
            var folderId = string.Empty;
            var message = "Success";

            if (getList.Torrents == null)
            {
                return (true, folderId, "Processing Queue is Empty");
            }

            foreach (var torrent in getList.Torrents)
            {
                if (torrent.Id == torrentId)
                {
                    progressCheckURL = torrent.Progress_url;
                    if (torrent.Warnings != null)
                        message = torrent.Warnings;

                    break;
                }
            }

            if (progressCheckURL != string.Empty)
            {
                float completed = 0;
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                try
                {
                    while (completed < 101 && stopwatch.Elapsed < TimeSpan.FromSeconds(120))
                    {
                        await Task.Delay(10000);
                        var response = await _httpClientWrapper.GetAsync(progressCheckURL);

                        int n = response.Length;
                        var result = response[2..^1];

                        var currentProgress = JsonConvert.DeserializeObject<ProgressResponse>(result);

                        completed = currentProgress.stats != null ? currentProgress.stats.progress : completed;
                        folderId = currentProgress.folder_created;
                        if (currentProgress.warnings != null)
                        {
                            message = currentProgress.warnings;
                        }
                    }

                    if (completed < 101)
                    {
                        await DeleteTorrent(token, torrentId.ToString());
                        return (false, folderId, message);
                    }
                }
                catch (Exception ex)
                {
                    if (string.IsNullOrEmpty(folderId))
                        await DeleteTorrent(token, torrentId.ToString());

                    message.Concat($",{ex.Message}");
                }

            }

            return (true, folderId, message);
        }

        private async Task<(bool, string)> DirectLinkToStream(string url)
        {
            try
            {
                string login = _configuration.GetValue<string>("StreamTapeLogin");
                string key = _configuration.GetValue<string>("StreamTapeKey");
                var remoteupload = await _streamTape.AddRemoteUpload(login, key, url, "QOQ8AXFChrY", "");
                var stopwatch = new Stopwatch();

                while (true && stopwatch.Elapsed < TimeSpan.FromSeconds(240))
                {
                    var progress = await _streamTape.CheckRemoteUploadProgress(login, key, remoteupload.result.id);
                    if (progress.status == "finished")
                    {
                        return (true, progress.url);
                    }
                    await Task.Delay(10000);
                }
                return (false, default);
            }
            catch (Exception)
            {

                return (false, default);
            }
        }
    }
}
