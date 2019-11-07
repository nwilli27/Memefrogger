using FroggerStarter.Constants;
using FroggerStarter.Enums;
using System;
using System.Collections.Generic;

namespace FroggerStarter.Model.Area_Managers.Water
{
    class WaterManager : LaneManager
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="WaterManager"/> class.
        /// </summary>
        /// <param name="startingYLocation">The starting y location.</param>
        /// <param name="endingYLocation">The ending y location.</param>
        public WaterManager(double startingYLocation, double endingYLocation) : base(startingYLocation, endingYLocation) {}

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
        public override void AddLaneOfObstacles(IList<object> laneSettings)
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

            var lane = new WaterLane(defaultSpeed, direction);
            lane.AddObstacles(obstacleType, maxNumberObstacles);
            Lanes.Add(lane);
            this.UpdateYLocationOfLanes();
        }

        #endregion

        #region Private Helpers

        

        #endregion
    }
}
