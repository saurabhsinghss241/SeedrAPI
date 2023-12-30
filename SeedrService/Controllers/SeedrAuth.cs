using Microsoft.AspNetCore.Mvc;
using SeedrService.Models;
using Seedr.Service;

namespace SeedrService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedrAuth : ControllerBase
    {
        private readonly ISeedrLogin _seedrLogin;
        public SeedrAuth(ISeedrLogin seedrLogin)
        {
            _seedrLogin = seedrLogin;
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
                return new AuthResponse() { Error = ex.Message };
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
    }
}
