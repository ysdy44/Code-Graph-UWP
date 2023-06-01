using Code_Graph.Project;
using Code_Graph.Project.Datas;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Code_Graph
{
    partial class MainPage
    {
        private XElement Save(int index) => MainPage.Save(this.Files[index]);
        public XElement Save(Group group)
        {
            return group.X == default && group.Y == default ?
                group.Source == null ?
                new XElement("Group") :
                new XElement("Group", group.Source.Select(this.Save))
                : group.Source == null
                    ? new XElement("Group",
                    new XAttribute("X", group.X),
                    new XAttribute("Y", group.Y))

                    : new XElement("Group",
                    new XAttribute("X", group.X),
                    new XAttribute("Y", group.Y),
                    group.Source.Select(this.Save));
        }
        private static XElement Save(Csproj file)
        {
            return file.Children == null
                ? new XElement("File",
                //new XAttribute("Level", file.Level), 
                //new XAttribute("Key", file.Key),
                new XAttribute("Index", file.Index),
                new XAttribute("Name", file.Name),
                new XAttribute("DisplayName", file.DisplayName))

                : new XElement("File",
                //new XAttribute("Level", file.Level),
                //new XAttribute("Key", file.Key),
                new XAttribute("Index", file.Index),
                new XAttribute("Name", file.Name),
                new XAttribute("DisplayName", file.DisplayName),
                file.Children.Select(a => new XElement("Index", a)));
        }

        public static IEnumerable<GroupData> Load(XDocument document)
        {
            foreach (XElement item in document.Root.Elements("Group"))
            {
                GroupData group = new GroupData
                {
                    Source = MainPage.Load(item.Elements("File")).ToNullableArray()
                };

                if (item.Attribute("X") is XAttribute x) group.X = (int)x;
                if (item.Attribute("Y") is XAttribute y) group.Y = (int)y;

                yield return group;
            }
        }
        internal static IEnumerable<CsprojData> Load(IEnumerable<XElement> files)
        {
            foreach (XElement item in files)
            {
                CsprojData file = new CsprojData
                {
                    Children = MainPage.Load(item).ToNullableArray()
                };

                if (item.Attribute("Index") is XAttribute index) file.Index = (int)index;
                if (item.Attribute("Name") is XAttribute Name) file.Name = Name.Value;
                if (item.Attribute("DisplayName") is XAttribute displayName) file.DisplayName = displayName.Value;

                yield return file;
            }
        }
        internal static IEnumerable<int> Load(XElement element)
        {
            foreach (XElement item in element.Elements("Index"))
            {
                yield return (int)item;
            }
        }
    }
}