namespace Seedr.Models
{
    public class ProgressResponse
    {
        public string title { get; set; }
        public float size { get; set; }
        public float download_rate { get; set; }
        public int torrent_quality { get; set; }
        public string warnings { get; set; }
        public Stats stats { get; set; }
        public int stopped { get; set; }
        public float progress { get; set; }
        public string hash { get; set; }
        public string folder_created { get; set; }
        public List<object> files_progress { get; set; }
    }
    public class Stats
    {
        public string torrent_hash { get; set; }
        public float progress { get; set; }
        public string title { get; set; }
        public int downloading_from { get; set; }
        public int uploading_to { get; set; }
        public string warnings { get; set; }
        public int stopped { get; set; }
        public int folder_created { get; set; }
        public float download_rate { get; set; }
        public float size { get; set; }
        public int torrent_quality { get; set; }
        public float seeders { get; set; }
        public float leechers { get; set; }
    }
}
