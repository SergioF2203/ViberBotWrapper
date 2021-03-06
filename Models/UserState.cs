using Models.Enums;

namespace Models
{
    public class UserState
    {
        public string OpponentName { get; set; } = string.Empty;
        public bool IsDebugMode { get; set; }
        public State UState { get; set; }
        public int Score { get; set; }
        public int OpScore { get; set; }
        public int CountMatches { get; set; } = 0;
        public string LastMatchId { get; set; }
    }
}
