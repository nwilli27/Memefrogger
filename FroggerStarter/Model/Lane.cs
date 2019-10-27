using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Windows.ApplicationModel.Calls.Background;
using Windows.UI.Xaml;

namespace FroggerStarter.Model
{

    /// <summary>
    ///     A row of moving obstacles of type GameObject.
    /// </summary>
    /// <seealso cref="System.Collections.IEnumerable" />
    internal class Lane : IEnumerable<Obstacle>
    {
        #region Properties



        #endregion

        #region Data Members

        private readonly IList<Obstacle> obstacles;
        private readonly Direction direction;
        private readonly double horizontalWidth;
        private readonly double defaultSpeed;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Lane"/> class.
        ///     Precondition: none
        ///     Post-condition: this.obstacles.Count == 0
        ///                     this.horizontalWidth = horizontalWidth
        ///                     this.direction = direction
        ///                     this.defaultSpeed = defaultSpeed
        /// </summary> 
        /// <param name="direction">The direction the obstacles are moving in the lane</param>
        /// <param name="horizontalWidth">The horizontalWidth of the lane</param>
        /// <param name="defaultSpeed">The default speed of all obstacles</param>
        public Lane(double horizontalWidth, double defaultSpeed, Direction direction)
        {
            this.obstacles = new List<Obstacle>();
            this.direction = direction;
            this.horizontalWidth = horizontalWidth;
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
        /// <param name="numberOfObstacles">The number of obstacles.</param>
        public void AddObstacles(ObstacleType obstacleType, int maxNumberObstacles)
        {
            for (var i = 0; i < maxNumberObstacles; i++)
            {
                if (i == 0)
                {
                    var obstacle = new Obstacle(obstacleType, this.direction) {IsMoving = true};
                    this.add(obstacle);
                }
                else
                {
                    this.add(new Obstacle(obstacleType, this.direction));
                }
            }

            //this.readjustSpaceBetweenObstacles();
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
            foreach (var currentObstacle in this.obstacles)
            {
                if (currentObstacle.IsMoving)
                {
                    currentObstacle.MoveForward(this.horizontalWidth);
                }
            }

            //this.obstacles.ToList().ForEach(obstacle => obstacle.MoveForward(this.horizontalWidth));
        }

        /// <summary>
        ///     Sets the obstacles to default speed of the lane.
        ///     Precondition: none
        ///     Post-condition: @each obstacles in this.obstacles : obstacle.SpeedX == this.defaultSpeed
        /// </summary>
        public void SetObstaclesToDefaultSpeed()
        {
            this.obstacles.ToList().ForEach(obstacle => obstacle.SpeedX = this.defaultSpeed);
        }

        public void MoveNextAvailableObstacle()
        {
            var nextObstacle = this.obstacles.FirstOrDefault(obstacle => !obstacle.IsMoving);
            if (nextObstacle != null)
            {
                this.adjustNextObstacleXLocation(nextObstacle);
                nextObstacle.IsMoving = true;
            }
        }

        private void adjustNextObstacleXLocation(GameObject nextObstacle)
        {
            while (this.checkForEqualSpacingBetweenObstacles(nextObstacle))
            {
                this.adjustXSpacingForObstacle(nextObstacle, nextObstacle.Width);
            }
        }

        private bool checkForEqualSpacingBetweenObstacles(GameObject nextObstacle)
        {
            var obstacleTopRange = this.getRangeBasedOnObstacleSize(nextObstacle).TopRange;
            var obstacleBottomRange = this.getRangeBasedOnObstacleSize(nextObstacle).BottomRange;

            var movingObstacles = this.obstacles.Where(obstacle => obstacle.IsMoving);
            var movingObstaclesInvertedXLocations = movingObstacles.Select(this.getObstacleInvertedLaneLocation);

            
            var obstacleSpacing = movingObstaclesInvertedXLocations.Where(xLocation => xLocation >= obstacleBottomRange && xLocation <= obstacleTopRange);

            return obstacleSpacing.Any();
        }

        private (double TopRange, double BottomRange) getRangeBasedOnObstacleSize(GameObject obstacle)
        {
            var obstacleTopRange =
                obstacle.Width >= 70 ? obstacle.X + obstacle.Width : obstacle.X + obstacle.Width * 2;

            var obstacleBottomRange =
                obstacle.Width >= 70 ? obstacle.X - obstacle.Width : obstacle.X - obstacle.Width * 2;

            return (TopRange: obstacleTopRange, BottomRange: obstacleBottomRange);
        }

        private bool checkCollisionWithObstacles(GameObject obstacle)
        {
            var hasCollided = false;
            this.obstacles.ToList().ForEach(currentObstacle => hasCollided = currentObstacle.HasCollidedWith(obstacle));
            return hasCollided;
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

        //TODO move this to obstacle class
        private void adjustXSpacingForObstacle(GameObject obstacle, double amountOfSpace)
        {
            switch (this.direction)
            {
                case Direction.Left:
                    obstacle.X += amountOfSpace;
                    break;

                case Direction.Right:
                    obstacle.X -= amountOfSpace;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static double getCenteredYLocationOfLane(GameObject obstacle, double yLocation, double heightOfLane)
        {
            return ((heightOfLane - obstacle.Height) / 2) + yLocation;
        }

        private double getObstacleInvertedLaneLocation(GameObject obstacle)
        {
            switch (this.direction)
            {
                case Direction.Left:

                    return obstacle.X + obstacle.Width >= this.horizontalWidth
                        ? obstacle.X + obstacle.Width
                        : obstacle.X + obstacle.Width + this.horizontalWidth;

                case Direction.Right:

                    return obstacle.X <= 0
                        ? obstacle.X + obstacle.Width
                        : obstacle.X - this.horizontalWidth;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void add(Obstacle obstacle)
        {
            obstacle.SpeedX = this.defaultSpeed;

            switch (this.direction)
            {
                case Direction.Left:
                    obstacle.X = this.horizontalWidth;
                    break;

                case Direction.Right:
                    obstacle.X = -obstacle.Width;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            this.obstacles.Add(obstacle);
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
            return this.horizontalWidth / this.obstacles.Count;
        }

        #endregion
    }
}
