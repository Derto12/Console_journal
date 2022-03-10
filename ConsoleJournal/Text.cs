using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleJournal 
{
    public class Text 
    {
        public enum Align
        {
            Left, Right, Center
        }

        static public ConsoleColor HighlightColor { get; set; } = ConsoleColor.White;
        public string Content {
            get { return _content; }
            set 
            {
                if(value != _content) 
                {
                    if (value == "") _content = value;
                    else _content = AlignLines(value);
                }
            }
        }
        public Align TextAlign
        {
            get { return _textAlign; }
            set
            {
                if(value != _textAlign)
                {
                    _textAlign = value;
                    if (Content != "") Content = AlignLines(Content);
                }
            }
        }
        public int LinePos { get; set; } = -1;
        public (int left, int right) Margin { get; set; }
        public ConsoleColor Color { get; set; } = ConsoleColor.White;
        public bool IsHighlighted { get; set; }

        private Align _textAlign = Align.Left;
        private string _content = "";


        public Text() { }
        public Text(string content)
        {
            Content = content;
        }
        public Text(string content, Align textalign) 
        {
            TextAlign = textalign;
            Content = content;
        }
        public Text(string content, Align textalign, int linePos) 
        {
            TextAlign = textalign;
            Content = content;
            LinePos = linePos;
        }

        public Text(string content, (int left, int right) margin, int linePos, bool isHighLighted) 
        {
            Content = content;
            Margin = margin;
            LinePos = linePos;
            IsHighlighted = isHighLighted;
        }


        private static string PadBoth(string source, int totalWidth)
        {
            int spaces = totalWidth - source.Length;
            int padLeft = spaces / 2 + source.Length;
            return source.PadLeft(padLeft);
        }

        private string AlignLines(string text) 
        {
            var lines = text.Split('\n');
            List<string> list = new List<string>();

            int maxWidth = Console.BufferWidth - (Margin.left + Margin.right);
            int i = 0;
            string currentline = lines.ElementAt(i);
            string remains = "";

            while (i < lines.Count() || remains != "") 
            {
                if (currentline == "") currentline = lines.ElementAt(i);

                if (currentline.Length > maxWidth) 
                {
                    if (currentline.Contains(' ')) 
                    {
                        string cutline = CutText(currentline, maxWidth, out remains);

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
                        remains = currentline.Substring(maxWidth, currentline.Length - maxWidth) + remains;
                        currentline = currentline.Substring(0, maxWidth);
                    }
                } 
                else 
                {
                    list.Add(Pad(currentline));

                    if (currentline == remains || remains == "") { currentline = remains = ""; i++; } else currentline = remains;
                }
            }

            string Pad(string str)
            {
                switch (TextAlign)
                {
                    case Align.Left:
                        return str;

                    case Align.Right:
                        return str.PadLeft(maxWidth);

                    case Align.Center:
                        return PadBoth(str, maxWidth);

                    default:
                        return "";
                }
            }

            string output = "";
            list.ForEach(l => output += $"{l}\n");

            return output;
        }

        private static string CutText(string text, int totalWidth, out string remains)
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

        public void Display() {
            if (LinePos > 0) Console.CursorTop = LinePos;
            if (Margin.left > 0) Console.CursorLeft = Margin.left;
            if (IsHighlighted) (Console.ForegroundColor, Console.BackgroundColor) = (ConsoleColor.Black, HighlightColor);
            else Console.ForegroundColor = Color;

            Console.Write(Content);

            Console.ResetColor();
        }
    }
}
