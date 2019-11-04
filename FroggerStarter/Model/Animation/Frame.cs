using FroggerStarter.Model.Game_Objects;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model.Animation
{
    /// <summary>
    ///     Holds the implementation for a single frame.
    /// </summary>
    /// <seealso cref="GameObject" />
    public sealed class Frame : GameObject
    {
        #region Data Members

        private bool isVisible;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is visible; otherwise, <c>false</c>.
        /// </value>
        public bool IsVisible
        {
            get => this.isVisible;
            set
            {
                if (value)
                {
                    this.HasBeenPlayed = true;
                }
                this.isVisible = value;
                this.ChangeSpriteVisibility(value);
            }
        }

        /// <summary>
        ///     Gets a value indicating whether this instance has been played.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has been played; otherwise, <c>false</c>.
        /// </value>
        public bool HasBeenPlayed { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Frame"/> class.
        /// </summary>
        /// <param name="sprite">The sprite.</param>
        public Frame(BaseSprite sprite)
        {
            this.Sprite = sprite;
            this.IsVisible = false;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Resets to invisible and the HasBeenPlayed is false.
        ///     Precondition: none
        ///     Post-condition: this.isVisible = false
        ///                     this.HasBeenPlayed = false
        /// </summary>
        public void ResetStatusAndVisibility()
        {
            this.isVisible = false;
            this.HasBeenPlayed = false;
        }

        #endregion
    }
}
