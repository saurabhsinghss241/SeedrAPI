using Seedr.Models;

namespace TorrentEngine.Models
{
    public class GenerateLinksResponse : Error
    {
        public List<GenerateURL> Links { get; set; }
    }
}
