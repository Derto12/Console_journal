namespace ConsoleJournal 
{
    public struct Location {
        public int left;
        public int top;

        public override string ToString() => $"({left};{top})";
    }
}