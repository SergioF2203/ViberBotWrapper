using System.Collections.Generic;

namespace Models
{
    public class WebHookModel
    {
        public string url { get; set; }
        public List<string> event_types { get; set; }
        public bool send_name { get; set; }
        public bool send_photo { get; set; }
    }
}
