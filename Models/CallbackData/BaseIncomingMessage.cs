namespace Models.CallbackData
{
    public class BaseIncomingMessage
    {
        public string Event { get; set; }
        public long Timestamp { get; set; }
        public long Message_Token { get; set; }
        public string Type { get; set; }
        public string ContextInformation { get; set; }
        public User User { get; set; }
        public bool Subscribed { get; set; }
        public Sender Sender { get; set; }
        public Message Message { get; set; }

    }
}
