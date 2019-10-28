
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     Holds the implementation for the home location for
    ///     frog to jump in.
    /// </summary>
    internal class HomeSpot : GameObject
    {

        #region Properties

        /// <summary>
        ///     Gets or sets a value indicating whether the home is filled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is filled; otherwise, <c>false</c>.
        /// </value>
        public bool IsFilled { get; set; }

        #endregion

        #region Constructors

        public HomeSpot()
        {
            Sprite = new HomeSprite();
        }

        #endregion

    }
}
