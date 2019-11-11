
using FroggerStarter.View.Sprites.RoadSprites;

namespace FroggerStarter.Model.Game_Objects.Moving_Object.RoadObstacle
{
    /// <summary>
    ///     A Turbo Car object of type RoadObstacle.
    /// </summary>
    /// <seealso cref="WaterObstacle" />
    public sealed class TurboCar : Car
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TurboCar" /> class.
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        public TurboCar()
        {
            Sprite = new TurboCarSprite();
        }

        #endregion
    }
}