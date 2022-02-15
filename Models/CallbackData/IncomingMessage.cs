namespace Models.CallbackData
{
    public class IncomingMessage : BaseIncomingMessage
    {
        //public string Event { get; set; }
        //public long Timestamp { get; set; }
        //public long Message_Token { get; set; }
        public Sender Sender { get; set; }
        public Message Message { get; set; }
        public string Tracking_Data { get; set; }
    }
}