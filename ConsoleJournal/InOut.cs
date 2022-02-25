using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleJournal 
{
    public class InOut 
    {
        public static void Write(string msg, ConsoleColor foreColor, ConsoleColor backColor = ConsoleColor.Black) {
            Console.ForegroundColor = foreColor;
            Console.BackgroundColor = backColor;
            Console.Write(msg);
            Console.ResetColor();
        }

        public static void Write(string msg, Location loc) {
            Console.SetCursorPosition(loc.Left, loc.Top);
            Console.Write(msg);
        }

        public static void Write(string msg, Location loc, ConsoleColor foreColor, ConsoleColor backColor = ConsoleColor.Black) {
            Console.SetCursorPosition(loc.Left, loc.Top);
            Write(msg, foreColor, backColor);
        }

        public static void ClearBehind() {
            int origLeft = Console.CursorLeft;
            for (int left = origLeft; left < Console.BufferWidth; left++) {
                Console.CursorLeft = left;
                Console.Write(" ");
            }
            Console.CursorLeft = origLeft;
        }

        public static void ClearLine(int top) {
            Console.SetCursorPosition(0, top);
            ClearBehind();
        }
    }
}
