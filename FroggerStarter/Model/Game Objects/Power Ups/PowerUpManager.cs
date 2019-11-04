using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using FroggerStarter.Constants;
using FroggerStarter.Enums;
using FroggerStarter.Factory;
using FroggerStarter.Utility;

namespace FroggerStarter.Model.Game_Objects.Power_Ups
{
    /// <summary>
    ///     A Class that manages a collection of Power Ups
    /// </summary>
    internal class PowerUpManager : IEnumerable<PowerUp>
    {

        #region Data Members

        private readonly IList<PowerUp> powerUps;
        private DispatcherTimer powerUpSpawnTimer;

        #endregion

        #region Constants

        private const int EndingSpawnTimeRange = (int) GameSettings.ScoreTime - 10;
        private const int StartingSpawnTimeRange = 3;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PowerUpManager"/> class.
        ///     Precondition: none
        ///     Post-condition: this.powerUps.Count() == 0
        /// </summary>
        public PowerUpManager()
        {
            this.powerUps = new List<PowerUp>();
            this.setupPowerUpSpawnTimer();
            this.createPowerUps();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Resets the power up spawn timer and moves all current
        ///     power ups off the board and makes them invisible.
        ///     Precondition: none
        ///     Post-condition: @each powerUp.Visibility = Collapsed
        ///                           powerUp.X = -powerUp.Width
        /// </summary>
        public void ResetPowerUpsAndSpawnTimer()
        {
            this.resetRandomTickSpeedAndStart();
            this.powerUps.ToList().ForEach(powerUp => powerUp.MoveOffBoardAndMakeInvisible());
        }

        /// <summary>
        ///     Stops the power up spawn timer.
        ///     Precondition: none
        ///     Post-condition: powerUpSpawnTimer.Stop()
        /// </summary>
        public void StopPowerUpSpawnTimer()
        {
            this.powerUpSpawnTimer.Stop();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<PowerUp> GetEnumerator()
        {
            return this.powerUps.GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region Private Helpers

        private void setupPowerUpSpawnTimer()
        {
            this.powerUpSpawnTimer = new DispatcherTimer();
            this.powerUpSpawnTimer.Tick += this.onSpawnPowerUpTick;
            this.resetRandomTickSpeedAndStart();
        }

        private void resetRandomTickSpeedAndStart()
        {
            this.powerUpSpawnTimer.Interval = new TimeSpan(0, 0, 0, getRandomSpawnTimeInterval(), 0);
            this.powerUpSpawnTimer.Start();
        }

        private void onSpawnPowerUpTick(object sender, object e)
        {
            var randomValueRangeOfListSize = Randomizer.GetRandomValueInRange(0, this.powerUps.Count);
            var randomPowerUp = this.powerUps[randomValueRangeOfListSize];

            randomPowerUp.SetLocationAndMakeVisible();
            this.powerUpSpawnTimer.Stop();
        }

        private static int getRandomSpawnTimeInterval()
        {
            return Randomizer.GetRandomValueInRange(StartingSpawnTimeRange, EndingSpawnTimeRange);
        }

        private void createPowerUps()
        {
            foreach (PowerUpType powerUpType in Enum.GetValues(typeof(PowerUpType)))
            {
                var powerUp = PowerUpFactory.CreatePowerUp(powerUpType);
                powerUp.ChangeSpriteVisibility(false);
                this.powerUps.Add(powerUp);
            }
        }

        #endregion

    }
}
