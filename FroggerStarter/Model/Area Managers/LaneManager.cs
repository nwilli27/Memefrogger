using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using FroggerStarter.Model.Game_Objects.Moving_Object;

namespace FroggerStarter.Model.Area_Managers
{
    /// <summary>
    ///     Class to hold and manage a collection of Lanes.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.IEnumerable{Lane}" />
    public abstract class LaneManager : IEnumerable<Obstacle>
    {

        #region Data Members

        /// <summary>
        ///     The lanes collection
        /// </summary>
        protected readonly IList<Lane> Lanes;

        /// <summary>
        ///     The starting y location
        /// </summary>
        protected readonly double StartingYLocation;

        /// <summary>
        ///     The height
        /// </summary>
        protected readonly double Height;

        /// <summary>
        ///     The obstacle spawn timer
        /// </summary>
        protected DispatcherTimer ObstacleSpawnTimer;

        #endregion

        #region Constants

        /// <summary>
        ///     The spawn time interval
        /// </summary>
        protected const int SpawnTimeInterval = 4;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="LaneManager"/> class
        ///     Precondition: startingYLocation lt; endingYLocation;
        /// 
        ///     Post-condition: this.lanes.Count() == 0
        ///                     this.startingYLocation == startingYLocation
        ///                     this.height += endingYLocation - startingYLocation
        /// </summary>
        /// <param name="startingYLocation">The starting y location.</param>
        /// <param name="endingYLocation">The ending y location.</param>
        protected LaneManager(double startingYLocation, double endingYLocation)
        {
            this.StartingYLocation = startingYLocation < endingYLocation ? startingYLocation : throw new ArgumentOutOfRangeException();
            this.Lanes = new List<Lane>();
            this.Height = endingYLocation - startingYLocation;

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
            this.Lanes.ToList().ForEach(lane => lane.MoveObstacles());
        }

        /// <summary>
        ///     Resets the lanes to one obstacle each.
        ///     Precondition: none
        ///     Post-condition: @each obstacle.isActive = false
        ///                     @first obstacle.isActive = true
        /// </summary>
        public void ResetLanesToOneObstacle()
        {
            this.Lanes.ToList().ForEach(lane => lane.ResetLaneToOneObstacle());
            this.resetSpawnTimer();
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
        /// <param name="laneSettings">The lane settings.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public abstract void AddLaneOfObstacles(IList<object> laneSettings);
        //TODO: Determine if this needs to be abstract?
        //TODO: Implementation between Road/Water might only be the var lane = new Lane(etc) line...

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<Obstacle> GetEnumerator()
        {
            return this.Lanes.SelectMany(lane => lane).GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Lanes.SelectMany(lane => lane).GetEnumerator();
        }

        #endregion

        #region Private Helpers

        private void setupObstacleSpawnTimer()
        {
            this.ObstacleSpawnTimer = new DispatcherTimer();
            this.ObstacleSpawnTimer.Tick += this.obstacleSpawnTimerOnTick;
            this.ObstacleSpawnTimer.Interval = new TimeSpan(0, 0, 0, SpawnTimeInterval, 0);
            this.ObstacleSpawnTimer.Start();
        }

        private void obstacleSpawnTimerOnTick(object sender, object e)
        {
            this.Lanes.ToList().ForEach(lane => lane.MakeObstacleActive());
        }

        private void resetSpawnTimer()
        {
            this.ObstacleSpawnTimer.Stop();
            this.ObstacleSpawnTimer.Start();
        }

        /// <summary>
        ///     Updates the y location of lanes.
        ///     Precondition: none
        ///     Post-condition: Determines the height of the lanes based on the number of lanes 
        ///                     and the height of the lanes.
        /// </summary>
        protected void UpdateYLocationOfLanes()
        {
            var heightOfEachLane = this.Height / this.Lanes.Count;
            for (var i = 0; i < this.Lanes.Count; i++)
            {
                var yLocation = this.StartingYLocation + (heightOfEachLane * i);
                this.Lanes[i].SetObstaclesToLaneYLocation(yLocation, heightOfEachLane);
            }
        }

        #endregion
    }
}
