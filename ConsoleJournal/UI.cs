using Figgle;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleJournal 
{
    public class UI
    {
        static public string ListSymbol { get; set; } = ">";
        static public ConsoleColor DocumentForeground { get; set; } = ConsoleColor.White;

        static void SetCursor(int left, int top) => Console.SetCursorPosition(left, top);
        private static int SelectIndex<T>(IEnumerable<T> options, int index = 0)
        {
            int n = options.Count();

            if (index < 0) index = n - 1;
            else if (index >= n) index = 0;

            Console.CursorVisible = false;
            int origLeft = Console.CursorLeft;

            for (int i = 0; i < n; i++) new Text($"{ListSymbol} {options.ElementAt(i)}", (origLeft, 0), -1, i == index).Display();

            ConsoleKey key = Console.ReadKey(true).Key;

            Console.SetCursorPosition(origLeft, Console.CursorTop - n);
            Console.CursorVisible = true;

            switch (key) 
            {
                case ConsoleKey.UpArrow:
                    return SelectIndex(options, --index);
                case ConsoleKey.DownArrow:
                    return SelectIndex(options, ++index);
                case ConsoleKey.Enter:
                    return index;
                default:
                    return SelectIndex(options, index);
            }
        }
        private static T SelectItem<T>(IEnumerable<T> options) => options.ElementAt(SelectIndex(options));

        public static Screen MainMenu(string greeting)
        {
            Console.Clear();

            var banner = FiggleFonts.Roman.Render("Journal");

            new Text(banner, Text.Align.Center, 4).Display();

            new Text(greeting, Text.Align.Center).Display();

            SetCursor(4, 21);
            string choice = UI.SelectItem(new string[] { "New document", "View documents", "Settings", "Exit" });

            switch (choice)
            {
                case "New document":
                    return Screen.New;
                case "View documents":
                    return Screen.Docs;
                case "Settings":
                    return Screen.Settings;
                default:
                case "Exit":
                    return Screen.None;
            }
        }

        public static Screen NewMenu()
        {
            return Screen.None;
        }

        public static Screen DocsMenu()
        {
            return Screen.None;
        }

        public static Screen SettingsMenu()
        {
            return Screen.None;
        }
    }
}
