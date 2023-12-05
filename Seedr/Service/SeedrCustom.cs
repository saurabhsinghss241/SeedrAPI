using Newtonsoft.Json;
using ResilientClient;
using Seedr.Models;
using Seedr.Service.Interface;
using System.Diagnostics;

namespace Seedr.Service
{
    public class SeedrCustom : ISeedrCustom
    {
        private readonly ISeedr _seedr;
        private readonly IHttpClientWrapper _httpClientWrapper;
        public SeedrCustom(ISeedr seedr, IHttpClientWrapper httpClientWrapper)
        {
            _seedr = seedr;
            _httpClientWrapper = httpClientWrapper;
        }
        public async Task<GenerateURL> MagnetToDirectLink(string token, string magnet)
        {
            try
            {
                var addMagnetResponse = await _seedr.AddTorrent(token, magnet);

                if (addMagnetResponse.Code != 200 || addMagnetResponse.Wt != null)
                    return new GenerateURL() { Error = "Provide Valid and Healthy Torrent. Max Size 2.5 GB" };

                (bool success, string folderId, string message) = await IsTorrentProcessed(token, addMagnetResponse.User_torrent_id);
                if (success && !string.IsNullOrEmpty(folderId))
                {
                    //Using FolderId List all Files and Pick the biggest and then generate link
                    var files = await _seedr.ListContent(token, int.Parse(folderId));

                    if (files.Files != null)
                    {
                        var maxFile = files.Files.OrderByDescending(x => x.Size).First();
                        var directLink = await _seedr.FetchFile(token, maxFile.Folder_file_id.ToString());
                        //TODO
                        //(bool streamStatus, string streamUrl) = await DirectLinkToStream(directLink.Url);
                        //if (streamStatus)
                        {
                            await _seedr.DeleteFolder(token, folderId);
                            return new GenerateURL() { Result = true, Url = "streamUrl" };
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
        public async Task<(bool, string, string)> IsTorrentProcessed(string token, int torrentId)
        {
            var getList = await _seedr.ListContent(token);

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

                        var currentProgress = await GetCurrentTorrentProgressStatus(progressCheckURL);

                        completed = currentProgress.stats != null ? currentProgress.stats.progress : completed;
                        folderId = currentProgress.folder_created;
                        if (currentProgress.warnings != null)
                        {
                            message = currentProgress.warnings;
                        }
                    }

                    if (completed < 101)
                    {
                        await _seedr.DeleteTorrent(token, torrentId.ToString());
                        return (false, folderId, message);
                    }
                }
                catch (Exception ex)
                {
                    if (string.IsNullOrEmpty(folderId))
                        await _seedr.DeleteTorrent(token, torrentId.ToString());

                    message.Concat($",{ex.Message}");
                }

            }

            return (true, folderId, message);
        }

        public async Task<ProgressResponse> GetCurrentTorrentProgressStatus(string progressUrl)
        {
            try
            {
                var response = await _httpClientWrapper.GetAsync(progressUrl);

                int n = response.Length;
                var result = response[2..^1];

                return JsonConvert.DeserializeObject<ProgressResponse>(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        //private async Task<(bool, string)> DirectLinkToStream(string url)
        //{
        //    try
        //    {
        //        string login = _configuration.GetValue<string>("StreamTapeLogin");
        //        string key = _configuration.GetValue<string>("StreamTapeKey");
        //        var remoteupload = await _streamTape.AddRemoteUpload(login, key, url, "QOQ8AXFChrY", "");
        //        var stopwatch = new Stopwatch();

        //        while (true && stopwatch.Elapsed < TimeSpan.FromSeconds(240))
        //        {
        //            var progress = await _streamTape.CheckRemoteUploadProgress(login, key, remoteupload.result.id);
        //            if (progress.status == "finished")
        //            {
        //                return (true, progress.url);
        //            }
        //            await Task.Delay(10000);
        //        }
        //        return (false, default);
        //    }
        //    catch (Exception)
        //    {

        //        return (false, default);
        //    }
        //}
    }
}
