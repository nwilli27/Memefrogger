using System;
using System.Collections.Generic;
using System.Linq;
using FroggerStarter.Enums;
using Windows.UI.Xaml;
using FroggerStarter.Model.Levels;

namespace FroggerStarter.Model.Area_Managers.Road
{
    /// <summary>
    ///     Defines the object for a RoadManager based on the LaneManager.
    /// </summary>
    /// <seealso cref="LaneManager" />
    public class RoadManager : LaneManager
    {

        #region Data Members

        private DispatcherTimer obstacleSpawnTimer;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="RoadManager" /> class.
        /// </summary>
        /// <param name="startingYLocation">The starting y location.</param>
        /// <param name="endingYLocation">The ending y location.</param>
        public RoadManager(double startingYLocation, double endingYLocation) : base(startingYLocation, endingYLocation)
        {
            this.setupObstacleSpawnTimer();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Adds a lane with a specified number of [obstacleType] obstacles to it and
        ///     then readjusts the Y spacing of each lane.
        ///     Precondition: defaultSpeed &gt; 0
        ///                   numberOfObstacles &gt; 0
        ///     Post-condition: this.lanes.Count += 1
        ///                     lane.obstacles.Count += numberOfObstacles
        ///                     @each lane yLocation readjusts accordingly
        /// </summary>
        /// <param name="laneSettings">The lane settings.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public void AddLaneOfObstacles(IList<object> laneSettings)
        {
            var direction = (Direction)laneSettings[0];
            var defaultSpeed = (double)laneSettings[1];
            var obstacleType = (ObstacleType)laneSettings[2];
            var maxNumberObstacles = (int)laneSettings[3];

            if (maxNumberObstacles <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (defaultSpeed <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            var lane = new RoadLane(defaultSpeed, direction);
            lane.AddObstacles(obstacleType, maxNumberObstacles);
            Lanes.Add(lane);
            this.UpdateYLocationOfLanes();
        }

        /// <summary>
        ///     Resets the lanes to one obstacle each.
        ///     Precondition: none
        ///     Post-condition: @each obstacle.isActive = false
        ///                     @first obstacle.isActive = true
        /// </summary>
        public void ResetLanesToOneObstacle()
        {
            this.Lanes.ToList().ForEach(lane => (lane as RoadLane)?.ResetLaneToOneObstacle());
            this.resetSpawnTimer();
        }

        #endregion

        #region Private Helpers

        private void setupObstacleSpawnTimer()
        {
            this.obstacleSpawnTimer = new DispatcherTimer();
            this.obstacleSpawnTimer.Tick += this.obstacleSpawnTimerOnTick;
            this.obstacleSpawnTimer.Interval = new TimeSpan(0, 0, 0, LevelManager.GetCurrentLevel().RoadObstacleSpawnTime, 0);
            this.obstacleSpawnTimer.Start();
        }

        private void obstacleSpawnTimerOnTick(object sender, object e)
        {
            this.Lanes.ToList().ForEach(lane => (lane as RoadLane)?.MakeObstacleActive());
        }

        private void resetSpawnTimer()
        {
            this.obstacleSpawnTimer.Stop();
            this.obstacleSpawnTimer.Start();
        }

        #endregion
    }
}