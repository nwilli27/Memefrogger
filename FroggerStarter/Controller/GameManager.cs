using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FroggerStarter.Model;

namespace FroggerStarter.Controller
{
    /// <summary>
    ///     Manages all aspects of the game play including moving the player,
    ///     the obstacles, lives, and score.
    /// </summary>
    public class GameManager
    {
        #region Data members

        private readonly double backgroundHeight;
        private readonly double backgroundWidth;
        private readonly double highRoadYLocation;

        private Canvas gameCanvas;
        private Frog player;
        private LaneManager laneManager;
        private PlayerStats playerStats;
        private DispatcherTimer timer;
        private DispatcherTimer obstacleSpeedTimer;

        #endregion

        #region Events

        public EventHandler<LivesUpdatedEventArgs> LifeLoss;
        public EventHandler<ScoreUpdatedEventArgs> ScoreUpdated;
        public EventHandler<GameOverEventArgs> GameOver;

        #endregion

        #region Constants

        private const int BottomLaneOffset = 5;
        private const int RoadShoulderOffset = 50;
        private const int TotalNumberOfLives = 3;
        private const int MaxScore = 3;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameManager" /> class.
        ///     Precondition: backgroundHeight > 0
        ///                   backgroundWidth > 0
        ///     Post-condition: this.backgroundHeight == backgroundHeight
        ///                     this.backgroundWidth == backgroundWidth
        ///                     this.highShouldYLocation == highRoadYLocation
        ///                     
        ///                     obstacleSpeedTimer.Start()
        ///                     gameTimer.Start()
        ///     
        /// </summary>
        /// <param name="backgroundHeight">Height of the background.</param>
        /// <param name="backgroundWidth">Width of the background.</param>
        /// <param name="highRoadYLocation">The high road y location.</param>
        /// <exception cref="ArgumentOutOfRangeException">backgroundHeight &lt;= 0
        /// or
        /// backgroundWidth &lt;= 0</exception>
        public GameManager(double backgroundHeight, double backgroundWidth, double highRoadYLocation)
        {
            if (backgroundHeight <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(backgroundHeight));
            }
            if (backgroundWidth <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(backgroundWidth));
            }

            this.backgroundHeight = backgroundHeight;
            this.backgroundWidth = backgroundWidth;
            this.highRoadYLocation = highRoadYLocation;

            this.setupGameTimer();
            this.setupObstacleSpeedTimer();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Initializes the game working with appropriate classes to place frog
        ///     and obstacles on game screen.
        ///     Precondition: background != null
        ///     Post-condition: Game is initialized and ready for play.
        /// </summary>
        /// <param name="gamePage">The game page.</param>
        /// <exception cref="ArgumentNullException">gameCanvas</exception>
        public void InitializeGame(Canvas gamePage)
        {
            this.gameCanvas = gamePage ?? throw new ArgumentNullException(nameof(gamePage));

            this.createAndPlacePlayer();
            this.createAndPlaceObstaclesInLanes();
            this.setupPlayerStatsAndHud();
        }

        /// <summary>
        ///     Moves the player to the left.
        ///     Precondition: none
        ///     Post-condition: player.X = player.X - player.SpeedX
        /// </summary>
        public void MovePlayerLeft()
        {
            this.player.MoveLeftWithBoundaryCheck(0);
        }

        /// <summary>
        ///     Moves the player to the right.
        ///     Precondition: none
        ///     Post-condition: player.X = player.X + player.SpeedX
        /// </summary>
        public void MovePlayerRight()
        {
            this.player.MoveRightWithBoundaryCheck(this.backgroundWidth);
        }

        /// <summary>
        ///     Moves the player up.
        ///     Precondition: none
        ///     Post-condition: player.Y = player.Y - player.SpeedY
        /// </summary>
        public void MovePlayerUp()
        {
            if (this.hasPlayerMadeItToHighRoad())
            {
                this.setPlayerToCenterOfBottomLane();
                this.pointScored();
            }
            else
            {
                this.player.MoveUpWithBoundaryCheck(this.highRoadYLocation);
            }
        }

        /// <summary>
        ///     Moves the player down.
        ///     Precondition: none
        ///     Post-condition: player.Y = player.Y + player.SpeedY
        /// </summary>
        public void MovePlayerDown()
        {
            this.player.MoveDownWithBoundaryCheck(this.backgroundHeight - BottomLaneOffset);
        }

        #endregion

        #region Private Helpers

