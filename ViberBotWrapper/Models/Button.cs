using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViberBotWebApp.Models
{
    public class Button
    {
        public string ActionType { get; set; }
        public string ActionBody { get; set; }
        public string Text { get; set; }
        public string TextSize { get; set; }
        public int? Columns { get; set; }
        public int? Rows { get; set; }
        public string? BgColor { get; set; }

    }
}
