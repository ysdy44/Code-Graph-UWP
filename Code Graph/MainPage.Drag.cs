using Code_Graph.Project;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Shapes;

namespace Code_Graph
{
    partial class MainPage
    {
        private void DragSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize == Size.Empty) return;
            if (e.NewSize == e.PreviousSize) return;

            if (sender is FrameworkElement item)
            {
                int index = this.ThumbChildren.IndexOf(item);
                if (index < 0) return;

                Group group = this.Groups[index];
                if (group.X == default && group.Y == default) return;

                double w = e.NewSize.Width;
                double h = e.NewSize.Height;

                Canvas.SetLeft(item, group.X * 10 - (w / 2));
                Canvas.SetTop(item, group.Y * 10 - (h / 2));

                for (int i = 0; i < this.Links.Length; i++)
                {
                    KeyValuePair<int, int> link = this.Links[i];

                    if (link.Key == index)
                    {
                        Line line = this.LineChildren[i] as Line;
                        line.X1 = group.X * 10;
                        line.Y1 = group.Y * 10;
                    }

                    if (link.Value == index)
                    {
                        Line line = this.LineChildren[i] as Line;
                        line.X2 = group.X * 10;
                        line.Y2 = group.Y * 10;

                        Group key = this.Groups[link.Key];
                        Group value = group;
                        Point arrow = new IntersectsRectAndLine(w, h, value.X * 10, value.Y * 10, key.X * 10, key.Y * 10).Arrow;

                        Ellipse ellipse = this.EllipseChildren[i] as Ellipse;
                        Canvas.SetLeft(ellipse, arrow.X - 6);
                        Canvas.SetTop(ellipse, arrow.Y - 6);
                    }
                }
            }
        }
        private void DragStarted(object sender, DragStartedEventArgs e)
        {
            if (sender is FrameworkElement item)
            {
                this.StartingIndex = this.ThumbChildren.IndexOf(item);
                if (this.StartingIndex < 0) return;

                Group group = this.Groups[this.StartingIndex];
                this.StartingPoint.X = group.X * 10;
                this.StartingPoint.Y = group.Y * 10;
            }
        }
        private void DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (this.StartingIndex < 0) return;

            this.StartingPoint.X += e.HorizontalChange;
            this.StartingPoint.Y += e.VerticalChange;

            if (sender is FrameworkElement item)
            {
                int x = (int)(this.StartingPoint.X / 10);
                int y = (int)(this.StartingPoint.Y / 10);

                Group group = this.Groups[this.StartingIndex];
                if (group.X == x && group.Y == y) return;

                group.X = x;
                group.Y = y;

                Canvas.SetLeft(item, x * 10 - (item.ActualWidth / 2));
                Canvas.SetTop(item, y * 10 - (item.ActualHeight / 2));
            }
            this.Click(OptionType.Update);
        }
        private void DragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (this.StartingIndex < 0) return;

            this.Click(OptionType.Update);
        }
    }
}