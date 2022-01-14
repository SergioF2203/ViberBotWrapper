using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViberBotWebApp.Models;

namespace ViberBotWebApp.Buttons
{
    public static class Buttons
    {
        public static Button MainMenu()
        {
            return new()
            {
                ActionType = "reply",
                ActionBody = "close_the_game",
                Text = "<font color=\"#fefff6\">\uD83D\uDC3E Main Menu</font>",
                TextSize = "regular",
                Columns = 6,
                BgColor = "#735ff2"
            };
        }

        public static Button Match()
        {
            return new()
            {
                ActionType = "reply",
                ActionBody = "custom_match",
                Text = "<font color=\"#fefff6\">\uD83C\uDFD3 Match</font>",
                TextSize = "regular",
                Columns = 3,
                BgColor = "#735ff2"
            };
        }

        public static Button Statistics()
        {
            return new()
            {
                ActionType = "reply",
                ActionBody = "Statistics",
                Text = "<font color=\"#fefff6\">\uD83D\uDCC8 Statistics</font>",
                TextSize = "regular",
                Columns = 3,
                BgColor = "#735ff2"
            };
        }

        public static Button Rematch()
        {
            return new()
            {
                ActionType = "reply",
                ActionBody = "rematch_again",
                Text = "<font color=\"#fefff6\">\uD83D\uDD01 ReMatch</font>",
                TextSize = "regular",
                Columns = 3,
                BgColor = "#735ff2"
            };
        }

        public static Button Finish()
        {
            return new()
            {
                ActionType = "reply",
                ActionBody = "close_the_game",
                Text = "<font color=\"#fefff6\">\uD83C\uDFC1 Finish</font>",
                TextSize = "regular",
                Columns = 3,
                BgColor = "#735ff2"
            };
        }

        public static Button Win()
        {
            return new()
            {
                ActionType = "reply",
                ActionBody = "winwin",
                Text = "<font color=\"#fefff6\">&#x1F3c6 WIN</font>",
                TextSize = "regular",
                Columns = 3,
                BgColor = "#149110"
            };
        }

        public static Button Lose()
        {
            return new()
            {
                ActionType = "reply",
                ActionBody = "loselose",
                Text = "<font color=\"#fefff6\">&#x1f61f LOSE</font>",
                TextSize = "regular",
                Columns = 3,
                BgColor = "#a3290d"
            };
        }

        public static Button Result()
        {
            return new()
            {
                ActionType = "reply",
                ActionBody = "game_result",
                Text = "<font color=\"#fefff6\">\uD83D\uDCCB Match Result</font>",
                TextSize = "regular",
                Columns = 6,
                BgColor = "#735ff2"
            };
        }

        public static Button Zero()
        {
            return new()
            {
                ActionType = "reply",
                ActionBody = "zeroitalian",
                Text = "<font color=\"#fefff6\">\u274C 0</font>",
                TextSize = "regular",
                Columns = 6,
                BgColor = "#735ff2"

            };
        }


        public static Button One()
        {
            return new()
            {
                ActionType = "reply",
                ActionBody = "uno",
                Text = "<font color=\"#fefff6\">1</font>",
                TextSize = "regular",
                Columns = 2,
                BgColor = "#735ff2"

            };
        }

        public static Button Two()
        {
            return new()
            {
                ActionType = "reply",
                ActionBody = "due",
                Text = "<font color=\"#fefff6\">2</font>",
                TextSize = "regular",
                Columns = 2,
                BgColor = "#735ff2"

            };
        }

        public static Button Three()
        {
            return new()
            {
                ActionType = "reply",
                ActionBody = "tre",
                Text = "<font color=\"#fefff6\">3</font>",
                TextSize = "regular",
                Columns = 2,
                BgColor = "#735ff2"
            };
        }

        public static Button Four()
        {
            return new()
            {
                ActionType = "reply",
                ActionBody = "quattro",
                Text = "<font color=\"#fefff6\">4</font>",
                TextSize = "regular",
                Columns = 2,
                BgColor = "#735ff2"
            };
        }

        public static Button Five()
        {
            return new()
            {
                ActionType = "reply",
                ActionBody = "cinque",
                Text = "<font color=\"#fefff6\">5</font>",
                TextSize = "regular",
                Columns = 2,
                BgColor = "#735ff2"
            };
        }

        public static Button Six()
        {
            return new()
            {
                ActionType = "reply",
                ActionBody = "sei",
                Text = "<font color=\"#fefff6\">6</font>",
                TextSize = "regular",
                Columns = 2,
                BgColor = "#735ff2"
            };
        }

