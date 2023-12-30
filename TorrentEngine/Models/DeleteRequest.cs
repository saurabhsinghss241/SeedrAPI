namespace TorrentEngine.Models
{
    public class DeleteRequest : UserDetail
    {
        public string Id { get; set; }
        public IdType IdType { get; set; }

    }
    public enum IdType
    {
        Folder = 0,
        File = 1,
        Torrent = 2,
        Wishlist = 3,
    }
}
