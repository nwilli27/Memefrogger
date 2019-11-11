
using FroggerStarter.View.Sprites.WaterSprites;

namespace FroggerStarter.Model.Game_Objects.Moving_Object.WaterObstacle.Logs
{
    /// <summary>
    ///     A large log that is of type WaterObstacle
    /// </summary>
    /// <seealso cref="WaterObstacle" />
    internal sealed class LargeLog : WaterObstacle
    {

        /// <summary>
        ///     Initializes a new instance of the <see cref="LargeLog"/> class.
        /// </summary>
        public LargeLog()
        {
            this.Sprite = new LargeLogSprite();
        }
    }
}
