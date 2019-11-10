using System;
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
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="HighScoreBoard" /> class.
        /// </summary>
        public HighScoreBoard()
        {
            this.InitializeComponent();

            ((HighScoreBoardViewModel) this.DataContext).ReturnSelected += this.returnToStart;
        }

        private void returnToStart(object sender, EventArgs e)
        {
            this.Frame.Navigate(typeof(StartPage));
        }

        #endregion
    }
}