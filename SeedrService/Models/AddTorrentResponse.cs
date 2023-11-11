namespace SeedrService.Models
{
    public class AddTorrentResponse
    {
        public bool Result { get; set; }
        public int Code { get; set; }
        public int User_torrent_id { get; set; }
        public string Title { get; set; }
        public string Torrent_hash { get; set; }
        public string Error { get; set; }
    }
}
