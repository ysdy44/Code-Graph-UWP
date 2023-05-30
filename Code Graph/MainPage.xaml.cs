using Code_Graph.Project;
using System.Collections.Generic;
using System.Windows.Input;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace Code_Graph
{
    public sealed partial class MainPage : Page, ICommand
    {
        //@Converter
        private string BooleanToGlyphConverter(bool value) => value ? "\uE26C" : "\uE26B";

        Csproj[] Files;
        Group[] Groups;
        KeyValuePair<int, int>[] Links;

        int StartingIndex;
        Point StartingPoint;

        Style this[Level item] => item == default ? null : this.Resources[$"{item}Style"] as Style;
        Thumb this[int item] => this.ThumbCanvas.Children[item] as Thumb;

        UIElementCollection LineChildren => this.LineCanvas.Children;
        UIElementCollection EllipseChildren => this.EllipseCanvas.Children;
        UIElementCollection ThumbChildren => this.ThumbCanvas.Children;

        public MainPage()
        {
            this.InitializeComponent();
            this.Grid.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;
 
                this.AlignmentGrid.RebuildWithInterpolation(e.NewSize);
            };
            this.AlignmentGrid.ManipulationStarted += (s, e) =>
            {
            };
            this.AlignmentGrid.ManipulationDelta += (s, e) =>
            {
                this.TranslateTransform.X += e.Delta.Translation.X;
                this.TranslateTransform.Y += e.Delta.Translation.Y;
            };
            this.AlignmentGrid.ManipulationCompleted += (s, e) =>
            {
                int offfsetX = (int)(this.TranslateTransform.X / 10);
                int offsetY = (int)(this.TranslateTransform.Y / 10);

                this.TranslateTransform.X = 0;
                this.TranslateTransform.Y = 0;

                if (offfsetX == 0 && offsetY == 0) return;

                for (int i = 0; i < this.Groups.Length; i++)
                {
                    Group group = this.Groups[i];

                    int x = group.X + offfsetX;
                    int y = group.Y + offsetY;

                    group.X = x;
                    group.Y = y;

                    if (this.ThumbChildren[i] is FrameworkElement item)
                    {
                        Canvas.SetLeft(item, x * 10 - (item.ActualWidth / 2));
                        Canvas.SetTop(item, y * 10 - (item.ActualHeight / 2));
                    }
                }
                this.Click(OptionType.Update);
            };
        }
    }
}