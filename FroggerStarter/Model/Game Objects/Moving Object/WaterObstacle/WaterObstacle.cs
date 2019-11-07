using FroggerStarter.Enums;

namespace FroggerStarter.Model.Game_Objects.Moving_Object.WaterObstacle
{
    /// <summary>
    ///     An obstacle that is a WaterObstacle
    /// </summary>
    /// <seealso cref="WaterObstacle" />
    public class WaterObstacle : Obstacle
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WaterObstacle"/> class.
        /// </summary>
        /// <param name="direction">The direction.</param>
        public WaterObstacle(Direction direction) : base(direction) { }

        #endregion
    }
}
