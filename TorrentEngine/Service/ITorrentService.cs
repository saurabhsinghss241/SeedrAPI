using Seedr.Models;
using TorrentEngine.Models;

namespace TorrentEngine.Service
{
    public interface ITorrentService
    {
        Task<AddTorrentResponse> AddMagnet(AddMagnetRequest request);
        Task<ListContentResponse> ListAll(ListAllRequest request);
        Task<GenerateLinksResponse> GenerateLinks(GenerateLinksRequest request);
        Task<CommonResponse> DeleteFile(DeleteRequest request);
        Task<CommonResponse> DeleteFolder(DeleteRequest request);
        Task<CommonResponse> DeleteTorrent(DeleteRequest request);
        Task<CommonResponse> DeleteWishlist(DeleteRequest request);

    }
}
