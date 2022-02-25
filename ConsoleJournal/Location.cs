namespace ConsoleJournal 
{
    public class Location {

        public int Left { get; set; }
        public int Top { get; set; }

        public Location() { }

        public Location(int left, int top) => (Left, Top) = (left, top);

        public override string ToString() => $"({Left};{Top})";
    }
}