using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models.Enums;

namespace ViberBotWebApp.ActionsProvider
{
    public class StateManagerService
    {
        private Dictionary<string, UserState> UsersStates { get; set; } = new();

        public bool IsExistPlayer(string id)
        {
            if (UsersStates.ContainsKey(id))
                return true;

            return false;
        }

        public bool AddPlayer(string id)
        {
            if (!UsersStates.ContainsKey(id))
            {
                UsersStates.Add(id, new UserState());
                return true;
            }

            return false;
        }

        public bool RemovePlayer(string id)
        {
            if (UsersStates.ContainsKey(id))
            {
                UsersStates.Remove(id);
            }

            return false;
        }

        public bool SetPlayerState(string id, State state)
        {
            if (UsersStates.ContainsKey(id))
            {
                UsersStates[id].UState = state;
                return true;
            }

            return false;
        }

        public bool IncrementCounterMatch(string id)
        {
            if (UsersStates.ContainsKey(id))
            {
                UsersStates[id].Score++;
                return true;
            }

            return false;
        }

        public bool SetLastMatchId(string playerid, string matchid)
        {
            if (UsersStates.ContainsKey(playerid))
            {
                UsersStates[playerid].LastMatchId = matchid;
                return true;
            }

            return false;
        }

        public bool IsLastMatchId(string playerId)
        {
            if (UsersStates.ContainsKey(playerId))
                return !string.IsNullOrEmpty(UsersStates[playerId].LastMatchId);

            return false;
        }

        public string GetLastMatchId(string playerId)
        {
            if (UsersStates.ContainsKey(playerId))
            {
                return UsersStates[playerId].LastMatchId;
            }

            return string.Empty;
        }

        public bool ResetLastMatchId(string playerId) 
        {
            if (UsersStates.ContainsKey(playerId))
            {
                UsersStates[playerId].LastMatchId = string.Empty;
                return true;
            }
            return false;
        }

        public bool ResetCounterMatch(string id)
        {
            if (UsersStates.ContainsKey(id))
            {
                UsersStates[id].Score = 0;
                return true;
            }

            return false;
        } 

        public int GetCounterMatch(string id)
        {
            if (UsersStates.ContainsKey(id))
            {
                return UsersStates[id].Score;
            }

            return -1;
        }

        public bool SetOpponentName(string id, string oppenentName)
        {
            if (UsersStates.ContainsKey(id))
            {
                UsersStates[id].OpponentName = oppenentName;
                return true;
            }
            return false;
        }

        public State GetPlayerState(string id)
        {
            return UsersStates[id].UState;
        }

        public string GetOpponentName(string id)
        {
            return UsersStates[id].OpponentName;
        }

        public string GetPlayersWStatus()
        {
            var result = new StringBuilder();

            foreach(var item in UsersStates)
            {
                result.Append("id: ");
                result.Append(item.Key);
                result.Append("\n");

                result.Append("opponent: ");
                result.Append(item.Value.OpponentName);
                result.Append("\n");

                result.Append("state: ");
                result.Append(item.Value.UState);
                result.Append("\n");
                result.Append("-------");
                result.Append("\n");

            }

            return result.ToString();
        }

        public bool Clear()
        {
            UsersStates.Clear();

            if (UsersStates.Count == 0)
                return true;

            return false;
        }
    }
}
