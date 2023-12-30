using Microsoft.AspNetCore.Mvc;
using Seedr.Models;
using TorrentEngine.Models;
using TorrentEngine.Service;

namespace TorrentEngine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TorrentController : ControllerBase
    {
        private readonly ILogin _login;
        private readonly ITorrentService _torrentService;
        public TorrentController(ILogin login, ITorrentService torrentService)
        {
            _login = login;
            _torrentService = torrentService;
        }

        [HttpPost]
        [Route("GenerateAuthToken")]
        public async Task<AuthResponse> GenerateAuthToken(AuthRequest request)
        {
            try
            {
                return await _login.Authenticate(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new AuthResponse { ErrorMsg = ex.Message };
            }
        }

        [HttpPost]
        [Route("AddMagnet")]
        public async Task<AddTorrentResponse> AddMagnet(AddMagnetRequest request)
        {
            try
            {
                return await _torrentService.AddMagnet(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new AddTorrentResponse { ErrorMsg = ex.Message };
            }
        }

        [HttpPost]
        [Route("ListAll")]
        public async Task<ListContentResponse> ListAll(ListAllRequest request)
        {
            try
            {
                return await _torrentService.ListAll(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ListContentResponse { ErrorMsg = ex.Message };
            }
        }

        [HttpPost]
        [Route("GenerateLinks")]
        public async Task<GenerateLinksResponse> GenerateLinks(GenerateLinksRequest request)
        {
            try
            {
                return await _torrentService.GenerateLinks(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new GenerateLinksResponse { ErrorMsg = ex.Message };
            }
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<CommonResponse> Delete(DeleteRequest request)
        {
            try
            {
                return request.IdType switch
                {
                    IdType.Folder => await _torrentService.DeleteFolder(request),
                    IdType.File => await _torrentService.DeleteFile(request),
                    IdType.Torrent => await _torrentService.DeleteTorrent(request),
                    IdType.Wishlist => await _torrentService.DeleteWishlist(request),
                    _ => throw new ArgumentException("Provide valid IdType."),
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new CommonResponse { ErrorMsg = ex.Message };
            }
        }
    }
}
