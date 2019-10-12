using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FroggerStarter.Model
{

    /// <summary>
    ///     A row of moving obstacles of type GameObject.
    /// </summary>
    /// <seealso cref="System.Collections.IEnumerable" />
    internal class Lane : IEnumerable<Obstacle>
    {
        #region Data Members

        private readonly ICollection<Obstacle> obstacles;
        private readonly LaneDirection laneDirection;
        private readonly double laneWidth;
        private readonly double defaultSpeed;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Lane"/> class.
        ///     Precondition: none
        ///     Post-condition: this.obstacles.Count == 0
        ///                     this.laneDirection = laneDirection
        ///                     this.defaultSpeed = defaultSpeed
        /// </summary> 
        /// <param name="laneDirection">The direction the obstacles are moving in the lane</param>
        /// <param name="laneWidth">The width of the lane</param>
        /// <param name="defaultSpeed">The default speed of all obstacles</param>
        public Lane(double laneWidth, double defaultSpeed, LaneDirection laneDirection)
        {
            this.obstacles = new Collection<Obstacle>();
            this.laneDirection = laneDirection;
            this.laneWidth = laneWidth;
            this.defaultSpeed = defaultSpeed;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Adds a specified number of obstacles to the lane of a specified obstacle type.
        ///     Precondition: none
        ///     Post-condition: this.obstacles.Count == numberOfObstacles
        /// </summary>
        /// <param name="obstacleType">Type of the obstacle.</param>
        /// <param name="numberOfObstacles">The number of obstacles.</param>
        public void AddObstacles(ObstacleType obstacleType, int numberOfObstacles)
        {
            for (var i = 0; i < numberOfObstacles; i++)
            {
                this.add(new Obstacle(obstacleType));
            }
            this.readjustSpaceBetweenObstacles();
        }

        /// <summary>
        ///     Moves all obstacles according to which direction the lane is going.
        ///     If the object moves past the lane boundary, its placed back on the other end of the lane.
        ///     Precondition: none
        ///     Post-condition: @each in this.obstacles.X +- obstacle.SpeedX
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">When lane direction isn't available</exception>
        public void MoveObstacles()
        {
            foreach (var currentObstacle in this.obstacles)
            {
                switch (this.laneDirection)
                {
                    case LaneDirection.Left:

                        if (hasObstacleMovedOffLeftSide(currentObstacle))
                        {
                            currentObstacle.X = this.laneWidth;
                        }
                        currentObstacle.MoveLeft();
                        break;

                    case LaneDirection.Right:
                        if (this.hasObstacleMovedOffRightSide(currentObstacle))
                        {
                            currentObstacle.X = -currentObstacle.Width;
                        }
                        currentObstacle.MoveRight();
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <summary>
        ///     Increases the speed of all obstacles by 0.5.
        ///     Precondition: none
        ///     Post-condition: @each in this.obstacles : obstacle.SpeedX += 0.5
        /// </summary>  
        public void IncreaseSpeedOfObstacles(double speed)
        {
            foreach (var currentObstacle in this.obstacles)
            {
                currentObstacle.SpeedX += speed;
            }
        }

        /// <summary>
        ///     Sets the obstacles to default speed of the lane.
        ///     Precondition: none
        ///     Post-condition: @each obstacles in this.obstacles : obstacle.SpeedX == this.defaultSpeed
        /// </summary>
        public void SetObstaclesToDefaultSpeed()
        {
            foreach (var currentObstacle in this.obstacles)
            {
                currentObstacle.SpeedX = this.defaultSpeed;
            }
        }

        /// <summary>
        ///     Sets all obstacles to the specified yLocation and are aligned
        ///     vertically within the height of the lane.
        ///     Precondition: heightOfLane gt; 0
        ///     Post-condition: @each obstacle in this.obstacles : obstacle.Y == verticalYAlignment
        /// </summary>
        /// <param name="yLocation">The y location.</param>
        /// <param name="heightOfLane">The height of lane.</param>
        public void SetObstaclesToLaneYLocation(double yLocation, double heightOfLane)
        {
            if (heightOfLane <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            foreach (var currentObstacle in this.obstacles)
            {
                var verticalYAlignment = getCenteredYLocationOfLane(currentObstacle, yLocation, heightOfLane);
                currentObstacle.Y = verticalYAlignment;
            }
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

        private bool hasObstacleMovedOffRightSide(GameObject currentObstacle)
        {
            return currentObstacle.X + currentObstacle.SpeedX > this.laneWidth;
        }

        private static bool hasObstacleMovedOffLeftSide(GameObject currentObstacle)
        {
            return currentObstacle.X + currentObstacle.SpeedX < -currentObstacle.Width;
        }

        private static double getCenteredYLocationOfLane(GameObject obstacle, double yLocation, double heightOfLane)
        {
            return ((heightOfLane - obstacle.Height) / 2) + yLocation;
        }

        private void add(Obstacle obstacle)
        {
            obstacle.SpeedX = this.defaultSpeed;

            switch (this.laneDirection)
            {
                case LaneDirection.Left:
                    this.obstacles.Add(obstacle);
                    break;

                case LaneDirection.Right:
                    obstacle.FlipSpriteHorizontally();
                    this.obstacles.Add(obstacle);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void readjustSpaceBetweenObstacles()
        {
            var startingObstacleXPoint = 0.0;

            foreach (var currentObstacle in this.obstacles)
            {
                currentObstacle.X = startingObstacleXPoint;
                startingObstacleXPoint += this.getSpacingBetweenObstacles();
            }
        }

        private double getSpacingBetweenObstacles()
        {
            return this.laneWidth / this.obstacles.Count;
        }

        #endregion
    }
}
