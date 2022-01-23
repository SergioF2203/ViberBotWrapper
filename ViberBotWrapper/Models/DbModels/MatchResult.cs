using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViberBotWebApp.Models.DbModels
{
    public class MatchResult
    {
        public string Id { get; set; }
        public string PlayerId { get; set; }
        public string OpponentName { get; set; }
        public long Timestamp { get; set; }
        public int Score { get; set; }
        public int OpScore { get; set; }
    }
}
