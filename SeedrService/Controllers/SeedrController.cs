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
        private readonly ISeedrLogin _seedrLogin;
        private readonly ISeedr _seedr;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContext;
        public SeedrController(ISeedrLogin seedrLogin,ISeedr seedr, IConfiguration configuration)
        {
            _seedrLogin = seedrLogin;
            _seedr = seedr;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("GetAccessToken")]
        public async Task<AuthResponse> GetAccessToken(AuthRequest request)
        {
            try
            {
                return await _seedrLogin.LoginUsingUsernamePassword(request.Username, request.Password);
            }
            catch (Exception ex)
            {
                return new AuthResponse() { Error = ex.Message};
            }
        }

        [HttpGet]
        [Route("AddMagnet")]
        public async Task<string> AddMagnet(string magnet)
        {
            try
            {
                var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

                if (string.IsNullOrWhiteSpace(_bearer_token))
                    return "Provide Bearer Token";

                if (!HelperMethods.IsMagnetLinkValid(magnet))
                    return "Provide Valid Magnet Link";

                return magnet;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpGet]
        [Route("RefreshToken")]
        public async Task<AuthResponse> RefreshToken(string refreshToken)
        {
            try
            {
                return await _seedrLogin.RefreshAccessToken(refreshToken);
            }
            catch (Exception ex)
            {
                return new AuthResponse() { Error = ex.Message };
            }
        }

        [HttpGet]
        [Route("DeleteFolder")]
        public string DeleteAsync(string id)
        {
            try
            {
                var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

                if (string.IsNullOrWhiteSpace(_bearer_token))
                    return "Provide Bearer Token";

                _seedr.DeleteFolder(_bearer_token,id);
                return "";
            }
            catch (Exception)
            {
                return "";
                //throw;
            }
        }
    }
}
