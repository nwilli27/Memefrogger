using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FroggerStarter.Constants;
using FroggerStarter.Enums;
using FroggerStarter.Model;
using FroggerStarter.Model.Animation;
using FroggerStarter.Model.Game_Objects.Power_Ups;
using FroggerStarter.Model.Score;

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
        private FrogHomes frogHomes;
        private PowerUpManager powerUpManager;

        private PlayerStats playerStats;

        private DispatcherTimer timer;
        private DispatcherTimer scoreTimer;

        #endregion

        #region Events

        /// <summary>
        ///     The life loss Event Args
        /// </summary>
        public EventHandler<LivesUpdatedEventArgs> LifeLoss;

        /// <summary>
        ///     The score updated Event Args
        /// </summary>
        public EventHandler<ScoreUpdatedEventArgs> ScoreUpdated;

        /// <summary>
        ///     The game over Event Args
        /// </summary>
        public EventHandler<GameOverEventArgs> GameOver;

        /// <summary>
        ///     The score timer tick Event Args
        /// </summary>
        public EventHandler<ScoreTimerTickEventArgs> ScoreTimerTick;

        #endregion

        #region Constants

        private const double ScoreTimeReduction = 0.02;
        private const int GameTimerInterval = 15;
        private const int ScoreTimerInterval = 10;

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
            this.createAndPlacePowerUps();
            this.setupPlayerStatsAndHud();

            this.setupGameTimer();
            this.setupScoreTimer();
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
                this.updateBoardForReachingEmptyHome();
                this.checkGameStatusForGameOver();
            }
            else if (this.hasMovedPastTopBoundary())
            {
                this.lifeLost();
                this.setPlayerToCenterOfBottomLane();
                this.player.HasCollided = true;
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

        #region Timer Methods

        private void setupGameTimer()
        {
            this.timer = new DispatcherTimer();
            this.timer.Tick += this.timerOnTick;
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, GameTimerInterval);
            this.timer.Start();
        }

        private void timerOnTick(object sender, object e)
        {
            this.laneManager.MoveAllObstacles();
            this.checkForPlayerToObstacleCollision();
            this.checkForPlayerToPowerUpCollision();
        }

        private void setupScoreTimer()
        {
            this.scoreTimer = new DispatcherTimer();
            this.scoreTimer.Tick += this.scoreTimerOnTick;
            this.scoreTimer.Interval = new TimeSpan(0, 0, 0, 0, ScoreTimerInterval);
            this.scoreTimer.Start();
        }

        private void scoreTimerOnTick(object sender, object e)
        {
            var scoreTick = new ScoreTimerTickEventArgs() { ScoreTick = ScoreTimer.ScoreTick -= ScoreTimeReduction };
            if (ScoreTimer.IsTimeUp)
            {
                this.lifeLost();
                this.setPlayerToCenterOfBottomLane();
                this.player.HasCollided = true;
            }
            this.ScoreTimerTick?.Invoke(this, scoreTick);
        }

        private void checkForPlayerToObstacleCollision()
        {
            foreach (var currentObstacle in this.laneManager)
            {
                if (this.player.HasCollidedWith(currentObstacle) && currentObstacle.IsActive)
                {
                    this.lifeLost();
                    this.setPlayerToCenterOfBottomLane();
                    this.player.HasCollided = true;
                }
            }
        }

        private void checkForPlayerToPowerUpCollision()
        {
            var collidedPowerUp = this.powerUpManager.ToList().FirstOrDefault(powerUp => this.player.HasCollidedWith(powerUp));
            if (collidedPowerUp != null)
            {
                collidedPowerUp.activate();
                this.powerUpManager.ResetPowerUpSpawnTimer();
            }
        }

        #endregion

        #region Setup Methods

        private void createAndPlaceFrogHomes()
        {
            this.frogHomes = new FrogHomes();
            this.frogHomes.ToList().ForEach(home => this.gameCanvas.Children.Add(home.Sprite));
        }

        private void createAndPlacePowerUps()
        {
            this.powerUpManager = new PowerUpManager();
            this.powerUpManager.ToList().ForEach(powerUp => this.gameCanvas.Children.Add(powerUp.Sprite));
        }

        private void createAndPlacePlayer()
        {
            this.player = new Frog();
            this.setupPlayerAnimations();
            this.gameCanvas.Children.Add(this.player.Sprite);
            this.setPlayerToCenterOfBottomLane();
        }

        private void setupPlayerAnimations()
        {
            this.player.DeathAnimation.ToList().ForEach(frame => this.gameCanvas.Children.Add(frame.Sprite));
            this.player.DeathAnimation.AnimationFinished += this.onDeathAnimationDone;
            this.player.FrogLeapAnimation.ToList().ForEach(frame => this.gameCanvas.Children.Add(frame.Sprite));
        }

        private void createAndPlaceObstaclesInLanes()
        {
            this.laneManager = new LaneManager(getRoadStartingYLocation(), GameBoard.BottomRoadYLocation);

            this.laneManager.AddLaneOfObstacles(GameSettings.Lane5);
            this.laneManager.AddLaneOfObstacles(GameSettings.Lane4);
            this.laneManager.AddLaneOfObstacles(GameSettings.Lane3);
            this.laneManager.AddLaneOfObstacles(GameSettings.Lane2);
            this.laneManager.AddLaneOfObstacles(GameSettings.Lane1);

            this.placeObstaclesOnCanvas();
        }

        private void placeObstaclesOnCanvas()
        {
            this.laneManager.ToList().ForEach(obstacle => this.gameCanvas.Children.Add(obstacle.Sprite));
        }

        #endregion

        #region Update Methods

        private void updateBoardForReachingEmptyHome()
        {
            this.makeHitHomeVisible();
            this.setPlayerToCenterOfBottomLane();
            this.increaseScore();
        }

        private void setPlayerToCenterOfBottomLane()
        {
            this.player.X = GameBoard.BackgroundWidth / 2 - this.player.Width / 2;
            this.player.Y = (GameBoard.BottomRoadYLocation + GameBoard.RoadShoulderOffset) - this.player.Height;
            this.player.Rotate(Direction.Up);
        }

        private void stopGamePlayAndShowGameOver()
        {
            this.timer.Stop();
            this.scoreTimer.Stop();
            this.player.StopMovement();
            this.gameOver();
        }

        private void makeHitHomeVisible()
        {
            var hitHome = this.frogHomes.ToList()
                              .Where(home => !home.IsFilled)
                              .First(home => this.player.HasCollidedWith(home));

            hitHome.IsFilled = true;
        }

        private void checkGameStatusForGameOver()
        {
            if (this.isGameOver())
            {
                this.stopGamePlayAndShowGameOver();
            }
            else
            {
                this.setPlayerToCenterOfBottomLane();
                this.resetScoreTimerBar();
            }
        }

        #endregion

        #region Boolean Conditions

        private bool hasReachedAnEmptyHome()
        {
            var filledHomes = this.frogHomes.ToList().Where(home => !home.IsFilled);
            var collidedWithHomes = filledHomes.Where(home => this.player.HasCollidedWith(home));

            return collidedWithHomes.Any();
        }

        private bool isGameOver()
        {
            return this.playerStats.Lives == 0 || this.frogHomes.HasHomesBeenFilled;
        }

        private bool hasMovedPastTopBoundary()
        {
            return this.player.Y < GameBoard.HighRoadYLocation + GameBoard.RoadShoulderOffset;
        }

        #endregion

        #region Events

        private void lifeLost()
        {
            this.playerStats.Lives--;
            var life = new LivesUpdatedEventArgs() { Lives = this.playerStats.Lives };
            this.LifeLoss?.Invoke(this, life);

            this.player.PlayDeathAnimation();
            this.scoreTimer.Stop();

            this.checkGameStatusForGameOver();
        }

        private void resetScoreTimerBar()
        {
            ScoreTimer.ResetScoreTick();
            var scoreTick = new ScoreTimerTickEventArgs() { ScoreTick = ScoreTimer.ScoreTick };
            this.ScoreTimerTick?.Invoke(this, scoreTick);
        }

        private void increaseScore()
        {
            this.playerStats.Score += (int)ScoreTimer.ScoreTick;
            var score = new ScoreUpdatedEventArgs() { Score = this.playerStats.Score };
            this.ScoreUpdated?.Invoke(this, score);
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

        private void onDeathAnimationDone(object sender, AnimationIsFinishedEventArgs e)
        {
            if (e.PlayerDeathIsOver && !this.isGameOver())
            {
                this.player.ChangeSpriteVisibility(true);
                this.player.StartMovement();
                this.laneManager.ResetLanesToOneObstacle();
                ScoreTimer.ResetScoreTick();
                this.scoreTimer.Start();
                this.player.HasCollided = false;
                this.powerUpManager.ResetPowerUpSpawnTimer();
            }
        }

        #endregion

        #region Private Helpers

        private static double getRoadStartingYLocation()
        {
            return GameBoard.HighRoadYLocation + GameBoard.RoadShoulderOffset;
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