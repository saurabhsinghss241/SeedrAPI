using SeedrService.Models;

namespace SeedrService.Service
{
    public interface ISeedr
    {
        Task<string> TestToken(string token);
        Task<string> GetSettings(string token);
        Task<MemBandwidthResponse> GetMemoryBandWidth(string token);
        Task<string> AddTorrent(string token,string magnet, string wishlistId = "", int folderId = -1);
        Task<string> ScanPage(string token, string url);
        Task<string> CreateArchive(string token,string folderId);
        Task<string> FetchFile(string token,string fileId);
        Task<string> ListContent(string token,int folderId = 0,string contentType = "folder");
        Task<string> RenameFile(string token,string fileId, string renameTo);
        Task<string> RenameFolder(string token,string folderId, string renameTo);
        Task<string> Delete(string token, int id);
        Task<string> DeleteFile(string token,string fileId);
        Task<string> DeleteFolder(string token,string folderId);
        Task<string> DeleteWishList(string token,string wishlistId);
        Task<string> DeleteTorrent(string token,string torrentId);
        Task<string> AddFolder(string token,string name);
        Task<string> SearchFile(string token,string query);
        Task<string> ChangeName(string token,string name,string password);
        Task<string> ChangePassword(string token,string oldPassword,string newPassword);
        Task<string> GetDevice(string token);



    }
}
