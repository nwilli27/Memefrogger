
using FroggerStarter.View.Sprites.WaterSprites;

namespace FroggerStarter.Model.Game_Objects.Moving_Object.WaterObstacle.Logs
{

    /// <summary>
    ///     A Medium log that is of type WaterObstacle
    /// </summary>
    /// <seealso cref="WaterObstacle" />
    internal sealed class MediumLog : WaterObstacle
    {

        /// <summary>
        ///     Initializes a new instance of the <see cref="MediumLog"/> class.
        /// </summary>
        public MediumLog()
        {
            this.Sprite = new MediumLogSprite();
        }
    }
}
