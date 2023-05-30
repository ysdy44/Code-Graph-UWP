using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace Code_Graph.Elements
{
    public sealed partial class ExpandStackPanel : StackPanel
    {
        public ExpandStackPanel(IEnumerable<string> text)
        {
            this.InitializeComponent();
            foreach (var item in text)
            {
                base.Children.Add(new TextBlock
                {
                    Text = item
                });
            }
        }
    }
}