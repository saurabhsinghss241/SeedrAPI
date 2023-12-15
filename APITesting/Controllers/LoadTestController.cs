using APITesting.Service;
using Microsoft.AspNetCore.Mvc;

namespace APITesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoadTestController : ControllerBase
    {
        private readonly ILoadService _loadService;
        public LoadTestController(ILoadService loadService)
        {
            _loadService = loadService;
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
        [Route("Cache")]
        public async Task<string> GetCachedEntry(int code)
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
    }
}
