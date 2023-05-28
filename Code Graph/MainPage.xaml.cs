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

        Style this[Level item] => this.Resources[$"{item}Style"] as Style;
        Thumb this[int item] => this.ThumbCanvas.Children[item] as Thumb;

        UIElementCollection LineChildren => this.LineCanvas.Children;
        UIElementCollection EllipseChildren => this.EllipseCanvas.Children;
        UIElementCollection ThumbChildren => this.ThumbCanvas.Children;

        public MainPage()
        {
            this.InitializeComponent();
            base.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;

                this.AlignmentGrid.RebuildWithInterpolation(e.NewSize);
            };
            this.AppBarListView.ItemClick += (s, e) =>
            {
                if (e.ClickedItem is SymbolIcon item)
                {
                    switch (item.Symbol)
                    {
                        case Symbol.Add: this.Click(OptionType.New); break;
                        case Symbol.Save: this.Click(OptionType.Save); break;
                        case Symbol.OpenFile: this.Click(OptionType.Load); break;
                        case Symbol.Delete: this.Click(OptionType.Clear); break;
                        default: break;
                    }
                }
            };
        }
    }
}