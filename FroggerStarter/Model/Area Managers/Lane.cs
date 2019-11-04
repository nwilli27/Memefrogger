using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FroggerStarter.Constants;
using FroggerStarter.Enums;
using FroggerStarter.Factory;
using FroggerStarter.Model.Game_Objects;
using FroggerStarter.Model.Game_Objects.Moving_Object;

namespace FroggerStarter.Model.Area_Managers
{

    /// <summary>
    ///     A row of moving obstacles of type GameObject.
    /// </summary>
    /// <seealso cref="System.Collections.IEnumerable" />
    public abstract class Lane : IEnumerable<Obstacle>
    {

        #region Data Members

        private readonly IList<Obstacle> obstacles;
        private readonly Direction direction;
        private readonly double defaultSpeed;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Lane"/> class.
        ///     Precondition: none
        ///     Post-condition: this.obstacles.Count == 0
        ///                     this.direction = direction
        ///                     this.defaultSpeed = defaultSpeed
        /// </summary> 
        /// <param name="direction">The direction the obstacles are moving in the lane</param>
        /// <param name="defaultSpeed">The default speed of all obstacles</param>
        protected Lane(double defaultSpeed, Direction direction)
        {
            this.obstacles = new List<Obstacle>();
            this.direction = direction;
            this.defaultSpeed = defaultSpeed;
        }

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
        public void AddObstacles(ObstacleType obstacleType, int maxNumberObstacles)
        {
            for (var i = 0; i < maxNumberObstacles; i++)
            {
                this.add(ObstacleFactory.CreateObstacle(obstacleType, this.direction));
            }
            
            this.MakeObstacleActive();
            this.moveFirstObstacleStartLocationForward();
        }

        /// <summary>
        ///     Resets the lane to one obstacle.
        ///     Precondition: none
        ///     Post-condition: @each obstacle.isActive = false
        ///                     @first obstacle.isActive = true
        /// </summary>
        public void ResetLaneToOneObstacle()
        {
            var firstActiveObstacle = this.obstacles.ToList().First(obstacle => obstacle.IsActive);
            var allButOneObstacle = this.obstacles.ToList().Where(obstacle => !obstacle.Equals(firstActiveObstacle));
            foreach (var obstacle in allButOneObstacle)
            {
                obstacle.MoveToDefaultLocation();
                obstacle.IsActive = false;
            }
        }

        /// <summary>
        ///     Moves all obstacles according to which direction the lane is going.
        ///     If the object moves past the lane boundary, its placed back on the other end of the lane.
        ///     Precondition: none
        ///     Post-condition: @each in this.obstacles.X +/- obstacle.SpeedX
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">When lane direction isn't available</exception>
        public void MoveObstacles()
        {
            var activeObstacles = this.obstacles.Where(obstacle => obstacle.IsActive).ToList();
            var collidedList = new List<Obstacle>();

            foreach (var obstacle in activeObstacles)
            {   
                var firstCollision = activeObstacles.FirstOrDefault(currentObstacle => obstacle.HasCollidedWith(currentObstacle) && !currentObstacle.Equals(obstacle));
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
        ///     Makes the next available obstacle active.
        ///     Precondition: none
        ///     Post-condition: @obstacles.Count() that isActive += 1
        /// </summary>
        public void MakeObstacleActive()
        {
            var nextObstacle = this.obstacles.FirstOrDefault(obstacle => !obstacle.IsActive);
            if (nextObstacle != null)
            {
                nextObstacle.IsActive = true;
            }
        }

        /// <summary>
        ///     Sets all obstacles to the specified yLocation and are aligned
        ///     vertically within the height of the lane.
        ///     Precondition: none
        ///     Post-condition: @each obstacle in this.obstacles : obstacle.Y == verticalYAlignment
        /// </summary>
        /// <param name="yLocation">The y location.</param>
        /// <param name="heightOfLane">The height of lane.</param>
        public void SetObstaclesToLaneYLocation(double yLocation, double heightOfLane)
        {
            this.obstacles.ToList().ForEach(obstacle => 
                obstacle.Y = getCenteredYLocationOfLane(obstacle, yLocation, heightOfLane));
        }

        /// <summary>
        ///     Returns an enumerator that iterates through the collection of game objects.
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator"></see> object that can 
        ///     be used to iterate through the collection of game objects.
        /// </returns>
        public IEnumerator GetEnumerator()
        {
            return this.obstacles.GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        /// <returns>
        ///     An enumerator that can be used to iterate through the collection.
        /// </returns>
        IEnumerator<Obstacle> IEnumerable<Obstacle>.GetEnumerator()
        {
            return this.obstacles.GetEnumerator();
        }

        #endregion

        #region Private Helpers

        private void moveFirstObstacleStartLocationForward()
        {
            var first = this.obstacles.First(obstacle => obstacle.IsActive);
            first.ShiftXForward(GameBoard.BackgroundWidth / this.obstacles.Count);
        }

        private static double getCenteredYLocationOfLane(GameObject obstacle, double yLocation, double heightOfLane)
        {
            return ((heightOfLane - obstacle.Height) / 2) + yLocation;
        }

        private void add(Obstacle obstacle)
        {
            obstacle.SpeedX = this.defaultSpeed;
            this.obstacles.Add(obstacle);
        }

        #endregion
    }
}
