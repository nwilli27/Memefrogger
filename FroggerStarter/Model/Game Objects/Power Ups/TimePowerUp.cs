
using System;
using FroggerStarter.Utility;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model.Game_Objects.Power_Ups
{
    /// <summary>
    ///     A special Power up that gains the user extra time
    /// </summary>
    /// <seealso cref="PowerUp" />
    internal sealed class TimePowerUp : PowerUp
    {
        #region Data Members

        private bool hasCollided;

        #endregion

        #region Constants

        private const int LowSecondRange = 3;
        private const int HighSecondRange = 9;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TimePowerUp"/> class.
        ///     Precondition: none
        ///     Post-condition: Sprite == TimePowerUpSprite
        /// </summary>
        public TimePowerUp()
        {
            this.Sprite = new TimePowerUpSprite();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Adds a random score extension between [3-9]
        ///     seconds to the current ongoing ScoreTick.
        ///     Precondition: none
        ///     Post-condition: ScoreTick += timeExtension
        /// </summary>
        public override void activate()
        {
            if (!this.hasCollided)
            {
                var timeExtension = getRandomTimeExtension();
                ScoreTimer.ScoreTick += timeExtension;
                this.ChangeSpriteVisibility(false);
                this.hasCollided = true;
            }
        }

        #endregion

        #region Private Helpers

        private static int getRandomTimeExtension()
        {
            return Randomizer.getRandomValueInRange(LowSecondRange, HighSecondRange);
        }

        #endregion
    }


}
