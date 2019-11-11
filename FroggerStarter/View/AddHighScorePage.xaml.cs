using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FroggerStarter.Model.Score;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FroggerStarter.View
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddHighScorePage : Page
    {
        #region Data members

        private readonly double applicationHeight = (double) Application.Current.Resources["HighScoreAddHeight"];
        private readonly double applicationWidth = (double) Application.Current.Resources["HighScoreAddWidth"];

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AddHighScorePage" /> class.
        /// </summary>
        public AddHighScorePage()
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
            var highScores = Serializer.Serializer<List<HighScore>>.ReadObjectFromFile("HighScoreBoard");
            if (highScores == null)
            {
                highScores = new List<HighScore>();
            }
            highScores.Add(new HighScore(int.Parse(this.scoreTextBlock.Text), this.nameTextBox.Text, int.Parse(this.levelTextBox.Text)));
            Serializer.Serializer<List<HighScore>>.WriteObjectToFile("HighScoreBoard", highScores);

            Frame.Navigate(typeof(PlayAgainPage));
        }

        #endregion
    }
}