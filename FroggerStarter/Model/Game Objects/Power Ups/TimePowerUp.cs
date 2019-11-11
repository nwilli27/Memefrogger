
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
            this.Type = PowerUpType.TimeIncrease;
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
            base.Activate();
            ScoreTimer.ScoreTick += this.timeExtension;
            SoundEffectManager.PlaySound(SoundEffectType.TimePowerUp);
        }

        /// <summary>
        ///     Changes the sprite visibility based on the bool condition passed in.
        ///     Also setups the sprites ability
        /// Precondition: none
        /// Post-condition: Sprite.Visibility == (Visible || Collapsed)
        /// </summary>
        /// <param name="conditionToChangeVisibility">if set to <c>true</c> [condition to change visibility].</param>
        public override void ChangeSpriteVisibility(bool conditionToChangeVisibility)
        {
            this.setupAbility();
            base.ChangeSpriteVisibility(conditionToChangeVisibility);
        }

        #endregion

        #region Private Helpers

        private void setupAbility()
        {
            this.timeExtension = getRandomTimeExtension();
            this.timeSprite.TimeExtension = this.timeExtension.ToString();
        }

        private static int getRandomTimeExtension()
        {
            return Randomizer.GetRandomValueInRange(LowSecondRange, HighSecondRange);
        }

        #endregion
    }
}
