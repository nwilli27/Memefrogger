using FroggerStarter.Constants;
using FroggerStarter.Enums;
using FroggerStarter.Model.Game_Objects.Moving_Object;
using System.Collections.Generic;
using System.Linq;

namespace FroggerStarter.Model.Area_Managers.Road
{
    /// <summary>
    ///     Defines the object for a RoadLane based on the Lane.
    /// </summary>
    /// <seealso cref="Lane" />
    public class RoadLane : Lane
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="RoadLane" /> class.
        ///     Precondition: none
        ///     Post-condition: this.obstacles.Count == 0
        ///                     this.direction = direction
        ///                     this.defaultSpeed = defaultSpeed
        /// </summary>
        /// <param name="defaultSpeed">The default speed of all obstacles</param>
        /// <param name="direction">The direction the obstacles are moving in the lane</param>
        public RoadLane(double defaultSpeed, Direction direction) : base(defaultSpeed, direction) {}

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

            this.MakeObstacleActive();
            this.moveFirstObstacleStartLocationForward();
        }

        /// <summary>
        ///     Makes the next available obstacle active.
        ///     Precondition: none
        ///     Post-condition: @obstacles.Count() that isActive += 1
        /// </summary>
        public void MakeObstacleActive()
        {
            var nextObstacle = this.obstacles.FirstOrDefault(obstacle => !(obstacle as RoadObstacle).IsActive);
            if (nextObstacle != null)
            {
                (nextObstacle as RoadObstacle).IsActive = true;
            }
        }

        /// <summary>
        ///     Moves all obstacles according to which direction the lane is going.
        ///     If the object moves past the lane boundary, its placed back on the other end of the lane.
        ///     Prevents collision with other obstacles by readjusting them off screen.
        ///     Precondition: none
        ///     Post-condition: @each in this.obstacles.X +/- obstacle.SpeedX
        /// </summary>
        public override void MoveObstacles()
        {
            var activeObstacles = this.obstacles.Where(obstacle => (obstacle as RoadObstacle).IsActive).ToList();
            var collidedList = new List<Obstacle>();

            foreach (var obstacle in activeObstacles)
            {
                var firstCollision = this.obstacles.FirstOrDefault(currentObstacle => obstacle.HasCollidedWith(currentObstacle) && !currentObstacle.Equals(obstacle));
                if (firstCollision != null)
                {
                    collidedList.Add(firstCollision);
                }
                obstacle.MoveForward();
            }

            var firstObstacleThatCollidedOffBounds = collidedList.FirstOrDefault(obstacle => obstacle.IsOutOfBounds());
            firstObstacleThatCollidedOffBounds?.ShiftXBackwardsByWidth();
        }

        /// <summary>
        ///     Resets the lane to one obstacle.
        ///     Precondition: none
        ///     Post-condition: @each obstacle.isActive = false
        ///                     @first obstacle.isActive = true
        /// </summary>
        public void ResetLaneToOneObstacle()
        {
            var firstActiveObstacle = this.getFirstActiveObstacle();
            var allButOneObstacle = this.obstacles.ToList().Where(obstacle => !obstacle.Equals(firstActiveObstacle));
            foreach (RoadObstacle obstacle in allButOneObstacle)
            {
                obstacle.MoveToDefaultLocation();
                obstacle.IsActive = false;
            }
        }

        #endregion

        #region Private Helpers

        private void moveFirstObstacleStartLocationForward()
        {
            this.getFirstActiveObstacle().ShiftXForward(GameBoard.BackgroundWidth / this.obstacles.Count);
        }

        private Obstacle getFirstActiveObstacle()
        {
            return this.obstacles.First(obstacle => (obstacle as RoadObstacle).IsActive);
        }

        #endregion
    }
}