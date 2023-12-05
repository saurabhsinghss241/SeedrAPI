using StreamTape.Models;

namespace StreamTape.Service.Interfaces
{
    public interface IStreamTape
    {
        Task<AddRUResponse> AddRemoteUpload(string login, string key, string url, string folder_id, string name);
        Task<string> RemoveRemoteUpload(string login, string key, string uploadId);
        Task<UploadInfo> CheckRemoteUploadProgress(string login, string key, string uploadId);
        Task<string> RenameFile(string login, string key, string linkid, string name);
    }
}
