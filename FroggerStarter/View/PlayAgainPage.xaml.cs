using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FroggerStarter.View
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlayAgainPage
    {
        #region Data members

        private readonly double applicationHeight = (double) Application.Current.Resources["PlayAgainHeight"];
        private readonly double applicationWidth = (double) Application.Current.Resources["PlayAgainWidth"];

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayAgainPage" /> class.
        /// </summary>
        public PlayAgainPage()
        {
            this.InitializeComponent();

            ApplicationView.GetForCurrentView().TryResizeView(
                new Size(Width = this.applicationWidth, Height = this.applicationHeight)
            );
        }

        #endregion

        #region Methods

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(StartPage));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HighScoreBoard));
        }

        #endregion
    }
}