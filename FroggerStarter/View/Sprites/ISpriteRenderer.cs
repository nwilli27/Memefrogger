namespace FroggerStarter.View.Sprites
{
    /// <summary>
    ///     Interface to render a sprite. This interface is needed within every sprite 
    ///     that is associated with a game object.
    /// </summary>
    public interface ISpriteRenderer
    {
        #region Methods

        /// <summary>
        ///     Renders the sprite at the specified location.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        void RenderAt(double x, double y);

        #endregion
    }
}
