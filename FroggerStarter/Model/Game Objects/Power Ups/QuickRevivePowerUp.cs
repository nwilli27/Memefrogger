
using System.Linq;
using FroggerStarter.Enums;
using FroggerStarter.Model.Game_Objects.Lives;
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

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="QuickRevivePowerUp"/> class.
        ///     Precondition: none
        ///     Post-condition: Sprite == QuickRevivePowerUpSprite
        /// </summary>
        public QuickRevivePowerUp()
        {
            this.Sprite = new QuickRevivePowerUpSprite();
        }

        #endregion

        #region Methods

        public void Activate(PlayerLives playerLives)
        {
            playerLives.MoveQuickReviveHeart();
            PlayerAbilities.HasQuickRevive = true;
            playerLives.First(heart => heart is QuickReviveHeart).ChangeSpriteVisibility(true);
            SoundEffectManager.PlaySound(SoundEffectType.QuickRevive);
        }

        public override void SetupAbility()
        {
            
        }

        public override void Activate()
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Private Helpers



        #endregion
    }
}
