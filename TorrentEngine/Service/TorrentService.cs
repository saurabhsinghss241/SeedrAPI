using Seedr.Models;
using Seedr.Service.Interfaces;
using TorrentEngine.Models;
using File = Seedr.Models.File;

namespace TorrentEngine.Service
{
    public class TorrentService : ITorrentService
    {
        private readonly ISeedr _seedr;
        private readonly ILogin _login;
        public TorrentService(ISeedr seedr, ILogin login)
        {
            _seedr = seedr;
            _login = login;
        }
        public async Task<AddTorrentResponse> AddMagnet(AddMagnetRequest request)
        {
            try
            {
                string token = await _login.GetToken(request.Username, request.Token);
                if (string.IsNullOrEmpty(token))
                    return new AddTorrentResponse { ErrorMsg = "Username or Token is Invalid." };
                return await _seedr.AddTorrent(token, request.Magnet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CommonResponse> DeleteFile(DeleteRequest request)
        {
            try
            {
                string token = await _login.GetToken(request.Username, request.Token);
                if (string.IsNullOrEmpty(token))
                    return new CommonResponse { ErrorMsg = "Username or Token is Invalid." };
                return await _seedr.DeleteFile(token, request.Id);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<CommonResponse> DeleteFolder(DeleteRequest request)
        {
            try
            {
                string token = await _login.GetToken(request.Username, request.Token);
                if (string.IsNullOrEmpty(token))
                    return new CommonResponse { ErrorMsg = "Username or Token is Invalid." };
                return await _seedr.DeleteFolder(token, request.Id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CommonResponse> DeleteTorrent(DeleteRequest request)
        {
            try
            {
                string token = await _login.GetToken(request.Username, request.Token);
                if (string.IsNullOrEmpty(token))
                    return new CommonResponse { ErrorMsg = "Username or Token is Invalid." };
                return await _seedr.DeleteTorrent(token, request.Id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CommonResponse> DeleteWishlist(DeleteRequest request)
        {
            try
            {
                string token = await _login.GetToken(request.Username, request.Token);
                if (string.IsNullOrEmpty(token))
                    return new CommonResponse { ErrorMsg = "Username or Token is Invalid." };
                return await _seedr.DeleteWishList(token, request.Id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GenerateLinksResponse> GenerateLinks(GenerateLinksRequest request)
        {
            try
            {
                string token = await _login.GetToken(request.Username, request.Token);
                if (string.IsNullOrEmpty(token))
                    return new GenerateLinksResponse { ErrorMsg = "Username or Token is Invalid." };

                ListContentResponse files = await _seedr.ListContent(token, request.FolderId);
                List<GenerateURL> links = new();
                foreach (File file in files.Files)
                {
                    if (file.Play_video)
                    {
                        var response = await _seedr.FetchFileLink(request.Token, file.Folder_file_id.ToString());
                        links.Add(response);
                    }
                }
                return new GenerateLinksResponse { Links = links };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ListContentResponse> ListAll(ListAllRequest request)
        {
            try
            {
                string token = await _login.GetToken(request.Username, request.Token);
                if (string.IsNullOrEmpty(token))
                    return new ListContentResponse { ErrorMsg = "Username or Token is Invalid." };
                return await _seedr.ListContent(token, request.FolderId);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
