
using FroggerStarter.Enums;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model.Game_Objects.Lives
{
    /// <summary>
    ///     A class to hold a single life
    /// </summary>
    internal sealed class LifeHeart : GameObject
    {

        #region Properties

        /// <summary>
        ///     The heart lost animation
        /// </summary>
        public Animation.Animation HeartLostAnimation;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="LifeHeart"/> class.
        /// </summary>
        public LifeHeart()
        {
            this.Sprite = new LifeSprite();
            this.HeartLostAnimation = new Animation.Animation(AnimationType.LifeHeartLost)
            {
                AnimationInterval = 175
            };
        }

        #endregion
    }
}
