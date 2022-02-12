using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleJournal 
{
    public class Menu{

        public static string ListSymbol { get; set; } = ">";

        public static ConsoleColor HighlightColor { get; set; } = ConsoleColor.White;
        public static ConsoleColor ForegroundColor { get; set; } = ConsoleColor.White;

        public static int SelectIndex<T>(IEnumerable<T> options, int index = 0)
        {
            int n = options.Count();

            if (index < 0) index = n - 1;
            else if (index >= n) index = 0;

            Console.CursorVisible = false;
            for (int i = 0; i < n; i++) {
                if (i == index) InOut.Write($"{ListSymbol} {options.ElementAt(i)}\n", ConsoleColor.Black, HighlightColor);
                else InOut.Write($"{ListSymbol} {options.ElementAt(i)}\n",ForegroundColor);
            }

            ConsoleKey key = Console.ReadKey(true).Key;

            Console.SetCursorPosition(0, Console.CursorTop - n);
            Console.CursorVisible = true;

            switch (key) {
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

        public static T SelectItem<T>(IEnumerable<T> options) => options.ElementAt(SelectIndex(options));


    }
}
