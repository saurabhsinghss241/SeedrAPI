using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using SeedrService.Helpers;
using SeedrService.Models;
using SeedrService.Service;

namespace SeedrService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedrController : ControllerBase
    {
        private readonly ISeedr _seedr;

        public SeedrController(ISeedr seedr)
        {
            _seedr = seedr;
        }

        [HttpGet]
        [Route("AddMagnet")]
        public async Task<AddTorrentResponse> AddMagnet(string magnet)
        {
            try
            {
                var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

                if (string.IsNullOrWhiteSpace(_bearer_token))
                    return new AddTorrentResponse() { Error = "Provide Valid Bearer Token" };

                if (!HelperMethods.IsMagnetLinkValid(magnet))
                    return new AddTorrentResponse() { Error = "Provide Valid Magnet Link" };

                return await _seedr.AddTorrent(_bearer_token, magnet);
            }
            catch (Exception ex)
            {
                return new AddTorrentResponse() { Error = ex.Message };
            }
        }

        [HttpGet]
        [Route("ListAll")]
        public async Task<ListContentResponse> ListAll(int folderId)
        {
            try
            {
                var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

                if (string.IsNullOrWhiteSpace(_bearer_token))
                    return new ListContentResponse() { Error = "Provide Valid Bearer Token" };

                return await _seedr.ListContent(_bearer_token, folderId);
            }
            catch (Exception ex)
            {
                return new ListContentResponse() { Error = ex.Message }; ;
            }
        }

        [HttpGet]
        [Route("GenerateLink")]
        public async Task<string> GenerateLink(string fileId)
        {
            try
            {
                var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

                if (string.IsNullOrWhiteSpace(_bearer_token))
                    return "Provide Bearer Token";

                return await _seedr.FetchFile(_bearer_token, fileId);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpGet]
        [Route("DeleteFile")]
        public async Task<CommonResponse> DeleteFile(string fileid)
        {
            try
            {
                var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

                if (string.IsNullOrWhiteSpace(_bearer_token))
                    return new CommonResponse() { Error = "Provide Bearer Token" };

                return await _seedr.DeleteFile(_bearer_token, fileid);
            }
            catch (Exception ex)
            {
                return new CommonResponse() { Error = ex.Message }; ;
            }
        }

        [HttpGet]
        [Route("DeleteFolder")]
        public async Task<CommonResponse> DeleteFolder(string folderId)
        {
            try
            {
                var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

                if (string.IsNullOrWhiteSpace(_bearer_token))
                    return new CommonResponse() { Error = "Provide Bearer Token" };

                return await _seedr.DeleteFolder(_bearer_token, folderId);
            }
            catch (Exception ex)
            {
                return new CommonResponse() { Error = ex.Message }; ;
            }
        }

        [HttpGet]
        [Route("DeleteTorrent")]
        public async Task<CommonResponse> DeleteTorrent(string torrentId)
        {
            try
            {
                var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

                if (string.IsNullOrWhiteSpace(_bearer_token))
                    return new CommonResponse() { Error = "Provide Bearer Token" };

                return await _seedr.DeleteTorrent(_bearer_token, torrentId);
            }
            catch (Exception ex)
            {
                return new CommonResponse() { Error = ex.Message }; ;
            }
        }

        [HttpGet]
        [Route("DeleteWishlist")]
        public async Task<CommonResponse> DeleteWishlist(string wishlistId)
        {
            try
            {
                var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

                if (string.IsNullOrWhiteSpace(_bearer_token))
                    return new CommonResponse() { Error = "Provide Bearer Token" };

                return await _seedr.DeleteWishList(_bearer_token, wishlistId);
            }
            catch (Exception ex)
            {
                return new CommonResponse() { Error = ex.Message }; ;
            }
        }
    }
}
