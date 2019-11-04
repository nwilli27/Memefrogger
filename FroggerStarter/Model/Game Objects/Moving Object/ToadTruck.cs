using FroggerStarter.Enums;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model.Game_Objects.Moving_Object
{
    /// <summary>
    ///     A toad truck object of type Obstacle.
    /// </summary>
    /// <seealso cref="Obstacle" />
    public sealed class ToadTruck : Obstacle
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ToadTruck" /> class.
        /// </summary>
        /// <param name="direction">The direction the vehicle is facing.</param>
        public ToadTruck(Direction direction) : base(direction)
        {
            Sprite = new ToadTruckSprite();
            this.MoveToDefaultLocation();
        }

        #endregion
    }
}