

using System;
using System.Collections;
using System.Collections.Generic;
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
            this.powerUpSpawnTimer.Interval = new TimeSpan(0, 0, 0, getRandomSpawnTimeInterval(), 0);
            this.powerUpSpawnTimer.Start();
        }

        private void onSpawnPowerUpTick(object sender, object e)
        {
            var randomValueRangeOfListSize = Randomizer.getRandomValueInRange(0, this.powerUps.Count);
            var randomPowerUp = this.powerUps[randomValueRangeOfListSize];
            randomPowerUp.ChangeSpriteVisibility(true);
            randomPowerUp.setRandomLocationOnBoard();
            this.powerUpSpawnTimer.Stop();
        }

        private static int getRandomSpawnTimeInterval()
        {
            return Randomizer.getRandomValueInRange(7, (int) GameSettings.ScoreTime);
        }

        private void createPowerUps()
        {
            foreach (PowerUpType powerUpType in Enum.GetValues(typeof(PowerUpType)))
            {
                this.powerUps.Add(PowerUpFactory.CreatePowerUp(powerUpType));
            }
        }

        #endregion

    }
}
