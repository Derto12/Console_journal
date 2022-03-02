using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleJournal 
{
    public class InOut 
    {
        public enum TextAlign
        {
            Left, Right, Center
        }

        public static void Write(string msg, ConsoleColor foreColor, ConsoleColor backColor = ConsoleColor.Black)
        {
            Console.ForegroundColor = foreColor;
            Console.BackgroundColor = backColor;
            Console.Write(msg);
            Console.ResetColor();
        }

        public static void Write(string msg, Location loc) 
        {
            Console.SetCursorPosition(loc.Left, loc.Top);
            Console.Write(msg);
        }

        public static void Write(string msg, Location loc, ConsoleColor foreColor, ConsoleColor backColor = ConsoleColor.Black) 
        {
            Console.SetCursorPosition(loc.Left, loc.Top);
            Write(msg, foreColor, backColor);
        }

        public static string PadBoth(string source, int totalWidth)
        {
            int spaces = totalWidth - source.Length;
            int padLeft = spaces / 2 + source.Length;
            return source.PadLeft(padLeft).PadRight(totalWidth);

        }

        public static List<string> AlignLines(IEnumerable<string> lines, int totalWidth, TextAlign align = TextAlign.Left) 
        {
            List<string> list = new List<string>();

            int i = 0;
            string currentline = lines.ElementAt(i);
            string remains = "";

            while (i < lines.Count() || remains != "")
            {
                if (currentline == "") currentline = lines.ElementAt(i);

                if (currentline.Length > totalWidth)
                {
                    if (currentline.Contains(' '))
                    {
                        string cutline = CutText(currentline, totalWidth, out remains);

                        if (cutline == "")
                        {
                            string firstword = currentline.Split(' ').ElementAt(0);
                            remains = currentline.Substring(firstword.Length, currentline.Length - firstword.Length);
                            currentline = firstword;
                        }
                        else
                        {
                            list.Add(Pad(cutline));
                            currentline = remains;
                        }
                    }
                    else
                    {
                        remains = currentline.Substring(totalWidth, currentline.Length - totalWidth) + remains;
                        currentline = currentline.Substring(0, totalWidth);
                    }
                }
                else
                {
                    list.Add(Pad(currentline));

                    if (currentline == remains || remains == "") { currentline = remains = ""; i++; }
                    else currentline = remains;
                }
            }

            string Pad(string text)
            {
                switch (align)
                {
                    case TextAlign.Left:
                        return text;

                    case TextAlign.Right:
                        return text.PadLeft(totalWidth);

                    case TextAlign.Center:
                        return PadBoth(text, totalWidth);

                    default:
                        return "";
                }
            }

            return list;
        }

        public static string CutText(string text, int totalWidth, out string remains)
        {
            var words = text.Split(' ').ToArray();
            string GetNextWord(int i)
            {
                if (i < words.Length)
                {
                    if (i < words.Length - 1) return $"{words[i]} ";
                    else return words[i];
                }
                else return "";
            }

            string line = "";
            string nextword = GetNextWord(0);

            for (int j = 0; j < words.Length && (line + nextword).Length <= totalWidth; j++)
            {
                line += nextword;
                nextword = GetNextWord(j + 1);
            }

            remains = text.Substring(line.Length, text.Length - line.Length);

            return line;
        }

        public static void PrintLinesAt(IEnumerable<string> lines, Location startLoc, TextAlign align = TextAlign.Left, int totalWidth = 0)
        {
            if (totalWidth <= 0) totalWidth = Console.BufferWidth - startLoc.Left;

            List<string> alignedLines = AlignLines(lines, totalWidth, align);

            Console.SetCursorPosition(startLoc.Left, startLoc.Top);

            foreach (var line in alignedLines) Console.WriteLine(line);
        }

        public static void PrintLinesAt(string text, Location startLoc, TextAlign align = TextAlign.Left, int totalWidth = 0) => PrintLinesAt(text.Split('\n'), startLoc, align, totalWidth);

        public static void ClearBehind() 
        {
            int origLeft = Console.CursorLeft;
            for (int left = origLeft; left < Console.BufferWidth; left++) {
                Console.CursorLeft = left;
                Console.Write(" ");
            }
            Console.CursorLeft = origLeft;
        }

        public static void ClearLine(int top) 
        {
            Console.SetCursorPosition(0, top);
            ClearBehind();
        }
    }
}
