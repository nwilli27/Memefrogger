
using FroggerStarter.View.Sprites.RoadSprites;

namespace FroggerStarter.Model.Game_Objects.Moving_Object.RoadObstacle
{
    /// <summary>
    ///     A toad truck object of type RoadObstacle.
    /// </summary>
    /// <seealso cref="WaterObstacle" />
    public sealed class ToadTruck : RoadObstacle
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ToadTruck" /> class.
        /// </summary>
        public ToadTruck()
        {
            Sprite = new ToadTruckSprite();
        }

        #endregion
    }
}