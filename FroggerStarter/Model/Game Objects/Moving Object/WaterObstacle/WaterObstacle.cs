using FroggerStarter.Constants;

namespace FroggerStarter.Model.Game_Objects.Moving_Object.WaterObstacle
{
    /// <summary>
    ///     An obstacle that is a WaterObstacle
    /// </summary>
    /// <seealso cref="WaterObstacle" />
    public class WaterObstacle : Obstacle
    {

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
