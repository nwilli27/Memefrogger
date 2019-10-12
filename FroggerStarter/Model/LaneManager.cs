using System;
using System.Collections.Generic;
using System.Linq;


namespace FroggerStarter.Model
{
    /// <summary>
    ///     Class to hold and manage a collection of Lanes.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.IEnumerable{Lane}" />
    internal class LaneManager
    {
        #region Properties

        /// <summary>
        ///     Gets the obstacles of all lanes.
        /// </summary>
        /// <value>
        ///     The obstacles of all lanes.
        /// </value>
        public IEnumerable<Obstacle> Obstacles => this.lanes.SelectMany(lane => lane);

        #endregion

        #region Data Members

        private readonly IList<Lane> lanes;
        private readonly double width;
        private readonly double startingYLocation;
        private readonly double height;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="LaneManager"/> class
        ///     Precondition: startingYLocation lt; endingYLocation;
        /// 
        ///     Post-condition: this.lanes.Count() == 0
        ///                     this.width == width
        ///                     this.startingYLocation == startingYLocation
        ///                     this.height += endingYLocation - startingYLocation
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="startingYLocation">The starting y location.</param>
        /// <param name="endingYLocation">The ending y location.</param>
        public LaneManager(double width, double startingYLocation, double endingYLocation)
        {
            this.startingYLocation = startingYLocation < endingYLocation ? startingYLocation : throw new ArgumentOutOfRangeException();
            this.width = width;
            this.lanes = new List<Lane>();
            this.height = endingYLocation - startingYLocation;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Moves all obstacles in all lanes.
        ///     Precondition: none
        ///     Post-condition: @each obstacle in this.lanes moveLeft() || moveRight()
        /// </summary>
        public void MoveAllObstacles()
        {
            foreach (var currentLane in this.lanes)
            {
                currentLane.MoveObstacles();
            }
        }

        /// <summary>
        ///     Increases the speed of all obstacles in all lanes by the set speed.
        ///     Precondition: none
        ///     Post-condition: @each obstacle in this.lanes.SpeedX += speed
        /// </summary>
        /// <param name="speed">The speed to increase by.</param>
        public void IncreaseSpeedOfObstacles(double speed)
        {
            foreach (var currentLane in this.lanes)
            {
                currentLane.IncreaseSpeedOfObstacles(speed);
            }
        }

        /// <summary>
        ///     Sets all obstacles to default speed of there corresponding lane.
        ///     Precondition: none
        ///     Post-condition: @each lane of obstacles : obstacle.SpeedX == lane.defaultSpeed
        /// </summary>
        public void SetAllObstaclesToDefaultSpeed()
        {
            foreach (var currentLane in this.lanes)
            {
                currentLane.SetObstaclesToDefaultSpeed();
            }
        }

        /// <summary>
        ///     Adds a lane with a specified number of [obstacleType] obstacles to it and
        ///     then readjusts the Y spacing of each lane.
        ///     Precondition: defaultSpeed > 0
        ///                   numberOfObstacles > 0
        ///                   
        ///     Post-condition: this.lanes.Count += 1
        ///                     lane.obstacles.Count += numberOfObstacles
        ///                     @each lane yLocation readjusts accordingly
        /// </summary>
        /// <param name="laneDirection">The lane direction.</param>
        /// <param name="defaultSpeed">The default speed.</param>
        /// <param name="obstacleType">Type of the obstacle.</param>
        /// <param name="numberOfObstacles">The number of obstacles.</param>
        public void AddLaneOfObstacles(LaneDirection laneDirection, double defaultSpeed, ObstacleType obstacleType, int numberOfObstacles)
        {
            if (numberOfObstacles <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (defaultSpeed <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            var lane = new Lane(this.width, defaultSpeed, laneDirection);
            lane.AddObstacles(obstacleType, numberOfObstacles);
            this.lanes.Add(lane);
            this.updateYLocationOfLanes();
        }

        #endregion

        #region Private Helpers

        private void updateYLocationOfLanes()
        {
            var heightOfEachLane = this.height / this.lanes.Count;
            for (var i = 0; i < this.lanes.Count; i++)
            {
                var yLocation = this.startingYLocation + (heightOfEachLane * i);
                this.lanes[i].SetObstaclesToLaneYLocation(yLocation, heightOfEachLane);
            }
        }

        #endregion
    }
}
