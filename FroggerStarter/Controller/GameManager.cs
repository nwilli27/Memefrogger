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
using FroggerStarter.Model.Levels;

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

        /// <summary>
        ///     The next level Event args
        /// </summary>
        public EventHandler<NextLevelEventArgs> NextLevel;

        /// <summary>
        ///     The pause finished event args
        /// </summary>
        public EventHandler<PauseIsFinishedEventArgs> PauseFinished;

        #endregion

        #region Properties

        private bool IsPlayerInWaterArea     => this.player.Y < GameBoard.MiddleRoadYLocation;
        private bool IsPlayerAboveMiddlePath => this.player.Y + this.player.Height < GameBoard.MiddleRoadYLocation + GameBoard.RoadShoulderOffset;
        private bool IsGameOver              => !PlayerStats.HasLivesLeft || this.frogHomes.HasHomesBeenFilled;
        private bool HasMovedPastTopBoundary => this.player.Y < GameBoard.HighRoadYLocation + GameBoard.RoadShoulderOffset;

        private static double WaterAreaTopLocation => GameBoard.HighRoadYLocation + GameBoard.RoadShoulderOffset;

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
            PlayerStats.SetupPlayerStats();
            LevelManager.CreateLevels();

            this.createAndPlaceEverythingOnGameBoard();

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
            this.player.Move(Direction.Left);
        }

        /// <summary>
        ///     Moves the player to the right.
        ///     Precondition: none
        ///     Post-condition: player.X = player.X + player.SpeedX
        /// </summary>
        public void MovePlayerRight()
        {
            this.player.Move(Direction.Right);
        }

        /// <summary>
        ///     Moves the player up.
        ///     Precondition: none
        ///     Post-condition: player.Y = player.Y - player.SpeedY
        /// </summary>
        public void MovePlayerUp()
        {
            this.player.Move(Direction.Up);

            if (this.hasReachedAnEmptyHome())
            {
                this.updateBoardForReachingEmptyHome();
                this.checkGameStatusForGameOver();
            }
            else if (this.HasMovedPastTopBoundary && !this.IsGameOver)
            {
                this.lifeLost();
            }
        }

        /// <summary>
        ///     Moves the player down.
        ///     Precondition: none
        ///     Post-condition: player.Y = player.Y + player.SpeedY
        /// </summary>
        public void MovePlayerDown()
        {
            this.player.Move(Direction.Down);
        }

        /// <summary>
        ///     Breakdowns the game by resetting the timers and stopping them.
        /// </summary>
        public void ResetGame()
        {
            PlayerAbilities.HasQuickRevive = false;
            this.resetAllTimers();
            this.stopAllTimers();
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

            if (this.IsPlayerInWaterArea)
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
            var scoreTick = new ScoreTimerTickEventArgs() {ScoreTick = ScoreTimer.ScoreTick -= ScoreTimeReduction};

            if (ScoreTimer.IsTimeUp && !this.player.DeathAnimation.AnimationHasStarted)
            {
                this.lifeLost();
            }

            this.ScoreTimerTick?.Invoke(this, scoreTick);
        }

        private void checkForPlayerToRoadObstacleCollision()
        {
            if (this.roadManager.Any(obstacle => this.player.HasCollidedWith(obstacle) && PlayerStats.HasLivesLeft))
            {
                this.lifeLost();
            }
        }

        private void checkForPlayerToWaterObstacleCollision()
        {
            var firstCollided =
                this.waterManager.FirstOrDefault(obstacle => this.player.HasCollidedMoreThanHalfOfSprite(obstacle));

            if (firstCollided != null)
            {
                this.player.MovePlayerWithObstacle(firstCollided);
            }
            else if (PlayerStats.HasLivesLeft)
            {
                this.lifeLost();
            }
        }

        private void setPlayerToSpawnInClosestCheckpoint()
        {
            if (this.IsPlayerAboveMiddlePath)
            {
                this.setPlayerToCenterOfMiddleLane();
            }
            else
            {
                this.setPlayerToCenterOfBottomLane();
            }
        }

        private void setPlayerToCenterOfMiddleLane()
        {
            this.player.X = GameBoard.BackgroundWidth / 2 - this.player.Width / 2;
            this.player.SetCenteredYLocationOfArea(GameBoard.RoadShoulderOffset, GameBoard.MiddleRoadYLocation);
            this.player.Rotate(Direction.Up);
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

        private void createAndPlaceEverythingOnGameBoard()
        {
            this.createAndPlaceWaterObstaclesOnCanvas();
            this.createAndPlaceFrogHomes();
            this.createAndPlacePlayer();
            this.createAndPlaceRoadObstaclesOnCanvas();
            this.createAndPlacePowerUps();
            this.setupPlayerStatsAndHud();
            this.createAndPlacePlayerHearts();
        }

        private void createAndPlaceFrogHomes()
        {
            this.frogHomes = new FrogHomes();
            this.frogHomes.ToList().ForEach(home => this.gameCanvas.Children.Add(home.Sprite));
        }

        private void createAndPlacePlayerHearts()
        {
            PlayerStats.Lives.ToList().ForEach(heart => this.gameCanvas.Children.Add(heart.Sprite));
            PlayerStats.Lives.ToList().ForEach(heart => heart.HeartLostAnimation.ToList().ForEach(frame => this.gameCanvas.Children.Add(frame.Sprite)));
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

            this.roadManager.AddLaneOfObstacles(LevelManager.GetCurrentLevel().RoadLane5);
            this.roadManager.AddLaneOfObstacles(LevelManager.GetCurrentLevel().RoadLane4);
            this.roadManager.AddLaneOfObstacles(LevelManager.GetCurrentLevel().RoadLane3);
            this.roadManager.AddLaneOfObstacles(LevelManager.GetCurrentLevel().RoadLane2);
            this.roadManager.AddLaneOfObstacles(LevelManager.GetCurrentLevel().RoadLane1);

            this.roadManager.ToList().ForEach(obstacle => this.gameCanvas.Children.Add(obstacle.Sprite));
        }

        private void createAndPlaceWaterObstaclesOnCanvas()
        {
            this.waterManager = new WaterManager(WaterAreaTopLocation, GameBoard.MiddleRoadYLocation);

            this.waterManager.AddLaneOfObstacles(LevelManager.GetCurrentLevel().WaterLane5);
            this.waterManager.AddLaneOfObstacles(LevelManager.GetCurrentLevel().WaterLane4);
            this.waterManager.AddLaneOfObstacles(LevelManager.GetCurrentLevel().WaterLane3);
            this.waterManager.AddLaneOfObstacles(LevelManager.GetCurrentLevel().WaterLane2);
            this.waterManager.AddLaneOfObstacles(LevelManager.GetCurrentLevel().WaterLane1);

            this.waterManager.ToList().ForEach(obstacle => this.gameCanvas.Children.Add(obstacle.Sprite));
            this.waterManager.SpeedBoatAnimationFrames.ToList().ForEach(frame => this.gameCanvas.Children.Add(frame.Sprite));
            this.waterManager.StartAllSpeedBoatWaterAnimations();
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
            this.player.IsDead = true;
            this.player.CanMove = false;
            ScoreCalculator.MultiplyPointsByHeartsRemaining();

            this.stopAllTimers();

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
            if (this.frogHomes.HasHomesBeenFilled && LevelManager.IsAtMaxLevel)
            {
                this.stopGamePlayAndShowGameOver();
            } 
            else if (this.frogHomes.HasHomesBeenFilled)
            {
                this.resetBoardForNextLevel();
                this.stopAllTimers();
                this.showNextLevelScreenForFiveSeconds();
            }
            else
            {
                this.setPlayerToCenterOfBottomLane();
                this.resetScoreTimerBar();
            }
        }

        private void resetBoardForNextLevel()
        {
            LevelManager.CurrentLevel++;
            GameSettings.PauseGame = true;
            this.resetCanvas();
            SoundEffectManager.PlaySound(SoundEffectType.NextLevel);
        }

        private void showNextLevelScreenForFiveSeconds()
        {
            var nextLevel = new NextLevelEventArgs() {NextLevel = LevelManager.CurrentLevel};
            this.NextLevel?.Invoke(this, nextLevel);
            GameSettings.PauseTimer.Tick += this.onNextLevelPause;
            GameSettings.PauseTimer.Start();
        }

        private void stopAllTimers()
        {
            this.timer.Stop();
            this.scoreTimer.Stop();
            this.powerUpManager.StopPowerUpSpawnTimer();
            this.waterManager.StopSpeedBoatWaterAnimations();
        }

        private void onNextLevelPause(object sender, object e)
        {
            this.updateBoardForNextLevel();
            this.stopPauseScreenForNextLevel();
        }

        private void updateBoardForNextLevel()
        {
            GameSettings.PauseGame = false;
            this.setCanvasToNextLevel();
            this.resetAllTimers();
            this.player.CanMove = true;
        }

        private void stopPauseScreenForNextLevel()
        {
            var pauseFinished = new PauseIsFinishedEventArgs() {PauseIsFinished = true};
            this.PauseFinished?.Invoke(this, pauseFinished);
        }

        private void resetCanvas()
        {
            this.waterManager.ToList().ForEach(obstacle => this.gameCanvas.Children.Remove(obstacle.Sprite));
            this.roadManager.ToList().ForEach(obstacle => this.gameCanvas.Children.Remove(obstacle.Sprite));
            this.powerUpManager.ToList().ForEach(powerUp => this.gameCanvas.Children.Remove(powerUp.Sprite));
            this.frogHomes.ToList().ForEach(home => this.gameCanvas.Children.Remove(home.Sprite));
            this.gameCanvas.Children.Remove(this.player.Sprite);
            this.waterManager.SpeedBoatAnimationFrames.ToList().ForEach(frame => this.gameCanvas.Children.Remove(frame.Sprite));
        }

        private void setCanvasToNextLevel()
        {
            this.createAndPlaceFrogHomes();
            this.createAndPlaceWaterObstaclesOnCanvas();
            this.createAndPlacePlayer();
            this.createAndPlacePowerUps();
            this.createAndPlaceRoadObstaclesOnCanvas();
        }

        private void resetAllTimers()
        {
            this.powerUpManager.ResetPowerUpsAndSpawnTimer();
            this.resetScoreTimerBar();
            this.scoreTimer.Start();
            this.timer.Start();
        }

        private void stopGamePlayInSlowMotion()
        {
            //TODO make these constants
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            this.scoreTimer.Stop();
            this.waterManager.SlowDownSpeedBoatWaterAnimations();
            this.player.DeathAnimation.AnimationInterval = 1000;
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

        #endregion

        #region Events

        private void lifeLost()
        {
            PlayerStats.TotalLives--;
            this.checkWhatLifeLostSoundToPlay();

            if (PlayerAbilities.HasQuickRevive)
            {
                this.player.PlayDeathAnimation();
                this.setPlayerToSpawnInClosestCheckpoint();
            }
            else if (!PlayerStats.HasLivesLeft)
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
            PlayerStats.Score += ScoreCalculator.CalculateScore();
            var score = new ScoreUpdatedEventArgs() { Score = PlayerStats.Score };
            this.ScoreUpdated?.Invoke(this, score);
        }

        private void gameOver()
        {
            var gameOver = new GameOverEventArgs() { GameOver = true };
            this.GameOver?.Invoke(this, gameOver);
        }

        private void setupPlayerStatsAndHud()
        {
            var score = new ScoreUpdatedEventArgs() { Score = PlayerStats.Score };
            this.ScoreUpdated?.Invoke(this, score);
        }

        private void onDeathAnimationDone(object sender, AnimationIsFinishedEventArgs e)
        {
            if (e.PlayerDeathIsOver && !this.IsGameOver)
            {
                this.player.ResetVisibilityAndMovement();
                ScoreTimer.ResetScoreTick();
                this.scoreTimer.Start();
                this.resetBoardForNoQuickRevive();
                checkToRemoveQuickReviveAbility();
            }
        }

        private void resetBoardForNoQuickRevive()
        {
            if (!PlayerAbilities.HasQuickRevive)
            {
                this.powerUpManager.ResetPowerUpsAndSpawnTimer();
                this.roadManager.ResetLanesToOneObstacle();
            }
        }

        private static void checkToRemoveQuickReviveAbility()
        {
            if (PlayerAbilities.HasQuickRevive)
            {
                PlayerAbilities.HasQuickRevive = false;
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
            if (PlayerStats.TotalLives == 0)
            {
                SoundEffectManager.PlaySound(SoundEffectType.GtaDeath);
            }
            else if (PlayerAbilities.HasQuickRevive)
            {
                SoundEffectManager.PlaySound(SoundEffectType.BeRightBack);
            }
            else if (this.HasMovedPastTopBoundary)
            {
                SoundEffectManager.PlaySound(SoundEffectType.AlmostHadIt);
            }
            else if (this.player.Y < GameBoard.MiddleRoadYLocation)
            {
                SoundEffectManager.PlaySound(SoundEffectType.MarioDrown);
            }
            else if (ScoreTimer.IsTimeUp)
            {
                SoundEffectManager.PlaySound(SoundEffectType.SadViolin);
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

    /// <summary>
    ///     Holds the event for the next level
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class NextLevelEventArgs : EventArgs
    {

        /// <summary>
        ///     Gets or sets the next level.
        /// </summary>
        /// <value>
        ///     The next level.
        /// </value>
        public int NextLevel { get; set; }
    }

}