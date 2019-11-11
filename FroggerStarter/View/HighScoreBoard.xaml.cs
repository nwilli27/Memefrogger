using System;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FroggerStarter.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FroggerStarter.View
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HighScoreBoard : Page
    {
        private readonly double applicationHeight = (double)Application.Current.Resources["HighScoreBoardHeight"];
        private readonly double applicationWidth = (double)Application.Current.Resources["HighScoreBoardWidth"];

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="HighScoreBoard" /> class.
        /// </summary>
        public HighScoreBoard()
        {
            this.InitializeComponent();

            ApplicationView.GetForCurrentView().TryResizeView(
                new Size(Width = this.applicationWidth, Height = this.applicationHeight)
            );

            ((HighScoreBoardViewModel) this.DataContext).ReturnSelected += this.returnToStart;
        }

        private void returnToStart(object sender, EventArgs e)
        {
            this.Frame.Navigate(typeof(StartPage));
        }

        #endregion
    }
}