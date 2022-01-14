using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViberBotWebApp.Models
{
    public class WebHookModel
    {
        public string url { get; set; }
        public List<string> event_types { get; set; }
        public bool send_name { get; set; }
        public bool send_photo { get; set; }
    }
}
