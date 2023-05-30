using Windows.UI.Xaml.Markup;

namespace Code_Graph.Strings
{
    [MarkupExtensionReturnType(ReturnType = typeof(OptionType))]
    public class OptionTypeExtension : MarkupExtension
    {
        public OptionType Type { get; set; }
        protected override object ProvideValue() => this.Type;
    }
}