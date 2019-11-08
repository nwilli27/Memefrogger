using FroggerStarter.Enums;
using FroggerStarter.Factory;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model.Game_Objects.Moving_Object.WaterObstacle.Logs
{
    /// <summary>
    ///     A small log of type Log
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.Game_Objects.Moving_Object.WaterObstacle.Log" />
    class SmallLog : Log
    {

        /// <summary>
        ///     Initializes a new instance of the <see cref="SmallLog"/> class.
        /// </summary>
        /// <param name="direction">The direction.</param>
        public SmallLog(Direction direction) : base(direction)
        {
            Sprite = new SmallLogSprite();
        }
    }
}
