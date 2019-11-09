using FroggerStarter.Constants;
using FroggerStarter.Enums;

namespace FroggerStarter.Model.Game_Objects.Moving_Object.WaterObstacle
{
    /// <summary>
    ///     An obstacle that is a WaterObstacle
    /// </summary>
    /// <seealso cref="WaterObstacle" />
    public class WaterObstacle : Obstacle
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WaterObstacle"/> class.
        /// </summary>
        /// <param name="direction">The direction.</param>
        public WaterObstacle(Direction direction) : base(direction) { }

        /// <summary>
        ///     Gets the type of the obstacle.
        /// </summary>
        /// <value>
        ///     The type of the obstacle.
        /// </value>
        public ObstacleType ObstacleType { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Moves the obstacle to the left.
        /// </summary>
        protected override void moveObstacleToTheLeft()
        {
            if (this.hasObstacleMovedOffLeftSide())
            {
                this.X = GameBoard.BackgroundWidth;
            }
            this.MoveLeft();
        }

        /// <summary>
        ///     Moves the obstacle to the right.
        /// </summary>
        protected override void moveObstacleToTheRight()
        {
            if (this.hasObstacleMovedOffRightSide())
            {
                this.X = -this.Width;
            }
            this.MoveRight();
        }

        #endregion
    }
}
