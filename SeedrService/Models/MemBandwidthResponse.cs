namespace SeedrService.Models
{
    public class MemBandwidthResponse
    {
        public int Bandwidth_used { get; set; }
        public long Bandwidth_max { get; set; }
        public int Space_used { get; set; }
        public long Space_max { get; set; }
        public int Is_premium { get; set; }
    }
}
