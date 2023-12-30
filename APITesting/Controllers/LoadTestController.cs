using APITesting.Service;
using HashingService;
using Microsoft.AspNetCore.Mvc;

namespace APITesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoadTestController : ControllerBase
    {
        private readonly ILoadService _loadService;
        private readonly IHashingService _hashingService;
        public LoadTestController(ILoadService loadService, IHashingService hashingService)
        {
            _loadService = loadService;
            _hashingService = hashingService;
        }

        [HttpGet]
        public async Task<string> Get(int code)
        {
            try
            {
                return await _loadService.GetDataNew(code);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"{ex.Message}");
                return ex.Message;
            }

        }

        [HttpGet]
        [Route("Encrypt")]
        public string Encrypt(string key)
        {
            try
            {
                return _hashingService.Hash(key);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return ex.Message;
            }

        }
        [HttpGet]
        [Route("Decrypt")]
        public async Task<string> Decrypt(string key, string hash)
        {
            try
            {
                if (_hashingService.IsValidHash(key, hash))
                    return "Valid Hash";
                return "Invalid Hash";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return ex.Message;
            }

        }
    }
}
