using Code_Graph.Project;
using Code_Graph.Project.Datas;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Code_Graph
{
    public sealed partial class MainPage : Page, ICommand
    {
        //@Strings
        private string Project => App.Resource.GetString(UIType.Project.ToString());

        //@Converter
        private string BooleanToGlyphConverter(bool value) => value ? "\uE26C" : "\uE26B";

        Csproj[] Files;
        Group[] Groups;
        KeyValuePair<int, int>[] Links;

        int StartingIndex;
        Point StartingPoint;

        UIElementCollection LineChildren => this.LineCanvas.Children;
        UIElementCollection EllipseChildren => this.EllipseCanvas.Children;
        UIElementCollection ThumbChildren => this.ThumbCanvas.Children;

        public MainPage()
        {
            this.InitializeComponent();
            {
                IEnumerable<GroupData> datas = ProjectsExtensions.Samples(this.Project);
                this.Files = datas.ToFiles();
                this.Groups = this.Files.ToGroups(datas).ToArray();
                this.Links = this.Groups.ToLines(this.Files).ToArray();
                this.Click(OptionType.Add);

                this.Click(OptionType.Update);
            }
            this.Grid.SizeChanged += (s, e) =>
            {
                if (e.NewSize == Size.Empty) return;
                if (e.NewSize == e.PreviousSize) return;

                this.AlignmentGrid.RebuildWithInterpolation(e.NewSize);
            };
            this.AlignmentGrid.ManipulationStarted += (s, e) =>
            {
                if (this.Groups == null) return;
            };
            this.AlignmentGrid.ManipulationDelta += (s, e) =>
            {
                if (this.Groups == null) return;
                this.TranslateTransform.X += e.Delta.Translation.X;
                this.TranslateTransform.Y += e.Delta.Translation.Y;
            };
            this.AlignmentGrid.ManipulationCompleted += (s, e) =>
            {
                if (this.Groups == null) return;
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

        //@BackRequested
        /// <summary> The current page no longer becomes an active page. </summary>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
        }
        /// <summary> The current page becomes the active page. </summary>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is GroupData[] datas)
            {
                this.Execute(datas); // Command
            }
        }
    }
}