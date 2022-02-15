namespace Models
{
    public class WelcomeMessage
    {
        public Sender Sender { get; set; }
        public string TrackingData { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }
        public string Media { get; set; }
        public string Thumbnail { get; set; }
    }
}
