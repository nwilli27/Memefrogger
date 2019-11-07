using FroggerStarter.Enums;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model.Game_Objects.Moving_Object
{
    /// <summary>
    ///     A semi truck object of type RoadObstacle.
    /// </summary>
    /// <seealso cref="WaterObstacle" />
    public sealed class SemiTruck : RoadObstacle
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SemiTruck" /> class.
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        /// <param name="direction">The direction the vehicle is facing.</param>
        public SemiTruck(Direction direction) : base(direction)
        {
            Sprite = new SemiTruckSprite();
            this.MoveToDefaultLocation();
        }

        #endregion
    }
}