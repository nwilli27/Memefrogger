using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     Class to hold and manage a collection of Lanes.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.IEnumerable{Lane}" />
    internal class LaneManager : IEnumerable<Obstacle>
    {

        #region Data Members

        private readonly IList<Lane> lanes;
        private readonly double width;
        private readonly double startingYLocation;
        private readonly double height;

        private DispatcherTimer obstacleSpeedTimer;

        #endregion

        #region Constants

        private const double SpeedIncrease = 0.15;

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

            this.setupObstacleSpeedTimer();
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
            this.lanes.ToList().ForEach(lane => lane.MoveObstacles());
        }

        /// <summary>
        ///     Sets all obstacles to default speed of there corresponding lane.
        ///     Precondition: none
        ///     Post-condition: @each lane of obstacles : obstacle.SpeedX == lane.defaultSpeed
        /// </summary>
        public void SetAllObstaclesToDefaultSpeed()
        {
            this.lanes.ToList().ForEach(lane => lane.SetObstaclesToDefaultSpeed());
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
        /// <param name="direction">The lane direction.</param>
        /// <param name="defaultSpeed">The default speed.</param>
        /// <param name="obstacleType">Type of the obstacle.</param>
        /// <param name="numberOfObstacles">The number of obstacles.</param>
        public void AddLaneOfObstacles(Direction direction, double defaultSpeed, ObstacleType obstacleType, int maxNumberObstacles)
        {
            if (maxNumberObstacles <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (defaultSpeed <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            var lane = new Lane(this.width, defaultSpeed, direction);
            lane.AddObstacles(obstacleType, maxNumberObstacles);
            this.lanes.Add(lane);
            this.updateYLocationOfLanes();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<Obstacle> GetEnumerator()
        {
            return this.lanes.SelectMany(lane => lane).GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.lanes.SelectMany(lane => lane).GetEnumerator();
        }

        /// <summary>
        ///     Resets the obstacles speed timer.
        ///     Precondition: none
        ///     Post-condition: obstacleSpeedTimer.Start()
        /// </summary>
        public void ResetObstaclesSpeedTimer()
        {
            this.obstacleSpeedTimer.Stop();
            this.obstacleSpeedTimer.Start();
        }

        #endregion

        #region Private Helpers

        private void setupObstacleSpeedTimer()
        {
            this.obstacleSpeedTimer = new DispatcherTimer();
            this.obstacleSpeedTimer.Tick += this.obstacleSpeedTimerOnTick;
            this.obstacleSpeedTimer.Interval = new TimeSpan(0, 0, 0, 5, 0);
            this.obstacleSpeedTimer.Start();
        }

        private void obstacleSpeedTimerOnTick(object sender, object e)
        {
            this.lanes.ToList().ForEach(lane => lane.MoveNextAvailableObstacle());
        }

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
