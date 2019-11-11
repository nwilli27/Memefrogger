
using FroggerStarter.View.Sprites.WaterSprites;

namespace FroggerStarter.Model.Game_Objects.Moving_Object.WaterObstacle.Logs
{
    /// <summary>
    ///     A small log of type Log
    /// </summary>
    /// <seealso cref="WaterObstacle" />
    internal sealed class SmallLog : WaterObstacle
    {

        /// <summary>
        ///     Initializes a new instance of the <see cref="SmallLog"/> class.
        /// </summary>
        public SmallLog()
        {
            this.Sprite = new SmallLogSprite();
        }
    }
}
