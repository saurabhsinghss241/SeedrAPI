namespace Seedr.Models
{
    public class ListContentResponse : Error
    {
        public long Space_max { get; set; }
        public int Space_used { get; set; }
        public int Saw_walkthrough { get; set; }
        public List<double> T { get; set; }
        public string Timestamp { get; set; }
        public int Folder_id { get; set; }
        public string Fullname { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public int Parent { get; set; }
        public List<int> Indexes { get; set; }
        public List<Torrent> Torrents { get; set; }
        public List<Folder> Folders { get; set; }
        public List<File> Files { get; set; }
    }
    public class Folder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Fullname { get; set; }
        public int Size { get; set; }
        public bool Play_audio { get; set; }
        public bool Play_video { get; set; }
        public bool Is_shared { get; set; }
        public string Last_update { get; set; }
    }
    public class File
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public string Hash { get; set; }
        public int Folder_id { get; set; }
        public int Folder_file_id { get; set; }
        public int File_id { get; set; }
        public string Last_update { get; set; }
        public bool Play_audio { get; set; }
        public bool Play_video { get; set; }
        public string Video_progress { get; set; }
        public int Is_lost { get; set; }
        public string Thumb { get; set; }
    }
    public class Torrent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Folder { get; set; }
        public int Size { get; set; }
        public string Hash { get; set; }
        public int Download_rate { get; set; }
        public int Torrent_quality { get; set; }
        public int Connected_to { get; set; }
        public int Downloading_from { get; set; }
        public int Uploading_to { get; set; }
        public int Seeders { get; set; }
        public int Leechers { get; set; }
        public string Warnings { get; set; }
        public int Stopped { get; set; }
        public string Progress { get; set; }
        public string Progress_url { get; set; }
        public string Last_update { get; set; }
    }

}