        public static Button Seven()
        {
            return new()
            {
                ActionType = "reply",
                ActionBody = "sette",
                Text = "<font color=\"#fefff6\">7</font>",
                TextSize = "regular",
                Columns = 2,
                BgColor = "#735ff2"
            };
        }

        public static Button Eight()
        {
            return new()
            {
                ActionType = "reply",
                ActionBody = "otto",
                Text = "<font color=\"#fefff6\">8</font>",
                TextSize = "regular",
                Columns = 2,
                BgColor = "#735ff2"
            };
        }

        public static Button Nine()
        {
            return new()
            {
                ActionType = "reply",
                ActionBody = "nove",
                Text = "<font color=\"#fefff6\">9</font>",
                TextSize = "regular",
                Columns = 2,
                BgColor = "#735ff2"
            };
        }

        //public static Button TenW()
        //{
        //    return new()
        //    {
        //        ActionType = "reply",
        //        ActionBody = "dieci",
        //        Text = "<font color=\"#fefff6\">10</font>",
        //        TextSize = "regular",
        //        Columns = 3,
        //        BgColor = "#735ff2"
        //    };
        //}

        public static Button Ten()
        {
            return new()
            {
                ActionType = "reply",
                ActionBody = "dieci",
                Text = "<font color=\"#fefff6\">10</font>",
                TextSize = "regular",
                Columns = 6,
                BgColor = "#735ff2"
            };
        }


        public static Button Eleven()
        {
            return new()
            {
                ActionType = "reply",
                ActionBody = "undici",
                Text = "<font color=\"#fefff6\">\uD83C\uDFC6 11</font>",
                TextSize = "regular",
                Columns = 3,
                BgColor = "#735ff2"
            };
        }


        public static Button Yes()
        {
            return new()
            {
                ActionType = "reply",
                ActionBody = "yes_yes_yes",
                Text = "<font color=\"#fefff6\">\u2714 Yes</font>",
                TextSize = "regular",
                Columns = 3,
                BgColor = "#735ff2"
            };
        }

        public static Button No()
        {
            return new()
            {
                ActionType = "reply",
                ActionBody = "no_no_no",
                Text = "<font color=\"#fefff6\">\u274c No</font>",
                TextSize = "regular",
                Columns = 3,
                BgColor = "#735ff2"
            };
        }

        public static Button Perfomance()
        {
            return new()
            {
                ActionType = "reply",
                ActionBody = "perfomancestatistics",
                Text = "<font color=\"#fefff6\">\uD83C\uDFAF Perfomance</font>",
                TextSize = "regular",
                Columns = 3,
                BgColor = "#735ff2"
            };
        }

        public static Button PerfomanceDay()
        {
            return new()
            {
                ActionType = "reply",
                ActionBody = "perfomancedaystatistics",
                Text = "<font color=\"#fefff6\">Day</font>",
                TextSize = "regular",
                Columns = 2,
                BgColor = "#735ff2"
            };
        }
        public static Button PerfomanceWeek()
        {
            return new()
            {
                ActionType = "reply",
                ActionBody = "perfomanceweekstatistics",
                Text = "<font color=\"#fefff6\">Week</font>",
                TextSize = "regular",
                Columns = 2,
                BgColor = "#735ff2"
            };
        }

        public static Button PerfomanceMonth()
        {
            return new()
            {
                ActionType = "reply",
                ActionBody = "perfomancemonthstatistics",
                Text = "<font color=\"#fefff6\">Month</font>",
                TextSize = "regular",
                Columns = 2,
                BgColor = "#735ff2"
            };
        }


        public static Button WinRate()
        {
            return new()
            {
                ActionType = "reply",
                ActionBody = "winratestatistics",
                Text = "<font color=\"#fefff6\">\uD83D\uDCCA Win Rate</font>",
                TextSize = "regular",
                Columns = 3,
                BgColor = "#735ff2"
            };
        }

        public static Button WinRateMatch()
        {
            return new()
            {
                ActionType = "reply",
                ActionBody = "winratestatisticsmatch",
                Text = "<font color=\"#fefff6\"> Match Win Rate</font>",
                TextSize = "regular",
                Columns = 3,
                BgColor = "#735ff2"
            };
        }

        public static Button WinRateSeries()
        {
            return new()
            {
                ActionType = "reply",
                ActionBody = "winratestatisticsseries",
                Text = "<font color=\"#fefff6\"> Series Win Rate</font>",
                TextSize = "regular",
                Columns = 3,
                BgColor = "#735ff2"
            };
        }
    }
}
