using System;
using System.IO;
using System.Threading.Tasks;

namespace Utils
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

        public static string GetShorthandDateTime (DateTime dateTime)
        {
            var fulldate = dateTime.ToString();
            return fulldate.Substring(0, fulldate.IndexOf(' '));
        }
    }
}
