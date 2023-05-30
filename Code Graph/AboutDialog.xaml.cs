using System;
using Windows.UI.Xaml.Controls;

namespace Code_Graph
{
    public sealed partial class AboutDialog : ContentDialog
    {
        //@Strings
        private Uri GithubLink => new Uri(App.Resource.GetString(UIType.GithubLink.ToString()));
        private Uri FeedbackLink => new Uri(App.Resource.GetString(UIType.FeedbackLink.ToString()));

        //@Construct
        public AboutDialog()
        {
            this.InitializeComponent();
            this.AboutImage.DoubleTapped += (s, e) => this.AboutStoryboard.Begin(); // Storyboard

            base.SecondaryButtonClick += (s, e) => base.Hide();
            base.PrimaryButtonClick += (s, e) => base.Hide();
        }
    }
}