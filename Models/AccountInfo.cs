namespace Models
{
    public class AccountInfo
    {
        public int status { get; set; }
        public string status_message { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string uri { get; set; }
        public string icon { get; set; }
        public string background { get; set; }
        public string category { get; set; }
        public string subcategory { get; set; }
        public Location location { get; set; }
        public string country { get; set; }
        public string webHook { get; set; }
        public string[] event_types { get; set; }
        public int subscribers_count { get; set; }
        public Member[] members { get; set; }
    }
}
