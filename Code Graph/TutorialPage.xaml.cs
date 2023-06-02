using Windows.UI.Xaml.Controls;

namespace Code_Graph
{
    public sealed partial class TutorialPage : Page
    {
        public TutorialPage()
        {
            this.InitializeComponent();

            this.Forward00Button.Click += (s, e) => this.FlipView.SelectedIndex = 1;
            this.Forward02Button.Click += (s, e) => this.FlipView.SelectedIndex = 2;
            this.Forward03Button.Click += (s, e) => this.FlipView.SelectedIndex = 3;

            this.Button.Click += (s, e) =>
            {
                if (base.Frame.CanGoBack)
                    base.Frame.GoBack();
                else
                    base.Frame.Navigate(typeof(MainPage));
            };
        }
    }
}