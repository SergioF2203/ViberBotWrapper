using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ViberBotWebApp.DAL;
using ViberBotWebApp.Enums;
using ViberBotWebApp.Helpers;
using ViberBotWebApp.Models;
using ViberBotWebApp.Models.CallbackData;
using static System.Net.Mime.MediaTypeNames;

namespace ViberBotWebApp.ActionsProvider
{
    public class MessageActionProvider
    {
        private readonly StateManagerService _stateManager;
        private readonly DatabaseController _dbController;

        public RequestProvider RequestProvider { get; set; }

        public MessageActionProvider(IHttpClientFactory httpClientFactory, IConfiguration configuration, StateManagerService stateManager)
        {
            _stateManager = stateManager;
            _dbController = new(configuration);

            RequestProvider = new(configuration, httpClientFactory);
        }

        public WelcomeMessage SetWelcomeMessage(string name)
        {
            WelcomeMessage w_message = new()
            {
                Sender = new()
                {
                    name = "PPBot",
                    avatar = "https://media-direct.cdn.viber.com/pg_download?pgtp=icons&dlid=0-04-01-9324f81f31813b3430b399241b48e4e4b1d0544d65401b8f4ac237e7a443df24&fltp=jpg&imsz=0000",
                },
                TrackingData = "my_tracking data",
                Type = "text",
                Text = $"{name}, Welcome to our Bot ... We're newborn and want to grow up with you ... :). Send any message to start conversation with me and ... enjoy the day! ... (snowman)",
            };

            return w_message;
        }
        public async Task SetWebHook()
        {
            WebHookModel webhookbody = new()
            {
                url = "https://chatapi.viber.com/pa/set_webhook",
                event_types = new()
                {
                    "delivered",
                    "seen",
                    "failed",
                    "subscribed",
                    "unsubscribed",
                    "conversation_started"
                },
                send_name = true,
                send_photo = true
            };

            await RequestProvider.SendWebHookSetRequest(webhookbody);
        }

