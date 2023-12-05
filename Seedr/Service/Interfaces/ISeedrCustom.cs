using Seedr.Models;

namespace Seedr.Service.Interfaces
{
    public interface ISeedrCustom
    {
        Task<GenerateURL> MagnetToDirectLink(string token, string magnet);
        Task<(bool, string, string)> IsTorrentProcessed(string token, int torrentId);
        Task<ProgressResponse> GetCurrentTorrentProgressStatus(string progressUrl);

    }
}
