namespace Models
{
    public class Message
    {
        public string Type { get; set; }
        public string Text { get; set; }
        public string Media { get; set; }
        public string Tracking_Data { get; set; }
        public Location Location { get; set; }
    }
}
