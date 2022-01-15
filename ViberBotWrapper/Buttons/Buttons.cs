﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViberBotWebApp.Models;

namespace ViberBotWebApp.Buttons
{
    public class Buttons
    {
        public Button Today
        {
            get
            {
                return new()
                {
                    ActionType = "reply",
                    ActionBody = "daytoday",
                    Text = "<font color=\"#fefff6\">\uD83D\uDCC6 Today</font>",
                    TextSize = "regular",
                    Columns = 6,
                    BgColor = "#735ff2"
                };
            }
        }
        public Button MainMenu
        {
            get
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
        }

        public Button Match
        {
            get
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
        }

        public Button Statistics
        {
            get
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
        }

        public Button Rematch
        {
            get
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
        }

        public Button Finish
        {
            get
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
        }

        public Button Win
        {
            get
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
        }

        public Button Lose
        {
            get
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
        }

        public Button Result
        {
            get
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
        }

        public Button Zero
        {
            get
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
        }


        public Button One
        {
            get
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
        }

        public Button Two
        {
            get
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
        }

        public Button Three
        {
            get
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
        }

        public Button Four
        {
            get
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
        }

        public Button Five
        {
            get
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
        }

        public Button Six
        {
            get
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
        }

        public Button Seven
        {
            get
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
        }

        public Button Eight
        {
            get
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
        }

        public Button Nine
        {
            get
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

        public Button Ten
        {
            get
            {
                return new()
                {
                    ActionType = "reply",
                    ActionBody = "dieci",
                    Text = "<font color=\"#fefff6\">10</font>",
                    TextSize = "regular",
                    Columns = 3,
                    BgColor = "#735ff2"
                };
            }
        }


        public Button Eleven
        {
            get
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
        }


        public Button Yes
        {
            get
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
        }

        public Button No
        {
            get
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
        }

        public Button Perfomance
        {
            get
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
        }

        public Button PerfomanceDay
        {
            get
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
        }

        public Button PerfomanceWeek
        {
            get
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
        }

        public Button PerfomanceMonth
        {
            get
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
        }


        public Button WinRate
        {
            get
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
        }

        public Button WinRateMatch
        {
            get
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
        }

        public Button WinRateSeries
        {
            get
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
}