        private void setupGameTimer()
        {
            this.timer = new DispatcherTimer();
            this.timer.Tick += this.timerOnTick;
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, 15);
            this.timer.Start();
        }

        private void setupObstacleSpeedTimer()
        {
            this.obstacleSpeedTimer = new DispatcherTimer();
            this.obstacleSpeedTimer.Tick += this.obstacleSpeedTimerOnTick;
            this.obstacleSpeedTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            this.obstacleSpeedTimer.Start();
        }

        private void createAndPlacePlayer()
        {
            this.player = new Frog();
            this.gameCanvas.Children.Add(this.player.Sprite);
            this.setPlayerToCenterOfBottomLane();
        }

        private void createAndPlaceObstaclesInLanes()
        {
            this.laneManager = new LaneManager(this.backgroundWidth, this.getRoadStartingYLocation(), this.getRoadEndingYLocation());

            this.laneManager.AddLaneOfObstacles(Direction.Right, 2.5, ObstacleType.Car, 3);
            this.laneManager.AddLaneOfObstacles(Direction.Left, 2.0, ObstacleType.SemiTruck, 2);
            this.laneManager.AddLaneOfObstacles(Direction.Left, 1.5, ObstacleType.Car, 3);
            this.laneManager.AddLaneOfObstacles(Direction.Right, 1.0, ObstacleType.SemiTruck, 3);
            this.laneManager.AddLaneOfObstacles(Direction.Left, 0.5, ObstacleType.ToadTruck, 2);

            this.placeObstaclesOnCanvas();
        }

        private void placeObstaclesOnCanvas()
        {
            foreach (var currentObstacle in this.laneManager)
            {
                this.gameCanvas.Children.Add(currentObstacle.Sprite);
            }
        }

        private void setPlayerToCenterOfBottomLane()
        {
            this.player.X = this.backgroundWidth / 2 - this.player.Width / 2;
            this.player.Y = this.backgroundHeight - this.player.Height - BottomLaneOffset;
        }

        private void timerOnTick(object sender, object e)
        {
            this.laneManager.MoveAllObstacles();
            this.checkForPlayerToObstacleCollision();
        }

        private void obstacleSpeedTimerOnTick(object sender, object e)
        {
            this.laneManager.IncreaseSpeedOfObstacles(0.15);
        }

        private void resetObstaclesSpeedTimer()
        {
            this.obstacleSpeedTimer.Stop();
            this.obstacleSpeedTimer.Start();
        }

        private void checkForPlayerToObstacleCollision()
        {
            foreach (var currentObstacle in this.laneManager)
            {
                if (this.player.HasCollidedWith(currentObstacle))
                {
                    this.decrementLivesAndResetGame();
                }
            }
        }

        private void decrementLivesAndResetGame()
        {
            this.lifeLost();

            if (this.playerStats.Lives == 0)
            {
                this.stopGamePlayAndShowGameOver();
            }
            else
            {
                this.resetPlayerAndObstacles();
            }
        }

        private void resetPlayerAndObstacles()
        {
            this.setPlayerToCenterOfBottomLane();
            this.laneManager.SetAllObstaclesToDefaultSpeed();
            this.resetObstaclesSpeedTimer();
        }

        private void stopGamePlayAndShowGameOver()
        {
            this.timer.Stop();
            this.player.StopMovement();
            this.gameOver();
        }

        private bool hasPlayerMadeItToHighRoad()
        {
            return this.player.Y - this.player.SpeedY <= this.highRoadYLocation;
        }

        private double getRoadStartingYLocation()
        {
            return this.highRoadYLocation + RoadShoulderOffset;
        }

        private double getRoadEndingYLocation()
        {
            return this.backgroundHeight - RoadShoulderOffset - BottomLaneOffset;
        }

        private void lifeLost()
        {
            this.playerStats.Lives--;
            var life = new LivesUpdatedEventArgs() { Lives = this.playerStats.Lives };
            this.LifeLoss?.Invoke(this, life);
        }

        private void pointScored()
        {
            this.playerStats.Score++;
            var score = new ScoreUpdatedEventArgs() { Score = this.playerStats.Score };
            this.ScoreUpdated?.Invoke(this, score);

            if (this.playerStats.Score == MaxScore)
            {
                this.stopGamePlayAndShowGameOver();
            }
        }

        private void gameOver()
        {
            var gameOver = new GameOverEventArgs() { GameOver = true };
            this.GameOver?.Invoke(this, gameOver);
        }

        private void setupPlayerStatsAndHud()
        {
            this.playerStats = new PlayerStats()
            {
                Score = 0,
                Lives = TotalNumberOfLives,
            };

            var score = new ScoreUpdatedEventArgs() { Score = this.playerStats.Score };
            this.ScoreUpdated?.Invoke(this, score);
            var life = new LivesUpdatedEventArgs() { Lives = this.playerStats.Lives };
            this.LifeLoss?.Invoke(this, life);
        }

        #endregion
    }

    /// <summary>
    ///     Holds the Game Over display for the game over event.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class GameOverEventArgs : EventArgs
    {
        /// <summary>
        ///     Gets or sets a value indicating whether [game over].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [game over]; otherwise, <c>false</c>.
        /// </value>
        public bool GameOver { get; set; }
    }

}