namespace Seedr.Models
{
    public class AddTorrentResponse
    {
        public bool Result { get; set; }
        public int Code { get; set; }
        public int User_torrent_id { get; set; }
        public string Title { get; set; }
        public string Torrent_hash { get; set; }
        public Wt Wt { get; set; }
        public string Error { get; set; }
    }
    public class Wt
    {
        public string Id { get; set; }
        public string User_id { get; set; }
        public string Title { get; set; }
        public string Size { get; set; }
        public string Torrent_hash { get; set; }
        public string Torrent_magnet { get; set; }
        public string Torrent_meta { get; set; }
        public string Created { get; set; }
        public string Added { get; set; }
        public string Is_private { get; set; }
    }
}
