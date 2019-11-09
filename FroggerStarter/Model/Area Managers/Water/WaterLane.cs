
using FroggerStarter.Constants;
using FroggerStarter.Enums;
using System.Linq;

namespace FroggerStarter.Model.Area_Managers.Water
{
    /// <summary>
    ///     A Waterlane that is of type Lanea
    /// </summary>
    /// <seealso cref="Lane" />
    internal class WaterLane : Lane
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="WaterLane"/> class.
        /// </summary>
        /// <param name="defaultSpeed">The default speed of all obstacles</param>
        /// <param name="direction">The direction the obstacles are moving in the lane</param>
        public WaterLane(double defaultSpeed, Direction direction) : base(defaultSpeed, direction) {}

        #endregion

        #region Methods

        /// <summary>
        ///     Adds a specified number of obstacles to the lane of a specified obstacle type, then
        ///     readjusts their X coordinate spacing accordingly.
        ///     Precondition: none
        ///     Post-condition: this.obstacles.Count == numberOfObstacles
        ///                     @each obstacle.X is spaced accordingly on lane
        /// </summary>
        /// <param name="obstacleType">Type of the obstacle.</param>
        /// <param name="maxNumberObstacles">The max number of obstacles.</param>
        public override void AddObstacles(ObstacleType obstacleType, int maxNumberObstacles)
        {
            base.AddObstacles(obstacleType, maxNumberObstacles);

            this.readjustSpaceBetweenObstacles();
        }

        /// <summary>
        ///     Moves all obstacles according to which direction the lane is going.
        ///     If the object moves past the lane boundary, its placed back on the other end of the lane.
        ///     Precondition: none
        ///     Post-condition: @each in this.obstacles.X +/- obstacle.SpeedX
        /// </summary>
        public override void MoveObstacles()
        {
            this.obstacles.ToList().ForEach(obstacle => obstacle.MoveForward());
        }

        #endregion

        #region Private Helpers

        private void readjustSpaceBetweenObstacles()
        {
            var obstacleXLocation = 0.0;

            foreach (var currentObstacle in this.obstacles)
            {
                currentObstacle.X = obstacleXLocation;
                obstacleXLocation += this.getSpacingBetweenVehicles();
            }
        }

        private double getSpacingBetweenVehicles()
        {
            return (GameBoard.BackgroundWidth / this.obstacles.Count) + (this.getAverageWidth() / 2);
        }

        private double getAverageWidth()
        {
            return this.obstacles.Average(obstacle => obstacle.Width);
        }

        #endregion
    }
}
