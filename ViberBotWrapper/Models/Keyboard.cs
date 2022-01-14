using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViberBotWebApp.Models
{
    public class Keyboard
    {
        public string Type { get; set; }
        public bool DefaultHeight { get; set; }
        public List<Button> Buttons { get; set; }
    }
}
