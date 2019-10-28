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
        private readonly double startingYLocation;
        private readonly double height;

        private DispatcherTimer obstacleSpawnTimer;

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
        /// <param name="startingYLocation">The starting y location.</param>
        /// <param name="endingYLocation">The ending y location.</param>
        public LaneManager(double startingYLocation, double endingYLocation)
        {
            this.startingYLocation = startingYLocation < endingYLocation ? startingYLocation : throw new ArgumentOutOfRangeException();
            this.lanes = new List<Lane>();
            this.height = endingYLocation - startingYLocation;

            this.setupObstacleSpawnTimer();
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
        ///     Resets the lanes to one obstacle each.
        ///     Precondition: none
        ///     Post-condition: @each obstacle.isActive = false
        ///                     @first obstacle.isActive = true
        /// </summary>
        public void ResetLanesToOneObstacle()
        {
            this.lanes.ToList().ForEach(lane => lane.ResetLaneToOneObstacle());
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
        /// <param name="maxNumberObstacles">The max number of obstacles.</param>
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

            var lane = new Lane(defaultSpeed, direction);
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
        ///     Post-condition: obstacleSpawnTimer.Start()
        /// </summary>
        public void ResetObstacleSpawnTimer()
        {
            this.obstacleSpawnTimer.Stop();
            this.obstacleSpawnTimer.Start();
        }

        #endregion

        #region Private Helpers

        private void setupObstacleSpawnTimer()
        {
            this.obstacleSpawnTimer = new DispatcherTimer();
            this.obstacleSpawnTimer.Tick += this.obstacleSpawnTimerOnTick;
            this.obstacleSpawnTimer.Interval = new TimeSpan(0, 0, 0, 4, 0);
            this.obstacleSpawnTimer.Start();
        }

        private void obstacleSpawnTimerOnTick(object sender, object e)
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
