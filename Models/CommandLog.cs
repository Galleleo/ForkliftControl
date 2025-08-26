namespace ForkliftControl.Models
{
    public class CommandLog
    {
        public int Id { get; set; }
        public string Command { get; set; } = string.Empty;
        public int ForkliftId { get; set; }
        public Forklift? Forklift { get; set; }
        public DateTime Timestamp { get; set; }
    }
}