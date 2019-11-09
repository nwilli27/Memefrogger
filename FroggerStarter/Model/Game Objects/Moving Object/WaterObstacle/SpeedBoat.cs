using FroggerStarter.Enums;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model.Game_Objects.Moving_Object.WaterObstacle
{
    /// <summary>
    ///     A speed boat object of type Obstacle
    /// </summary>
    /// <seealso cref="WaterObstacle" />
    internal sealed class SpeedBoat : WaterObstacle
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SpeedBoat"/> class.
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        /// <param name="direction">The direction the boat is facing.</param>
        public SpeedBoat(Direction direction) : base(direction)
        {
            this.Sprite = new SpeedBoatSprite();
        }

        #endregion
    }
}
