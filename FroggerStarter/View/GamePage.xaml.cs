using System;
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
        private readonly double highRoadYLocation = (double) Application.Current.Resources["HighRoadYLocation"];
        private readonly double roadShoulderHeight = (double) Application.Current.Resources["RoadShoulderHeight"];

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

            this.gameManager = new GameManager(this.applicationHeight, this.applicationWidth, this.highRoadYLocation, this.roadShoulderHeight);
            this.gameManager.InitializeGame(this.canvas);
            this.gameManager.LifeLoss += this.onLifeLost;
            this.gameManager.ScoreIncremented += this.onPointScored;
            this.gameManager.GameOver += this.onGameOver;
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

        private void onLifeLost(object sender, LiveLossEventArgs e)
        {
            this.numberLives.Text = e.Lives.ToString();
        }

        private void onPointScored(object sender, ScoreUpdatedEventArgs e)
        {
            this.scoreValue.Text = e.Score.ToString();
        }

        private void onGameOver(object sender, GameOverEventArgs e)
        {
            this.gameOver.Text = e.GameOver.ToString();
        }

        #endregion
    }
}