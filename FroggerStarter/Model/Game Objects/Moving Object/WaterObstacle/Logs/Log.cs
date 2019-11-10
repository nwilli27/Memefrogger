using FroggerStarter.Enums;

namespace FroggerStarter.Model.Game_Objects.Moving_Object.WaterObstacle
{
    /// <summary>
    ///     A Log that is a type of water obstacle
    /// </summary>
    /// <seealso cref="WaterObstacle" />
    internal abstract class Log : WaterObstacle
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Log"/> class.
        /// </summary>
        /// <param name="direction">The direction.</param>
        protected Log(Direction direction) : base(direction) {}
    }
}
