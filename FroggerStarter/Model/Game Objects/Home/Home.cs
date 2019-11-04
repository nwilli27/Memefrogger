using FroggerStarter.Constants;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model.Game_Objects.Home
{
    /// <summary>
    ///     Holds the implementation for the home location for
    ///     frog to jump in.
    /// </summary>
    internal sealed class Home : GameObject
    {

        #region Data Members

        private bool isFilled;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets a value indicating whether the home is filled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is filled; otherwise, <c>false</c>.
        /// </value>
        public bool IsFilled
        {
            get => this.isFilled;
            set
            {
                this.isFilled = value;
                this.ChangeSpriteVisibility(value);
            }}

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Home"/> class.
        ///     Precondition: none
        ///     Post-condition: Sprite != null
        /// </summary>
        public Home()
        {
            Sprite = new HomeSprite();
            this.IsFilled = false;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Aligns the sprite in the center of the allocated home area.
        ///     Precondition: none
        ///     Post-condition: this.X == center of GameBoard.HomeWidth
        /// </summary>
        public void alignInCenterOfHomeLocation()
        {
            this.X = ((GameBoard.HomeWidth - this.Width) / 2) + this.X;
        }

        #endregion

    }
}
