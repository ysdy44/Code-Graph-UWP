namespace Code_Graph.Project
{
    public sealed class Group
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Level Level { get; internal set; }
        public int[] Source { get; set; }
        public override string ToString() => this.Source == null ? $"{this.Level}" : $"{this.Level} {this.Source.Length}";
    }
}