namespace SeedrService.Models
{
    public class AddRUResponse
    {
        public int status { get; set; }
        public string msg { get; set; }
        public Result result { get; set; }
    }
    public class Result
    {
        public string id { get; set; }
        public string folderid { get; set; }
    }
}
