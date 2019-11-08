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
    /// <seealso cref="IEnumerable" />
    public abstract class Lane : IEnumerable<Obstacle>
    {

        #region Data Members

        /// <summary>
        /// The obstacles
        /// </summary>
        protected IList<Obstacle> obstacles;

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
        public Lane(double defaultSpeed, Direction direction)
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
        public virtual void AddObstacles(ObstacleType obstacleType, int maxNumberObstacles)
        {
            for (var i = 0; i < maxNumberObstacles; i++)
            {
                this.add(ObstacleFactory.CreateObstacle(obstacleType, this.direction));
            }
        }

        /// <summary>
        ///     Moves all obstacles according to which direction the lane is going.
        ///     If the object moves past the lane boundary, its placed back on the other end of the lane.
        ///     Precondition: none
        ///     Post-condition: @each in this.obstacles.X +/- obstacle.SpeedX
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">When lane direction isn't available</exception>
        public abstract void MoveObstacles();

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
                obstacle.SetCenteredYLocationOfArea(heightOfLane, yLocation));
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

        private void add(Obstacle obstacle)
        {
            obstacle.SpeedX = this.defaultSpeed;
            this.obstacles.Add(obstacle);
        }

        #endregion
    }
}
