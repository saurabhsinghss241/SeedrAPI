using Newtonsoft.Json;

namespace SeedrService.Models
{
    public class ProgressRUResponse
    {
        public int status { get; set; }
        public string msg { get; set; }
        [JsonProperty("result")]
        public dynamic result { get; set; }
    }
    public class Results
    {
        public UploadInfo uploadInfo { get; set; }
    }
    public class UploadInfo
    {
        public string id { get; set; }
        public string remoteurl { get; set; }
        public string status { get; set; }
        public int bytes_loaded { get; set; }
        public int bytes_total { get; set; }
        public string folderid { get; set; }
        public string added { get; set; }
        public string last_update { get; set; }
        public string linkid { get; set; }
        public string url { get; set; }
    }
}
