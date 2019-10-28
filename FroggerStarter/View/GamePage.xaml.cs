
using Windows.Foundation;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using FroggerStarter.Controller;
using FroggerStarter.Model;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FroggerStarter.View
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage
    {
        #region Data members

        private readonly double applicationHeight = (double) Application.Current.Resources["AppHeight"];
        private readonly double applicationWidth = (double) Application.Current.Resources["AppWidth"];
 
        private readonly GameManager gameManager;

        #endregion

        #region Constructors

        public GamePage()
        {
            this.InitializeComponent();

            ApplicationView.PreferredLaunchViewSize = new Size
                {Width = this.applicationWidth, Height = this.applicationHeight};
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView()
                           .SetPreferredMinSize(new Size(this.applicationWidth, this.applicationHeight));

            Window.Current.CoreWindow.KeyDown += this.coreWindowOnKeyDown;

            this.initializeGameBoardConstants();

            this.gameManager = new GameManager();
            this.gameManager.LifeLoss += this.onLivesUpdated;
            this.gameManager.ScoreUpdated += this.onScoreUpdated;
            this.gameManager.GameOver += this.onGameOver;
            this.gameManager.ScoreTimerTick += this.onProgressBarOnTick;

            this.gameManager.InitializeGame(this.canvas);
        }

        private void initializeGameBoardConstants()
        {
            GameBoard.BackgroundWidth =     this.applicationWidth;
            GameBoard.BackgroundHeight =    this.applicationHeight;
            GameBoard.HighRoadYLocation =   (double) Application.Current.Resources["HighRoadYLocation"];
            GameBoard.BottomRoadYLocation = (double) Application.Current.Resources["BottomRoadYLocation"];
            GameBoard.HomeWidth =           (double) Application.Current.Resources["HomeWidth"];
            GameBoard.HomeLocationGapSize = (double) Application.Current.Resources["HomeLocationGapSize"];
        }

        #endregion

        #region Methods

        private void coreWindowOnKeyDown(CoreWindow sender, KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case VirtualKey.Left:
                    this.gameManager.MovePlayerLeft();
                    break;
                case VirtualKey.Right:
                    this.gameManager.MovePlayerRight();
                    break;
                case VirtualKey.Up:
                    this.gameManager.MovePlayerUp();
                    break;
                case VirtualKey.Down:
                    this.gameManager.MovePlayerDown();
                    break;
            }
        }

        private void onProgressBarOnTick(object sender, ScoreTimerTickEventArgs e)
        {
            if (this.timeProgressBar.Value >= this.timeProgressBar.Minimum)
            {
                this.timeProgressBar.Value = e.ScoreTick;
            }
        }

        private void onLivesUpdated(object sender, LivesUpdatedEventArgs e)
        {
            this.numberLives.Text = e.Lives.ToString();
        }

        private void onScoreUpdated(object sender, ScoreUpdatedEventArgs e)
        {
            this.scoreValue.Text = e.Score.ToString();
        }

        private void onGameOver(object sender, GameOverEventArgs e)
        {
            if (e.GameOver)
            {
                this.gameOver.Visibility = Visibility.Visible;
            }
        }

        #endregion
    }
}