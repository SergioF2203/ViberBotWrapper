using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViberBotWebApp.Enums;

namespace ViberBotWebApp.Models
{
    public class UserState
    {
        public string OpponentName { get; set; } = string.Empty;
        public State UState { get; set; }
        public int Score { get; set; }
        public int OpScore { get; set; }
    }
}
