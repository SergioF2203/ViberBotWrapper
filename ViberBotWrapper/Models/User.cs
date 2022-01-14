using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViberBotWebApp.Models
{
    public class User
    {
        public string id { get; set; }
        public string name { get; set; }
        public string avatar { get; set; }
        public string country { get; set; }
        public string language { get; set; }
        public string primary_device_os { get; set; }
        public int api_version { get; set; }
        public string viber_version { get; set; }
        public string mcc { get; set; }
        public string mnc { get; set; }
        public string device_type { get; set; }
        public bool subscribed { get; set; }

    }
}
