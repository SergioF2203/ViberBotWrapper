using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ViberBotWebApp.Helpers
{
    public static class HelperActions
    {
        public static async Task WriteToFile(string filename, string text)
        {
            using StreamWriter file = new(filename, append: true);
            await file.WriteLineAsync(text);
        }

        public static DateTime GetDate(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();

            return dateTime;
        }

        public static long GetUnixTimeStamp(DateTime date)
        {
            return new DateTimeOffset(date).ToUnixTimeSeconds();
        }
    }
}