        public async Task SetResponseMessage(BaseIncomingMessage data)
        {
            if (!_stateManager.IsExistPlayer(data.Sender.id))
            {
                _stateManager.AddPlayer(data.Sender.id);
            }

            Buttons.Buttons buttons = new();

            SendedMessage message = new()
            {
                receiver = data.Sender.id,
                min_api_version = 1,
                sender = new()
                {
                    name = "PPBot",
                    avatar = "https://media-direct.cdn.viber.com/pg_download?pgtp=icons&dlid=0-04-01-9324f81f31813b3430b399241b48e4e4b1d0544d65401b8f4ac237e7a443df24&fltp=jpg&imsz=0000"
                },
                type = "text",
                text = "",
            };

            var senderId = data.Sender.id;

            switch (data.Message.Text.ToLower())
            {
                case "no_no_no":
                case "custom_match":
                    if (!_stateManager.IsExistPlayer(data.Sender.id))
                    {
                        _stateManager.AddPlayer(data.Sender.id);
                    }

                    message.text = "Enter your opponent name of select bellow or get back:\n";
                    message.text += $"Your state is: {_stateManager.GetPlayerState(data.Sender.id)}";
                    _stateManager.SetPlayerState(data.Sender.id, Enums.State.OpponentName);

                    var opponentsName = _dbController.GetLastOpponentName(data.Sender.id).Result;
                    var buttonsList = new List<Button>();
                    foreach (var name in opponentsName)
                    {
                        var temp = Buttons.Buttons.CustomButton(name, 6);
                        buttonsList.Add(temp);
                    }
                    buttonsList.Add(buttons.MainMenu);
                    message.keyboard = new(buttonsList.ToArray());

                    break;

                case "statistics":
                    message.text = "What kind of statistics you want to know?";

                    message.keyboard = new(buttons.Perfomance, buttons.WinRate);

                    _stateManager.SetPlayerState(data.Sender.id, State.Statistics);

                    break;

                case "perfomancestatistics":
                    _stateManager.SetPlayerState(data.Sender.id, State.PerfomanceStatics);
                    message.text = "For the ... ?";

                    message.keyboard = new(buttons.PerfomanceOpponent, buttons.PerfomancePeriod);
                    break;

                case "perfomanceopponentstatistics":
                    _stateManager.SetPlayerState(data.Sender.id, State.PerfomanceOpponentName);
                    message.text = "Enter opponent name";
                    break;

                case "perfomanceperiodtstatistics":
                case "winratestatisticsperiod":
                    message.text = "For the period?";

                    //message.keyboard = new(buttons.Today, buttons.AllPeriod, buttons.Day, buttons.Week, buttons.Month);
                    message.keyboard = new(buttons.PeriodKeyboard.ToArray());
                    break;

                case "getpdaystatistics":
                    message.text = "What the day your choise?";

                    message.keyboard = new(buttons.Today, buttons.TheDay);

                    break;

                case "getcustomdaystatistics":
                    var winrwteOrPerfomanceText = "";
                    if (_stateManager.GetPlayerState(senderId) == State.WinrateStatistics)
                    {
                        _stateManager.SetPlayerState(senderId, State.WinrateDay);
                        winrwteOrPerfomanceText = "*WinRate*";
                    }
                    else if (_stateManager.GetPlayerState(senderId) == State.PerfomanceStatics)
                    {
                        _stateManager.SetPlayerState(senderId, State.PerfomanceDay);
                        winrwteOrPerfomanceText = "*Performance*";
                    }
                    message.text = $"Please enter the date (mm/dd/yyyy pattern) you want to know your {winrwteOrPerfomanceText}";

                    message.keyboard = new(buttons.MainMenu);

                    break;

                case "winratestatistics":
                    _stateManager.SetPlayerState(data.Sender.id, State.WinrateStatistics);
                    message.text = "What WinRate do you want to know?";

                    message.keyboard = new(buttons.WinRateMatch, buttons.WinRateSeries);
                    break;

                case "winratestatisticsmatch":
                    message.text = "For the ... ?";

                    message.keyboard = new(buttons.WinRateOpponent, buttons.WinRatePeriod);
                    break;

                case "user_added":
                    message.text = "congrats! New User added :)";
                    break;

                case "zeroitalian":
                case "uno":
                case "due":
                case "tre":
                case "quattro":
                case "cinque":
                case "sei":
                case "sette":
                case "otto":
                case "nove":
                case "dieci":
                    _stateManager.IncrementCounterMatch(data.Sender.id);
                    bool resultSaved = false;
                    var matchId = Guid.NewGuid().ToString();
                    _stateManager.SetLastMatchId(data.Sender.id, matchId);
                    if (_stateManager.GetPlayerState(data.Sender.id) == State.InGame)
                    {
                        int matchscore = 0;
                        int opScore = 11;
                        foreach (var item in Enum.GetNames(typeof(Score)))
                        {
                            if (item == data.Message.Text)
                            {
                                matchscore = (int)Enum.Parse(typeof(Score), item);
                                break;
                            }
                        }


                        if (await WriteResults(matchscore, opScore, data.Sender.id, matchId))
                            resultSaved = true;
                        else
                            resultSaved = false;

                    }
                    else if (_stateManager.GetPlayerState(data.Sender.id) == State.OpponentResult)
                    {
                        int matchscore = 11;
                        int opScore = 0;
                        foreach (var item in Enum.GetNames(typeof(Score)))
                        {
                            if (item == data.Message.Text)
                            {
                                opScore = (int)Enum.Parse(typeof(Score), item);
                                break;
                            }
                        }

                        if (await WriteResults(matchscore, opScore, data.Sender.id, matchId))
                            resultSaved = true;
                        else
                            resultSaved = false;

                    }
                    if (resultSaved)
                    {
                        _stateManager.SetPlayerState(data.Sender.id, State.MatchEnded);
                        message.text = "I have note your result";
                        message.keyboard = new()
                        {
                            Type = "keyboard",
                            DefaultHeight = false,
                            Buttons = new()
                            {
                                buttons.Rematch,
                                buttons.Finish,
                                buttons.EditLastMatch
                            }
                        };
                    }
                    else
                    {
                        _stateManager.SetPlayerState(data.Sender.id, State.MatchEnded);
                        message.text = "Something went wrong while I've saved your reslut :(";

                        message.keyboard = new(buttons.Match, buttons.Statistics);
                    }
                    break;

                case "undici":
                    if (_stateManager.GetPlayerState(data.Sender.id) == State.InGame)
                    {
                        _stateManager.SetPlayerState(data.Sender.id, State.OpponentResult);
                        message.text = "What is opponent's result?";

                        message.keyboard = new(
                            buttons.Zero,
                            buttons.One,
                            buttons.Two,
                            buttons.Three,
                            buttons.Four,
                            buttons.Five,
                            buttons.Six,
                            buttons.Seven,
                            buttons.Eight,
                            buttons.Nine,
                            buttons.TenW);
                    }
                    break;

                case "rematch_again":
                    message.text = $"Played games: {_stateManager.GetCounterMatch(data.Sender.id)}\n" +
                        $"Do you want to play with {_stateManager.GetOpponentName(data.Sender.id)} again?";

                    message.keyboard = new(buttons.Yes, buttons.No);

                    break;

                case "editlastmatchresult":
                    if (_stateManager.IsLastMatchId(data.Sender.id))
                    {
                        var rowaffected = 0;

                        if (!string.IsNullOrEmpty(_stateManager.GetLastMatchId(data.Sender.id)))
                            rowaffected = await _dbController.RemoveEntryById(_stateManager.GetLastMatchId(data.Sender.id));

                        if (rowaffected is not 0 && _stateManager.ResetLastMatchId(data.Sender.id))
                            message.text = "I've removed your last result and you can enter new match result ... (like)";
                        else
                            message.text = "Something went wrong. (angrymark) Please to address to the admin ...";


                        message.keyboard = new(buttons.Match, buttons.Statistics);

                    }
                    break;

                case "yes_yes_yes":
                    message.text = "Okay, I waiting the match result ...";
                    _stateManager.SetPlayerState(data.Sender.id, State.InGame);

                    message.keyboard = new(
                                buttons.Zero,
                                buttons.One,
                                buttons.Two,
                                buttons.Three,
                                buttons.Four,
                                buttons.Five,
                                buttons.Six,
                                buttons.Seven,
                                buttons.Eight,
                                buttons.Nine,
                                buttons.Ten,
                                buttons.Eleven);

                    break;


                case "close_the_game":
                    _stateManager.ResetCounterMatch(data.Sender.id);
                    message.text = "see you next time!";
                    _stateManager.SetPlayerState(data.Sender.id, State.MatchEnded);

                    message.keyboard = new(buttons.Match, buttons.Statistics);

                    break;

                case "game_result":
                    message.text = "What is your result?";

                    message.keyboard = new(
                            buttons.Zero,
                            buttons.One,
                            buttons.Two,
                            buttons.Three,
                            buttons.Four,
                            buttons.Five,
                            buttons.Six,
                            buttons.Seven,
                            buttons.Eight,
                            buttons.Nine,
                            buttons.Ten,
                            buttons.Eleven);

                    break;

                case "#resetmystate":
                    _stateManager.SetPlayerState(data.Sender.id, State.StandBy);
                    break;

                case "#getallplayerstatus":
                    var accInfo = await RequestProvider.GetAccountInfo();
                    foreach (var member in accInfo.members)
                    {
                        if (member.role == "admin" && member.id == data.Sender.id)
                        {
                            message.text = _stateManager.GetPlayersWStatus();
                            break;
                        }
                    }

                    message.keyboard = new(buttons.MainMenu);
                    break;

                case "#getplayerscount":
                    var accinfo = await RequestProvider.GetAccountInfo();
                    foreach (var member in accinfo.members)
                    {
                        if (member.role == "admin" && member.id == data.Sender.id)
                        {
                            message.text = $"there're {await _dbController.GetUsersCount()} of us already ...";
                        }
                    }

                    break;

                case "getresults":
                    var results = await _dbController.GetResults(data.Sender.id);
                    if (!string.IsNullOrEmpty(results))
                    {
                        message.text = results;
                    }
                    break;

                case "broadcastcustommessage":
                    var userIds = await _dbController.GetUserIds();

                    message.broadcast_list = userIds;

                    message.text = data.Message.Tracking_Data;

                    message.keyboard = new(buttons.MainMenu);

                    break;

                case "getplayerperfomance":
                    var totalPerfomance = await _dbController.GetPerfomance(data.Sender.id);
                    if (!string.IsNullOrEmpty(totalPerfomance))
                    {
                        message.text = $"Your performance is {totalPerfomance}";
                    }
                    else
                    {
                        message.text = "I have no data :(";
                    }

                    break;

                case "getplayerperstatisticstoday":
                    var today = DateTime.Now.ToString().Substring(0, DateTime.Now.ToString().IndexOf(' '));
                    message.text = $"Unfortunately I have no data for {today}";

                    var state = _stateManager.GetPlayerState(data.Sender.id);


                    switch (state.ToString())
                    {
                        case "WinrateStatistics":
                            var todayWinrate = await _dbController.GetWinRateUser(data.Sender.id, DateTime.Now);
                            var totalWinrate = await _dbController.GetWinRateUser(data.Sender.id, DateTime.Parse("1/1/2001"));
                            var wrlenght = todayWinrate.IndexOf('.');
                            if (todayWinrate.Length == 3 && wrlenght == -1)
                                wrlenght = 0;
                            var wrTotalLenght = totalWinrate.IndexOf('.');
                            if (!string.IsNullOrEmpty(todayWinrate))
                            {
                                var winrate_percent = todayWinrate.Substring(0, wrlenght + 3);
                                var totalwinrate_percent = totalWinrate.Substring(0, wrTotalLenght + 3);
                                message.text = $"Today your winrate is *{winrate_percent}%*\nAnd your winrate for all period is *{totalwinrate_percent}%*";
                            }
                            break;
                        case "PerfomanceStatics":
                            // TODO: refactor GetPerfomanceToday to GetPerfomance with date as parameter

                            var todayPerfomance = await _dbController.GetPerfomanceToday(data.Sender.id);
                            var allPeriodPerfomance = await _dbController.GetPerfomance(data.Sender.id);
                            var prlenght = todayPerfomance.IndexOf('.');
                            if (todayPerfomance.Length == 3 && prlenght == -1)
                                prlenght = 0;
                            var allPeriodPerfLenght = allPeriodPerfomance.IndexOf('.');
                            if (!string.IsNullOrEmpty(todayPerfomance))
                            {
                                var perfomance_percent = todayPerfomance.Substring(0, prlenght + 3);
                                var allperfomance_percent = allPeriodPerfomance.Substring(0, allPeriodPerfLenght + 3);
                                message.text = $"Your performance for today is *{perfomance_percent}%*\nAnd your perfomance for all period is *{allperfomance_percent}%*";
                            }
                            break;
                        case "OpponentPerfomanceStatistics":
                            var nameOpp = _stateManager.GetOpponentName(data.Sender.id);
                            todayPerfomance = await _dbController.GetOpponentPerfomanceToday(nameOpp);
                            if (!string.IsNullOrEmpty(todayPerfomance))
                            {
                                var perfomance_percent = todayPerfomance.Substring(0, todayPerfomance.IndexOf('.') + 3);
                                message.text = $"{nameOpp}'s performance for today is {perfomance_percent}%";
                            }
                            break;
                    }

                    message.keyboard = new(buttons.MainMenu, buttons.Statistics);

                    _stateManager.SetPlayerState(data.Sender.id, State.Unstate);

                    break;

                case "getperfomanceday":
                    break;

                case "getplayerperstatisticsallperiod":
                    if (_stateManager.GetPlayerState(senderId) == State.WinrateStatistics)
                    {
                        var winrate = await _dbController.GetWinRateUser(senderId, DateTime.Parse("1/1/2001"));
                        var winratePercent = winrate.Substring(0, winrate.IndexOf('.') + 3);
                        message.text = $"Your Match *WinRate* for all period is {winratePercent}%";

                        message.keyboard = new(buttons.MainMenu, buttons.Statistics);

                    }
                    else if (_stateManager.GetPlayerState(senderId) == State.PerfomanceStatics)
                    {
                        var perfomance = await _dbController.GetPerfomance(senderId);
                        var perfomancePercent = perfomance.Substring(0, perfomance.IndexOf('.') + 3);
                        message.text = $"Your *performance* for all period is {perfomancePercent}%";

                        message.keyboard = new(buttons.MainMenu, buttons.Statistics);
                    }



                    break;

                case "winratestatisticsmatchtoday":
                    var winratetoday = await _dbController.GetWinRateUser(data.Sender.id, DateTime.Now);
                    var winratePercentToday = winratetoday.Substring(0, winratetoday.IndexOf('.') + 3);
                    message.text = $"Today, Your Match WinRate is {winratePercentToday}%";

                    message.keyboard = new(buttons.MainMenu);


                    break;

                case var date when DateTime.TryParse(date, out DateTime currentDate):
                    var text = $"Unfortunately I have no data for {date}";
                    var shandDate = HelperActions.GetShorthandDateTime(currentDate);

                    if (_stateManager.GetPlayerState(senderId) == State.WinrateDay)
                    {
                        var winrateResult = await _dbController.GetWinRateUser(senderId, currentDate);
                        if (!string.IsNullOrWhiteSpace(winrateResult))
                        {
                            var winRateResultPercent = winrateResult.Substring(0, winrateResult.IndexOf('.') + 3);

                            text = $"Your *WinRate* for {shandDate} is {winRateResultPercent}%";
                        }
                    }
                    else if (_stateManager.GetPlayerState(senderId) == State.PerfomanceDay)
                    {
                        var dayPerfomance = await _dbController.GetPerfomanceDay(data.Sender.id, currentDate);
                        if (!string.IsNullOrEmpty(dayPerfomance))
                        {
                            var perfomaneResultPercent = dayPerfomance.Substring(0, dayPerfomance.IndexOf('.') + 3);

                            text = $"Your *Performance* for {shandDate} is {perfomaneResultPercent}%";
                        }
                    }


                    message.text = text;
                    message.keyboard = new(buttons.MainMenu);


                    break;

                default:
                    if (_stateManager.GetPlayerState(data.Sender.id) == State.OpponentName)
                    {
                        message.text = "Deal! I note your opponent's name and waiting your result ...";
                        _stateManager.SetOpponentName(data.Sender.id, data.Message.Text);
                        _stateManager.SetPlayerState(data.Sender.id, State.InGame);

                        message.keyboard = new(
                            buttons.Zero,
                            buttons.One,
                            buttons.Two,
                            buttons.Three,
                            buttons.Four,
                            buttons.Five,
                            buttons.Six,
                            buttons.Seven,
                            buttons.Eight,
                            buttons.Nine,
                            buttons.Ten,
                            buttons.Eleven);


                    }
                    else if (_stateManager.GetPlayerState(data.Sender.id) == State.PerfomanceOpponentName)
                    {
                        _stateManager.SetOpponentName(data.Sender.id, data.Message.Text);
                        _stateManager.SetPlayerState(data.Sender.id, State.OpponentPerfomanceStatistics);

                        message.text = "Deal! I note your opponent's name. Choose a period.";
                        message.keyboard = new()
                        {
                            Type = "keyboard",
                            DefaultHeight = false,
                            Buttons = new()
                            {
                                buttons.Today,
                                buttons.AllPeriod,
                                buttons.Day,
                                buttons.Week,
                                buttons.Month
                            }
                        };
                    }
                    else
                    {
                        message.text = "I have grown up and know a lot, but not that, sorry ... :(";

                        message.keyboard = new(buttons.Match, buttons.Statistics);
                    }
                    break;
            }

            await RequestProvider.SendMessageRequest(message);
        }

        private async Task<bool> WriteResults(int playerScore, int opponentScore, string playerId, string matchId)
        {
            if (await _dbController.SaveResultToDb(new()
            {
                Id = matchId,
                PlayerId = playerId,
                OpponentName = _stateManager.GetOpponentName(playerId),
                Timestamp = HelperActions.GetUnixTimeStamp(DateTime.Now),
                Score = playerScore,
                OpScore = opponentScore
            }) is not 0)
            {
                return true;
            }

            return false;
        }
    }
}
