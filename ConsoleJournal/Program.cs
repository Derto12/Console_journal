using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleJournal
{
    class Program
    {
        private static List<List<string>> Documents = new List<List<string>>();

        private static void ClearLine(int top)
        {
            Console.CursorTop = top;
            for (int left = 0; left < Console.BufferWidth; left++)
            {
                Console.CursorLeft = left;
                Console.Write(" ");
            }
        }

        private static int Select(IEnumerable<string> options, int index = 0)
        {
            int n = options.Count();

            if (index < 0) index = n - 1;
            else if (index >= n) index = 0;

            Console.CursorVisible = false;
            for (int i = 0; i < n; i++)
            {
                if(i == index)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine($"> {options.ElementAt(i)}");
                    Console.ResetColor();
                }
                else Console.WriteLine($"> {options.ElementAt(i)}");
            }

            ConsoleKey key = Console.ReadKey(true).Key;

            Console.SetCursorPosition(0, Console.CursorTop - n);
            Console.CursorVisible = true;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    return Select(options, --index);
                case ConsoleKey.DownArrow:
                    return Select(options, ++index);
                case ConsoleKey.Enter:
                    return index;
                default:
                    return Select(options, index);
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Let's just write sth here");
            Select(new string[] { "hey", "yoo", "jdsklafjdflaksdjf" });
        }
    }
}
