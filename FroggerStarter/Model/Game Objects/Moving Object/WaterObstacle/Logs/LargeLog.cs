using FroggerStarter.Enums;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model.Game_Objects.Moving_Object.WaterObstacle.Logs
{
    /// <summary>
    ///     A large log that is of type Log
    /// </summary>
    /// <seealso cref="Log" />
    class LargeLog : Log
    {

        /// <summary>
        ///     Initializes a new instance of the <see cref="LargeLog"/> class.
        /// </summary>
        /// <param name="direction">The direction.</param>
        public LargeLog(Direction direction) : base(direction)
        {
            Sprite = new LargeLogSprite();
        }
    }
}
