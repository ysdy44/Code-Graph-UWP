using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Code_Graph.Elements
{
    public sealed partial class ExpandButton : Button
    {
        bool IsExpanded;
        bool AllowExpand;

        public ExpandButton(string text, bool allowExpand)
        {
            this.InitializeComponent();
            this.TextBlock.Text = text;
            this.AllowExpand = allowExpand;

            if (this.AllowExpand)
            {
                this.IsExpanded = false;
                this.FontIcon1.Visibility = Visibility.Visible;
                this.FontIcon2.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.IsExpanded = true;
                this.FontIcon1.Visibility = Visibility.Collapsed;
                this.FontIcon2.Visibility = Visibility.Visible;
            }

            base.Click += (s, e) =>
            {
                if (this.IsExpanded)
                {
                    this.IsExpanded = false;
                    this.FontIcon1.Visibility = Visibility.Visible;
                    this.FontIcon2.Visibility = Visibility.Collapsed;

                    if (this.AllowExpand && base.Parent is Panel panel)
                    {
                        int index = panel.Children.IndexOf(this);
                        if (index < 0) return;
                        if (index + 1 >= panel.Children.Count) return;

                        panel.Children[index + 1].Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    this.IsExpanded = true;
                    this.FontIcon1.Visibility = Visibility.Collapsed;
                    this.FontIcon2.Visibility = Visibility.Visible;

                    if (this.AllowExpand && base.Parent is Panel panel)
                    {
                        int index = panel.Children.IndexOf(this);
                        if (index < 0) return;
                        if (index + 1 >= panel.Children.Count) return;

                        panel.Children[index + 1].Visibility = Visibility.Collapsed;
                    }
                }
            };
        }
    }
}