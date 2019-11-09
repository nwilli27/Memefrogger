using System;

using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FroggerStarter.Constants;
using FroggerStarter.Enums;
using FroggerStarter.Model.Animation;
using FroggerStarter.Model.Area_Managers.Road;
using FroggerStarter.Model.Game_Objects.Home;
using FroggerStarter.Model.Game_Objects.Power_Ups;
using FroggerStarter.Model.Player;
using FroggerStarter.Model.Score;
using FroggerStarter.Model.Sound;
using FroggerStarter.Model.Area_Managers.Water;

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
        private RoadManager roadManager;
        private WaterManager waterManager;
        private FrogHomes frogHomes;
        private PowerUpManager powerUpManager;

        private PlayerStats playerStats;

        private DispatcherTimer timer;
        private DispatcherTimer scoreTimer;

        #endregion

        #region Events

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
            SoundEffectManager.CreateAndLoadAllSoundEffects();

            this.createAndPlaceWaterObstaclesOnCanvas();
            this.createAndPlaceFrogHomes();
            this.createAndPlacePlayer();
            this.createAndPlaceRoadObstaclesOnCanvas();
            this.createAndPlacePowerUps();
            this.setupPlayerStatsAndHud();
            this.createAndPlacePlayerHearts();

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
            this.player.MoveLeftWithBoundaryCheck();
        }

        /// <summary>
        ///     Moves the player to the right.
        ///     Precondition: none
        ///     Post-condition: player.X = player.X + player.SpeedX
        /// </summary>
        public void MovePlayerRight()
        {
            this.player.MoveRightWithBoundaryCheck();
        }

        /// <summary>
        ///     Moves the player up.
        ///     Precondition: none
        ///     Post-condition: player.Y = player.Y - player.SpeedY
        /// </summary>
        public void MovePlayerUp()
        {
            this.player.MoveUpWithBoundaryCheck();
            
            if (this.hasReachedAnEmptyHome())
            {
                this.updateBoardForReachingEmptyHome();
                this.checkGameStatusForGameOver();
            }
            else if (this.hasMovedPastTopBoundary())
            {
                this.lifeLost();
                this.setPlayerToCenterOfBottomLane();
            }
        }

        /// <summary>
        ///     Moves the player down.
        ///     Precondition: none
        ///     Post-condition: player.Y = player.Y + player.SpeedY
        /// </summary>
        public void MovePlayerDown()
        {
            this.player.MoveDownWithBoundaryCheck();
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
            this.roadManager.MoveAllObstacles();
            this.waterManager.MoveAllObstacles();
            
            if (this.player.Y < GameBoard.MiddleRoadYLocation)
            {
                this.checkForPlayerToWaterObstacleCollision();
            }
            else
            {
                this.checkForPlayerToRoadObstacleCollision();
            }

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
            }
            this.ScoreTimerTick?.Invoke(this, scoreTick);
        }

        private void checkForPlayerToRoadObstacleCollision()
        {
            if (this.roadManager.Any(obstacle => this.player.HasCollidedWith(obstacle)))
            {
                this.lifeLost();
                this.setPlayerToCenterOfBottomLane();
            }
        }

        private void checkForPlayerToWaterObstacleCollision()
        {
            var firstCollided = this.waterManager.FirstOrDefault(obstacle => this.player.HasCollidedMoreThanHalfOfSprite(obstacle));

            if (firstCollided != null)
            {
                this.player.MovePlayerWithObstacle(firstCollided);
            }
            else
            {
                this.lifeLost();
                this.setPlayerToCenterOfBottomLane();
            }
        }

        private void checkForPlayerToPowerUpCollision()
        {
            var collidedPowerUp = this.powerUpManager.ToList().FirstOrDefault(powerUp => this.player.HasCollidedWith(powerUp));
            if (collidedPowerUp != null)
            {
                collidedPowerUp.Activate();
                this.powerUpManager.ResetPowerUpsAndSpawnTimer();
            }
        }

        #endregion

        #region SetupAbility Methods

        private void createAndPlaceFrogHomes()
        {
            this.frogHomes = new FrogHomes();
            this.frogHomes.ToList().ForEach(home => this.gameCanvas.Children.Add(home.Sprite));
        }

        private void createAndPlacePlayerHearts()
        {
            foreach (var live in this.playerStats.Lives)
            {
                this.gameCanvas.Children.Add(live.Sprite);
                live.HeartLostAnimation.ToList().ForEach(frame => this.gameCanvas.Children.Add(frame.Sprite));
            }
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

        private void createAndPlaceRoadObstaclesOnCanvas()
        {
            this.roadManager = new RoadManager(getRoadStartingYLocation(), GameBoard.BottomRoadYLocation);

            this.roadManager.AddLaneOfObstacles(GameSettings.RoadLane5);
            this.roadManager.AddLaneOfObstacles(GameSettings.RoadLane4);
            this.roadManager.AddLaneOfObstacles(GameSettings.RoadLane3);
            this.roadManager.AddLaneOfObstacles(GameSettings.RoadLane2);
            this.roadManager.AddLaneOfObstacles(GameSettings.RoadLane1);

            this.roadManager.ToList().ForEach(obstacle => this.gameCanvas.Children.Add(obstacle.Sprite));
        }

        private void createAndPlaceWaterObstaclesOnCanvas()
        {
            this.waterManager = new WaterManager(GameBoard.HighRoadYLocation + GameBoard.RoadShoulderOffset, GameBoard.MiddleRoadYLocation);

            this.waterManager.AddLaneOfObstacles(GameSettings.WaterLane5);
            this.waterManager.AddLaneOfObstacles(GameSettings.WaterLane4);
            this.waterManager.AddLaneOfObstacles(GameSettings.WaterLane3);
            this.waterManager.AddLaneOfObstacles(GameSettings.WaterLane2);
            this.waterManager.AddLaneOfObstacles(GameSettings.WaterLane1);

            this.waterManager.ToList().ForEach(obstacle => this.gameCanvas.Children.Add(obstacle.Sprite));
            this.waterManager.SpeedBoatAnimationFrames.ToList().ForEach(frame => this.gameCanvas.Children.Add(frame.Sprite));
        }

        #endregion

        #region Update Methods

        private void updateBoardForReachingEmptyHome()
        {
            this.makeHitHomeVisible();
            this.setPlayerToCenterOfBottomLane();
            this.increaseScore();
            this.playHomeSafelySoundEffect();
        }

        private void playHomeSafelySoundEffect()
        {
            if (!this.frogHomes.HasHomesBeenFilled)
            {
                SoundEffectManager.PlaySound(SoundEffectType.HomeSafely);
            }    
        }

        private void setPlayerToCenterOfBottomLane()
        {
            this.player.X = GameBoard.BackgroundWidth / 2 - this.player.Width / 2;
            this.player.SetCenteredYLocationOfArea(GameBoard.RoadShoulderOffset, GameBoard.BottomRoadYLocation);
            this.player.Rotate(Direction.Up);
        }

        private void stopGamePlayAndShowGameOver()
        {
            this.player.StopMovement();
            this.timer.Stop();
            this.scoreTimer.Stop();
            this.powerUpManager.StopPowerUpSpawnTimer();
            this.gameOver();
            SoundEffectManager.PlaySound(SoundEffectType.GameOver);
        }

        private void makeHitHomeVisible()
        {
            var hitHome = this.frogHomes.ToList()
                              .Where(home => !home.IsFilled)
                              .First(home => this.player.HasCollidedMoreThanHalfOfSprite(home));

            hitHome.IsFilled = true;
        }


        private void checkGameStatusForGameOver()
        { 
            if (this.frogHomes.HasHomesBeenFilled)
            {
                this.stopGamePlayAndShowGameOver();
            }
            else
            {
                this.setPlayerToCenterOfBottomLane();
                this.resetScoreTimerBar();
            }
        }

        private void stopGamePlayInSlowMotion()
        {
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            this.scoreTimer.Stop();
            this.player.DeathAnimation.AnimationInterval = 1500;
            this.player.PlayDeathAnimation();
            this.gameOver();
        }

        #endregion

        #region Boolean Conditions

        private bool hasReachedAnEmptyHome()
        {
            var filledHomes = this.frogHomes.ToList().Where(home => !home.IsFilled);
            var collidedWithHomes = filledHomes.Where(home => this.player.HasCollidedMoreThanHalfOfSprite(home));

            return collidedWithHomes.Any();
        }

        private bool isGameOver()
        {
            return this.playerStats.TotalLives == 0 || this.frogHomes.HasHomesBeenFilled;
        }

        private bool hasMovedPastTopBoundary()
        {
            return this.player.Y < GameBoard.HighRoadYLocation + GameBoard.RoadShoulderOffset;
        }

        #endregion

        #region Events

        private void lifeLost()
        {
            this.playerStats.TotalLives--;
            this.checkWhatLifeLostSoundToPlay();

            if (this.playerStats.TotalLives == 0)
            {
                this.stopGamePlayInSlowMotion();
            }
            else
            {
                this.scoreTimer.Stop();
                this.player.PlayDeathAnimation();
                this.checkGameStatusForGameOver();
            }
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
            this.player.IsDead = true;
        }

        private void setupPlayerStatsAndHud()
        {
            this.playerStats = new PlayerStats {Score = 0, TotalLives = GameSettings.TotalNumberOfLives };

            var score = new ScoreUpdatedEventArgs() { Score = this.playerStats.Score };
            this.ScoreUpdated?.Invoke(this, score);
        }

        private void onDeathAnimationDone(object sender, AnimationIsFinishedEventArgs e)
        {
            if (e.PlayerDeathIsOver && !this.isGameOver())
            {
                this.player.ResetAfterDeath();

                this.roadManager.ResetLanesToOneObstacle();
                ScoreTimer.ResetScoreTick();
                this.scoreTimer.Start();

                this.powerUpManager.ResetPowerUpsAndSpawnTimer();
            }
        }

        #endregion

        #region Private Helpers

        private static double getRoadStartingYLocation()
        {
            return GameBoard.MiddleRoadYLocation + GameBoard.RoadShoulderOffset;
        }

        private void checkWhatLifeLostSoundToPlay()
        {
            if (this.playerStats.TotalLives == 0)
            {
                SoundEffectManager.PlaySound(SoundEffectType.GtaDeath);
            }
            else if (this.hasMovedPastTopBoundary())
            {
                SoundEffectManager.PlaySound(SoundEffectType.AlmostHadIt);
            }
            else if (this.player.Y < GameBoard.MiddleRoadYLocation)
            {
                SoundEffectManager.PlaySound(SoundEffectType.MarioDrown);
            }
            else
            {
                SoundEffectManager.PlayRandomPlayerDeathSound();
            }
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