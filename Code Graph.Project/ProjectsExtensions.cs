using Code_Graph.Project.Datas;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Code_Graph.Project
{
    public static class ProjectsExtensions
    {
        public static string FileChoices => ".csgraph";
        public const string FileType = ".csproj";
        private const string Reference = "ProjectReference";
        public static bool Contains(string value) => value.Contains(ProjectsExtensions.Reference);
        public static T[] ToNullableArray<T>(this IEnumerable<T> source)
        {
            if (source == null) return null;
            if (source.Count() == 0) return null;
            return source.ToArray();
        }

        public static Csproj[] ToFiles(this IList<CsprojLine> references)
        {
            int length = references.Count;
            Csproj[] files = new Csproj[length];

            for (int i = 0; i < length; i++)
            {
                CsprojLine item = references[i];
                files[i] = (new Csproj
                {
                    Name = item.Name,
                    DisplayName = item.DisplayName,

                    Index = i,
                    Children = references.Children(item.Lines).ToNullableArray()
                });
            }

            for (int i = 0; i < length; i++)
            {
                Csproj item = files[i];
                item.Parents = files.Parents(i).ToNullableArray();
            }
            return files;
        }
        public static IEnumerable<int> Parents(this Csproj[] files, int parent)
        {
            for (int i = 0; i < files.Length; i++)
            {
                Csproj item = files[i];
                if (item.Children == null) continue;

                if (item.Children.Contains(parent))
                {
                    yield return i;
                }
            }
        }
        private static IEnumerable<int> Children(this IList<CsprojLine> references, string[] lines)
        {
            foreach (string line in lines)
            {
                for (int i = 0; i < references.Count; i++)
                {
                    if (line.Contains(references[i].Name))
                    {
                        yield return i;
                    }
                }
            }
        }
        public static string DisplayName(this Csproj[] files, ICollection<int> file)
        {
            switch (file.Count)
            {
                case 0:
                    return string.Empty;
                case 1:
                    return files[file.First()].DisplayName;
                default:
                    StringBuilder sb = new StringBuilder();
                    foreach (int item in file)
                    {
                        sb.AppendLine(files[item].DisplayName);
                    }
                    return sb.ToString().Trim();
            }
        }

        public static int Index(Csproj file) => file.Index;
        private static double Key(Csproj file) => file.Key;
        public static IEnumerable<Group> ToGroups(this Csproj[] files, int x, int y) => from c in files.GroupBy(ProjectsExtensions.Key) select c.ToGroup(x, y);
        private static Group ToGroup(this IEnumerable<Csproj> files, int x, int y)
        {
            Level level = files.First().Level;
            int[] source = files.Select(ProjectsExtensions.Index).ToNullableArray();
            switch (level)
            {
                case Level.None:
                    return new Group
                    {
                        Level = level,
                        Source = files.Select(ProjectsExtensions.Index).ToNullableArray()
                    };
                case Level.Level1:
                    return new Group
                    {
                        X = x,
                        Y = source == null ? y + 4 : y + 4 + source.Length,
                        Level = level,
                        Source = files.Select(ProjectsExtensions.Index).ToNullableArray()
                    };
                case Level.Level2:
                    return new Group
                    {
                        X = x,
                        Y = y,
                        Level = level,
                        Source = files.Select(ProjectsExtensions.Index).ToNullableArray()
                    };
                case Level.Level3:
                    return new Group
                    {
                        X = x,
                        Y = source == null ? y - 4 : y - 4 - source.Length * 2,
                        Level = level,
                        Source = files.Select(ProjectsExtensions.Index).ToNullableArray()
                    };
                default:
                    return new Group
                    {
                        Level = level,
                        Source = files.Select(ProjectsExtensions.Index).ToNullableArray()
                    };
            }
        }

        public static IEnumerable<KeyValuePair<int, int>> ToLines(this Group[] groups, Csproj[] files)
        {
            for (int i = 0; i < groups.Length; i++)
            {
                Group item1 = groups[i];
                if (item1.Source == null) continue;

                int first = item1.Source.First();
                Csproj file = files[first];
                if (file.Children == null) continue;

                foreach (int item in file.Children)
                {
                    for (int j = 0; j < groups.Length; j++)
                    {
                        Group item2 = groups[j];
                        if (item2.Source.Contains(item))
                        {
                            yield return new KeyValuePair<int, int>(i, j);
                        }
                    }
                }
            }
        }

        public static Csproj[] ToFiles(this IEnumerable<GroupData> datas)
        {
            int count = 0;
            foreach (GroupData item in datas)
            {
                if (item.Source == null) continue;

                foreach (CsprojData data in item.Source)
                {
                    if (count < data.Index)
                    {
                        count = data.Index;
                    }
                }
            }
            if (count == 0) return null;

            Csproj[] files = new Csproj[count + 1];
            foreach (GroupData item in datas)
            {
                if (item.Source == null) continue;

                foreach (CsprojData data in item.Source)
                {
                    files[data.Index] = new Csproj
                    {
                        DisplayName = data.DisplayName,
                        Name = data.Name,
                        Children = data.Children.ToNullableArray()
                    };
                }
            }

            for (int i = 0; i < count + 1; i++)
            {
                Csproj item = files[i];
                item.Parents = files.Parents(i).ToNullableArray();
            }
            return files;
        }
        public static IEnumerable<Group> ToGroups(this Csproj[] files, IEnumerable<GroupData> datas) => datas.Select(c => files.ToGroup(c));
        private static Group ToGroup(this Csproj[] files, GroupData data) => new Group
        {
            X = data.X,
            Y = data.Y,
            Source = data.Source.Select(a => a.Index).ToArray(),
            Level = data.Source == null ? default : files[data.Source.First().Index].Level
        };
    }
}