using FroggerStarter.Enums;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model.Game_Objects.Moving_Object.WaterObstacle.Logs
{
    /// <summary>
    ///     A Medium log that is of type Log
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.Game_Objects.Moving_Object.WaterObstacle.Log" />
    class MediumLog : Log
    {

        /// <summary>
        ///     Initializes a new instance of the <see cref="MediumLog"/> class.
        /// </summary>
        /// <param name="direction">The direction.</param>
        public MediumLog(Direction direction) : base(direction)
        {
            Sprite = new MediumLogSprite();
        }
    }
}
