namespace Code_Graph.Project
{
    public sealed partial class Csproj
    {
        public string Name { get; internal set; }
        public string DisplayName { get; set; }

        public int Index { get; internal set; }
        public int[] Children { get; internal set; }
        public int[] Parents { get; internal set; }

        public Level Level => this.Children == null ? Level.Level1 : this.Parents == null ? Level.Level3 : Level.Level2;

        public const int Unit = 1024;
        public int Key
        {
            get
            {
                double key = Csproj.Unit;
                switch (this.Level)
                {
                    case Level.None:
                        break;
                    case Level.Level1:
                        foreach (int item in this.Parents)
                        {
                            key += item;
                            key /= item + 1;
                        }
                        break;
                    case Level.Level2:
                        foreach (int item in this.Children)
                        {
                            key *= item + 1;
                            key -= item;
                        }
                        foreach (int item in this.Parents)
                        {
                            key += item;
                            key /= item + 1;
                        }
                        break;
                    case Level.Level3:
                        foreach (int item in this.Children)
                        {
                            key *= (item + 1);
                            key -= item;
                        }
                        break;
                    default:
                        break;
                }
                return (int)(key * Csproj.Unit);
            }
        }

        public override string ToString() => $"{this.Level} {this.Index} {this.Name}";
    }
}