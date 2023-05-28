using Code_Graph.Project;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace Code_Graph
{
    public sealed partial class MainPage : Page
    {
        Csproj[] Files;
        Group[] Groups;
        KeyValuePair<int, int>[] Links;

        int StartingIndex;
        Point StartingPoint;

        public MainPage()
        {
            this.InitializeComponent();
        }
    }
}