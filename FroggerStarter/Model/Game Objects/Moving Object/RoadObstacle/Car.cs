using FroggerStarter.Enums;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model.Game_Objects.Moving_Object
{
    /// <summary>
    ///     A car object of type RoadObstacle.
    /// </summary>
    /// <seealso cref="WaterObstacle" />
    public sealed class Car : RoadObstacle
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Car" /> class.
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        /// <param name="direction">The direction the vehicle is facing.</param>
        public Car(Direction direction) : base(direction)
        {
            Sprite = new CarSprite();
            this.MoveToDefaultLocation();
        }

        #endregion
    }
}