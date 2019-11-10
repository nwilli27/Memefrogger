
using FroggerStarter.Enums;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model.Game_Objects.Lives
{
    /// <summary>
    ///     A class to hold a single life
    /// </summary>
    internal sealed class QuickReviveHeart : LifeHeart
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="QuickReviveHeart"/> class.
        /// </summary>
        public QuickReviveHeart()
        {
            this.Sprite = new QuickReviveHeartSprite();
            this.HeartLostAnimation = new Animation.Animation(AnimationType.LifeHeartLost)
            {
                AnimationInterval = 175
            };
        }

        #endregion
    }
}
