using System;

namespace ConsoleJournal {
    class Settings 
    {
        public static string Username { get; set; } = "";
        public static ConsoleColor DocumentForeground { get; set; } = ConsoleColor.White;
        public static DateTime LastRefresh { get; set; } = DateTime.MinValue;
        public static string TodaysQuestion { get; set; } = "";
    }
}
