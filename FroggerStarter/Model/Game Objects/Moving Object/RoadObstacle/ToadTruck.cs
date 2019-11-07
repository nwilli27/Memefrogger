using FroggerStarter.Enums;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model.Game_Objects.Moving_Object
{
    /// <summary>
    ///     A toad truck object of type RoadObstacle.
    /// </summary>
    /// <seealso cref="WaterObstacle" />
    public sealed class ToadTruck : RoadObstacle
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