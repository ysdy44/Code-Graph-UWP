using Windows.UI.Xaml.Markup;

namespace Code_Graph.Strings
{
    [MarkupExtensionReturnType(ReturnType = typeof(string))]
    public class UIExtension : MarkupExtension
    {
        public UIType Type { get; set; }
        protected override object ProvideValue() => App.Resource.GetString(this.Type.ToString());
    }
}