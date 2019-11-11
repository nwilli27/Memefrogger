

namespace FroggerStarter.Model.Game_Objects.Lives
{
    /// <summary>
    ///     A class to hold a single life
    /// </summary>
    internal abstract class LifeHeart : GameObject
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the heart lost animation.
        /// </summary>
        /// <value>
        ///     The heart lost animation.
        /// </value>
        public Animation.Animation HeartLostAnimation { get; protected set; }

        #endregion

    }
}
