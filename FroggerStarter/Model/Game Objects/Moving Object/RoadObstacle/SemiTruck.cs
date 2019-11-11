
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model.Game_Objects.Moving_Object.RoadObstacle
{
    /// <summary>
    ///     A semi truck object of type RoadObstacle.
    /// </summary>
    /// <seealso cref="WaterObstacle" />
    public sealed class SemiTruck : RoadObstacle
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SemiTruck" /> class.
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        public SemiTruck()
        {
            Sprite = new SemiTruckSprite();
        }

        #endregion
    }
}