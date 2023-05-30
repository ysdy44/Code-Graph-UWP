using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace Code_Graph.Elements
{
    public sealed class CommandListView : ListView
    {
        //@Command
        public ICommand Command { get; set; }
        public CommandListView()
        {
            base.ItemClick += (s, e) =>
            {
                if (e.ClickedItem is SymbolIcon item)
                {
                    this.Command?.Execute(item.Symbol); // Command
                }
            };
        }
    }
}