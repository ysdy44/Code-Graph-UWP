namespace Code_Graph.Project.Datas
{
    public sealed class CsprojData
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }

        public int Index { get; set; }
        public int[] Children { get; set; }
    }
}