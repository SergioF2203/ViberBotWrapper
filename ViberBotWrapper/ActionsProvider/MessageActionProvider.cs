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
        private readonly IHttpClientFactory _httpClientFactory;
        //private readonly IConfiguration _configuration;
        private readonly StateManagerService _stateManager;
        private readonly DatabaseController _dbController;

        public RequestProvider RequestProvider { get; set; }

        public MessageActionProvider(IHttpClientFactory httpClientFactory, IConfiguration configuration, StateManagerService stateManager)
        {
            _httpClientFactory = httpClientFactory;
            //_configuration = configuration;
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
                tracking_data = "trackingData",
                type = "text",
                text = "",
            };

            switch (data.Message.Text.ToLower())
            {
                case "custom_match":
                    if (!_stateManager.IsExistPlayer(data.Sender.id))
                    {
                        _stateManager.AddPlayer(data.Sender.id);
                    }

                    message.text = "Enter your oppenent name or get back: ";
                    _stateManager.ChangeState(data.Sender.id, Enums.State.OpponentName);


                    message.keyboard = new()
                    {
                        Type = "keyboard",
                        DefaultHeight = false,
                        Buttons = new()
                        {
                            buttons.MainMenu
                        }
                    };

                    break;

                case "statistics":
                    message.text = "What kind of statistics you want to know?";
                    message.keyboard = new()
                    {
                        Type = "keyboard",
                        DefaultHeight = false,
                        Buttons = new()
                        {
                            buttons.Perfomance,
                            buttons.WinRate
                        }
                    };

                    _stateManager.ChangeState(data.Sender.id, State.Statistics);

                    break;

                case "perfomancestatistics":
                    message.text = "What period is your choise?";
                    message.keyboard = new()
                    {
                        Type = "keyboard",
                        DefaultHeight = false,
                        Buttons = new()
                        {
                            buttons.PerfomanceDay,
                            buttons.PerfomanceWeek,
                            buttons.PerfomanceMonth
                        }
                    };
                    break;

                case "winratestatistics":
                    message.text = "What WinRate do you want to know?";
                    message.keyboard = new()
                    {
                        Type = "keyboard",
                        DefaultHeight = false,
                        Buttons = new()
                        {
                            buttons.WinRateMatch,
                            buttons.WinRateSeries
                        }
                    };
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
                        resultSaved = true;
                        await WriteResults(matchscore, opScore, data.Sender.id);
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
                        resultSaved = true;
                        await WriteResults(matchscore, opScore, data.Sender.id);
                    }
                    if (resultSaved)
                    {
                        _stateManager.ChangeState(data.Sender.id, State.MatchEnded);
                        message.text = "I have note your result";
                        message.keyboard = new()
                        {
                            Type = "keyboard",
                            DefaultHeight = false,
                            Buttons = new()
                            {
                                buttons.Rematch,
                                buttons.Finish
                            }
                        };
                    }
                    break;

                case "undici":
                    if (_stateManager.GetPlayerState(data.Sender.id) == State.InGame)
                    {
                        _stateManager.ChangeState(data.Sender.id, State.OpponentResult);
                        message.text = "What is opponent's result?";
                        message.keyboard = new()
                        {
                            Type = "keyboard",
                            DefaultHeight = false,
                            Buttons = new()
                            {
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
                                buttons.TenW
                            }
                        };
                    }
                    break;

                case "rematch_again":
                    message.text = $"Do you want to play with {_stateManager.GetOpponentName(data.Sender.id)} again?";
                    message.keyboard = new()
                    {
                        Type = "keyboard",
                        DefaultHeight = false,
                        Buttons = new()
                        {
                            buttons.Yes,
                            buttons.No
                        }
                    };

                    break;

                case "yes_yes_yes":
                    message.text = "Okay, I waiting the match result ...";
                    _stateManager.ChangeState(data.Sender.id, State.InGame);
                    message.keyboard = new()
                    {
                        Type = "keyboard",
                        DefaultHeight = false,
                        Buttons = new()
                        {
                            buttons.Result,
                        }
                    };
                    break;

                case "no_no_no":
                case "close_the_game":
                    _stateManager.ResetCounterMatch(data.Sender.id);
                    message.text = "see you next time!";
                    _stateManager.ChangeState(data.Sender.id, State.MatchEnded);
                    message.keyboard = new()
                    {
                        Type = "keyboard",
                        DefaultHeight = false,
                        Buttons = new()
                        {
                            buttons.Match,
                            buttons.Statistics
                        }
                    };
                    break;

                case "game_result":
                    message.text = "What is your result?";
                    message.keyboard = new()
                    {
                        Type = "keyboard",
                        DefaultHeight = false,
                        Buttons = new()
                        {
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
                            buttons.Eleven
                        }
                    };
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
                    message.keyboard = new()
                    {
                        Type = "keyboard",
                        DefaultHeight = false,
                        Buttons = new() { buttons.MainMenu }
                    };
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
                    if(!string.IsNullOrEmpty(results))
                    {
                        message.text = results;
                    }
                    break;

                case "getplayerperfomance":
                    var totalPerfomance = await _dbController.GetPerfomance(data.Sender.id);
                    if (!string.IsNullOrEmpty(totalPerfomance))
                    {
                        message.text = $"Your perfomance is {totalPerfomance}";
                    }
                    else
                    {
                        message.text = "I have no data :(";
                    }

                    break;

                case "getplayerperfomancetoday":
                    var todayPerfomance = await _dbController.GetPerfomanceToday(data.Sender.id);
                    break;

                case "getperfomanceday":
                    break;

                case var date when new Regex(@"^(?:0?[1-9]|1[0-2])[./-](?:[012]?[0-9]|3[01])[./-](?:[0-9]{2}){1,2}$").IsMatch(date):
                    message.text = $"Unfortunately I have no data for {date}";

                    var dayPerfomance = await _dbController.GetPerfomanceDay(data.Sender.id, DateTime.Parse(date));
                    if (!string.IsNullOrEmpty(dayPerfomance))
                    {
                        message.text = $"Your perfomance for {date} is {dayPerfomance}";
                    }
                    break;

                default:
                    if (_stateManager.GetPlayerState(data.Sender.id) == State.OpponentName)
                    {
                        message.text = "Deal!, I note your opponent's name";
                        _stateManager.SetOpponentName(data.Sender.id, data.Message.Text);
                        _stateManager.ChangeState(data.Sender.id, Enums.State.InGame);

                        message.keyboard = new()
                        {
                            Type = "keyboard",
                            DefaultHeight = false,
                            Buttons = new()
                            {
                                buttons.Result,
                            }
                        };

                    }
                    else
                    {
                        message.text = "I have grown up and know a lot, but not that, sorry ... :(";
                        message.keyboard = new()
                        {
                            Type = "keyboard",
                            DefaultHeight = false,
                            Buttons = new()
                            {
                                buttons.Match,
                                buttons.Statistics
                            }
                        };
                    }
                    break;
            }

            await RequestProvider.SendMessageRequest(message);
        }

        private async Task WriteResults(int playerScore, int opponentScore, string playerId)
        {
            await _dbController.SaveResultToDb(new()
            {
                PlayerId = playerId,
                OpponentName = _stateManager.GetOpponentName(playerId),
                Timestamp = HelperActions.GetUnixTimeStamp(DateTime.Now),
                Score = playerScore,
                OpScore = opponentScore
            });
        }
    }
}
