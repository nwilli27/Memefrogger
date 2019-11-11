
using FroggerStarter.View.Sprites.RoadSprites;

namespace FroggerStarter.Model.Game_Objects.Moving_Object.RoadObstacle
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
        public Car()
        {
            Sprite = new CarSprite();
        }

        #endregion
    }
}