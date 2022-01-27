using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViberBotWebApp.Models
{
    public class SendedMessage
    {
        public string receiver { get; set; }
        public int min_api_version { get; set; }
        public IEnumerable<string> broadcast_list { get; set; }
        public Sender sender { get; set; }
        public string type { get; set; }
        public string text { get; set; }
        //public string tracking_data { get; set; }
        public Keyboard? keyboard { get; set; }
    }
}
