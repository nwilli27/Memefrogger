
using FroggerStarter.Enums;
using FroggerStarter.Model.Player;
using FroggerStarter.Model.Sound;
using FroggerStarter.View.Sprites.PowerUpSprites;

namespace FroggerStarter.Model.Game_Objects.Power_Ups
{
    /// <summary>
    ///     A special Power up that gains the user extra time
    /// </summary>
    /// <seealso cref="PowerUp" />
    internal sealed class QuickRevivePowerUp : PowerUp
    {

        #region Properties

        /// <summary>
        ///     Gets a value indicating whether this instance has been activated maximum number times.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has been activated maximum number times; otherwise, <c>false</c>.
        /// </value>
        public override bool HasBeenActivatedMaxNumberTimes => this.NumberOfTimesActivated == MaxNumberOfActivates;

        #endregion

        #region Constants

        private const int MaxNumberOfActivates = 3;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="QuickRevivePowerUp"/> class.
        ///     Precondition: none
        ///     Post-condition: Sprite == QuickRevivePowerUpSprite
        /// </summary>
        public QuickRevivePowerUp()
        {
            this.Sprite = new QuickRevivePowerUpSprite();
            this.Type = PowerUpType.QuickRevive;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Adds quick revive heart to lives.
        ///     Plays quick revive sound.
        ///     Precondition: none
        ///     Post-condition: QuickReviveHeart.Sprite = Visible
        ///                     PlayerAbilities.HasQuickRevive = true
        /// </summary>
        public override void Activate()
        {
            base.Activate();
            PlayerStats.Lives.MoveQuickReviveHeart();
            PlayerAbilities.HasQuickRevive = true;
            SoundEffectManager.PlaySound(SoundEffectType.QuickRevive);
        }

        #endregion
    }
}
