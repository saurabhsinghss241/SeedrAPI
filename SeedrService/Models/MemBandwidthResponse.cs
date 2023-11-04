namespace SeedrService.Models
{
    public class MemBandwidthResponse
    {
        public int bandwidth_used { get; set; }
        public long bandwidth_max { get; set; }
        public int space_used { get; set; }
        public long space_max { get; set; }
        public int is_premium { get; set; }
    }
}
