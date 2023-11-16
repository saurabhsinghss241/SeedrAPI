using Microsoft.AspNetCore.Mvc;
using SeedrService.Models;
using SeedrService.Service;

namespace SeedrService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreamTapeController : ControllerBase
    {
        private readonly IStreamTape _streamTape;
        public StreamTapeController(IStreamTape streamTape)
        {
            _streamTape = streamTape;
        }

        [HttpGet]
        [Route("AddRemoteUpload")]
        public async Task<AddRUResponse> AddRemoteUpload(string login, string key, string url, string folderId, string name)
        {
            return await _streamTape.AddRemoteUpload(login, key, url, folderId, name);
        }

        [HttpGet]
        [Route("CheckProgressRemoteUpload")]
        public async Task<UploadInfo> CheckProgressRemoteUpload(string login, string key, string uploadId)
        {
            return await _streamTape.CheckRemoteUploadProgress(login, key, uploadId);
        }

        [HttpGet]
        [Route("RemoveRemoteUpload")]
        public async Task<string> RemoveRemoteUpload(string login, string key, string uploadId)
        {
            return await _streamTape.RemoveRemoteUpload(login, key, uploadId);
        }
    }
}
