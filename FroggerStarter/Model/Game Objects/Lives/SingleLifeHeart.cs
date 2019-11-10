
using FroggerStarter.Enums;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model.Game_Objects.Lives
{
    /// <summary>
    ///     A class to hold a single life
    /// </summary>
    internal sealed class SingleLifeHeart : LifeHeart
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SingleLifeHeart"/> class.
        /// </summary>
        public SingleLifeHeart()
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
