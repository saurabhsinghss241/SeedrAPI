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
        public async Task<string> AddMagnet(string magnet)
        {
            try
            {
                var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

                if (string.IsNullOrWhiteSpace(_bearer_token))
                    return "Provide Valid Bearer Token";

                if (!HelperMethods.IsMagnetLinkValid(magnet))
                    return "Provide Valid Magnet Link";

                return await _seedr.AddTorrent(_bearer_token, magnet);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpGet]
        [Route("ListAll")]
        public async Task<string> ListAll(int folderId)
        {
            try
            {
                var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

                if (string.IsNullOrWhiteSpace(_bearer_token))
                    return "Provide Valid Bearer Token";

                return await _seedr.ListContent(_bearer_token, folderId);
            }
            catch (Exception ex)
            {
                return $"{ex.Message}";
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
        [Route("Delete")]
        public async Task<string> DeleteAsync(int id)
        {
            try
            {
                var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

                if (string.IsNullOrWhiteSpace(_bearer_token))
                    return "Provide Bearer Token";

                return await _seedr.Delete(_bearer_token, id);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
