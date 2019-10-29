using System;
using System.Linq;
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

        private Canvas gameCanvas;
        private Frog player;
        private LaneManager laneManager;
        private PlayerStats playerStats;
        private DispatcherTimer timer;
        private DispatcherTimer scoreTimer;
        private FrogHomes frogHomes;

        #endregion

        #region Events

        public EventHandler<LivesUpdatedEventArgs> LifeLoss;
        public EventHandler<ScoreUpdatedEventArgs> ScoreUpdated;
        public EventHandler<GameOverEventArgs> GameOver;
        public EventHandler<ScoreTimerTickEventArgs> ScoreTimerTick;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameManager" /> class.
        ///     Precondition: backgroundHeight > 0
        ///                   backgroundWidth > 0
        /// 
        ///     Post-condition: none
        ///     
        /// </summary>
        public GameManager()
        {
            this.setupGameTimer();
            this.setupScoreTimer();
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
            this.createAndPlaceFrogHomes();
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
            this.player.MoveRightWithBoundaryCheck(GameBoard.BackgroundWidth);
        }

        /// <summary>
        ///     Moves the player up.
        ///     Precondition: none
        ///     Post-condition: player.Y = player.Y - player.SpeedY
        /// </summary>
        public void MovePlayerUp()
        {
            this.player.MoveUpWithBoundaryCheck(GameBoard.HighRoadYLocation);
            
            if (this.hasReachedAnEmptyHome())
            {
                this.makeHitHomeVisible();
                this.setPlayerToCenterOfBottomLane();
                this.increaseScore();
                this.checkGameStatusForGameOver();
                ScoreTimer.ResetScoreTick();

            } else if (this.player.Y < GameBoard.HighRoadYLocation + GameBoard.RoadShoulderOffset)
            {
                this.lifeLost();
                this.resetPlayerAndObstacles();
            }
        }

        /// <summary>
        ///     Moves the player down.
        ///     Precondition: none
        ///     Post-condition: player.Y = player.Y + player.SpeedY
        /// </summary>
        public void MovePlayerDown()
        {
            this.player.MoveDownWithBoundaryCheck(GameBoard.BottomRoadYLocation + GameBoard.RoadShoulderOffset);
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

        private void setupScoreTimer()
        {
            this.scoreTimer = new DispatcherTimer();
            this.scoreTimer.Tick += this.scoreTimerOnTick;
            this.scoreTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            this.scoreTimer.Start();
        }

        private void scoreTimerOnTick(object sender, object e)
        {
            var scoreTick = new ScoreTimerTickEventArgs() { ScoreTick = ScoreTimer.ScoreTick -= 0.02 };
            if (ScoreTimer.IsTimeUp)
            {
                this.lifeLost();
            }
            this.ScoreTimerTick?.Invoke(this, scoreTick);
        }

        private void createAndPlaceFrogHomes()
        {
            this.frogHomes = new FrogHomes();
            this.frogHomes.ToList().ForEach(home => this.gameCanvas.Children.Add(home.Sprite));
        }

        private void createAndPlacePlayer()
        {
            this.player = new Frog();
            this.player.DeathAnimation.ToList().ForEach(frame => this.gameCanvas.Children.Add(frame.Sprite));
            this.gameCanvas.Children.Add(this.player.Sprite);
            this.setPlayerToCenterOfBottomLane();
        }

        private void createAndPlaceObstaclesInLanes()
        {
            this.laneManager = new LaneManager(getRoadStartingYLocation(), GameBoard.BottomRoadYLocation);

            this.laneManager.AddLaneOfObstacles(
                (Direction)GameSettings.Lane5[0],
                (double)GameSettings.Lane5[1],
                (ObstacleType)GameSettings.Lane5[2],
                (int)GameSettings.Lane5[3]);

            this.laneManager.AddLaneOfObstacles(Direction.Left, 1.75, ObstacleType.SemiTruck, 3);
            this.laneManager.AddLaneOfObstacles(Direction.Left, 1.5, ObstacleType.Car, 4);
            this.laneManager.AddLaneOfObstacles(Direction.Right, 1.25, ObstacleType.SemiTruck, 2);
            this.laneManager.AddLaneOfObstacles(Direction.Left, 1.0, ObstacleType.ToadTruck, 3);

            this.placeObstaclesOnCanvas();
        }

        private void placeObstaclesOnCanvas()
        {
            this.laneManager.ToList().ForEach(obstacle => this.gameCanvas.Children.Add(obstacle.Sprite));
        }

        private void setPlayerToCenterOfBottomLane()
        {
            this.player.X = GameBoard.BackgroundWidth / 2 - this.player.Width / 2;
            this.player.Y = (GameBoard.BottomRoadYLocation + GameBoard.RoadShoulderOffset) - this.player.Height;
        }

        private void timerOnTick(object sender, object e)
        {
            this.laneManager.MoveAllObstacles();
            this.checkForPlayerToObstacleCollision();
        }

        private void checkForPlayerToObstacleCollision()
        {
            foreach (var currentObstacle in this.laneManager)
            {
                if (this.player.HasCollidedWith(currentObstacle) && currentObstacle.Sprite.Visibility == Visibility.Visible)
                {
                    this.lifeLost();
                    this.resetPlayerAndObstacles();
                }
            }
        }

        private void resetPlayerAndObstacles()
        {
            this.setPlayerToCenterOfBottomLane();
            this.laneManager.ResetLanesToOneObstacle();
            this.laneManager.ResetObstacleSpawnTimer();
        }

        private void stopGamePlayAndShowGameOver()
        {
            this.timer.Stop();
            this.scoreTimer.Stop();
            this.player.StopMovement();
            this.gameOver();
        }

        private bool hasReachedAnEmptyHome()
        {
            var filledHomes = this.frogHomes.ToList().Where(home => !home.IsFilled);
            var collidedWithHomes = filledHomes.Where(home => this.player.HasCollidedWith(home));

            return collidedWithHomes.Any();
        }

        private void makeHitHomeVisible()
        {
            var hitHome = this.frogHomes.ToList()
                            .Where(home => !home.IsFilled)
                            .First(home => this.player.HasCollidedWith(home));

            hitHome.IsFilled = true;
        }

        private static double getRoadStartingYLocation()
        {
            return GameBoard.HighRoadYLocation + GameBoard.RoadShoulderOffset;
        }

        private void lifeLost()
        {
            this.playerStats.Lives--;
            var life = new LivesUpdatedEventArgs() { Lives = this.playerStats.Lives };
            this.LifeLoss?.Invoke(this, life);
            ScoreTimer.ResetScoreTick();

            this.player.PlayAnimationDeath();

            this.checkGameStatusForGameOver();
        }

        private void checkGameStatusForGameOver()
        {
            if (this.playerStats.Lives == 0 || this.frogHomes.HasHomesBeenFilled)
            {
                this.stopGamePlayAndShowGameOver();
            }
        }

        private void increaseScore()
        {
            this.playerStats.Score += (int) ScoreTimer.ScoreTick;
            var score = new ScoreUpdatedEventArgs() { Score = this.playerStats.Score };
            this.ScoreUpdated?.Invoke(this, score);

            if (this.playerStats.Score == GameSettings.MaxScore)
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
                Lives = GameSettings.TotalNumberOfLives,
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