
using FroggerStarter.Enums;
using FroggerStarter.Model.Score;
using FroggerStarter.Model.Sound;
using FroggerStarter.Utility;
using TimePowerUpSprite = FroggerStarter.View.Sprites.PowerUpSprites.TimePowerUpSprite;

namespace FroggerStarter.Model.Game_Objects.Power_Ups
{
    /// <summary>
    ///     A special Power up that gains the user extra time
    /// </summary>
    /// <seealso cref="PowerUp" />
    internal sealed class TimePowerUp : PowerUp
    {
        #region Data Members

        private readonly TimePowerUpSprite timeSprite;
        private int timeExtension;

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
            this.timeSprite = new TimePowerUpSprite();
            this.Sprite = this.timeSprite;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Adds a random score extension between [3-9]
        ///     seconds to the current ongoing ScoreTick.
        ///     Precondition: none
        ///     Post-condition: ScoreTick += timeExtension
        /// </summary>
        public override void Activate()
        {
            ScoreTimer.ScoreTick += this.timeExtension;
            SoundEffectManager.PlaySound(SoundEffectType.TimePowerUp);
        }

        /// <summary>
        ///     Setups the the power up by assigning a random time extension
        ///     and binding the TimeExtension property to the Sprite text block.
        ///     Precondition: none
        ///     Post-condition: this.TimeExtension = random [3-9]
        /// </summary>
        public override void SetupAbility()
        {
            this.timeExtension = getRandomTimeExtension();
            this.timeSprite.TimeExtension = this.timeExtension.ToString();
        }

        #endregion

        #region Private Helpers

        private static int getRandomTimeExtension()
        {
            return Randomizer.GetRandomValueInRange(LowSecondRange, HighSecondRange);
        }

        #endregion
    }
}
