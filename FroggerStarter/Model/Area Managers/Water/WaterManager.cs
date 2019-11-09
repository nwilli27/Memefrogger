
using FroggerStarter.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using FroggerStarter.Model.Animation;
using FroggerStarter.Model.Game_Objects.Moving_Object.WaterObstacle;

namespace FroggerStarter.Model.Area_Managers.Water
{
    internal class WaterManager : LaneManager
    {

        #region Properties

        /// <summary>
        ///     Gets the speed boat animation frames.
        /// </summary>
        /// <value>
        ///     The speed boat animation frames.
        /// </value>
        public IEnumerable<Frame> SpeedBoatAnimationFrames => this.Where(obstacle => obstacle is SpeedBoat).SelectMany(obstacle => (obstacle as SpeedBoat)?.SplashAnimation);

        #endregion

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

        /// <summary>
        ///     Starts all speed boat water animations
        ///     Precondition: none
        ///     Post-condition: @each speedBoat SplashAnimation.Start()
        /// </summary>
        public void StartAllSpeedBoatWaterAnimations()
        {
            this.ToList().ForEach(speedboat => (speedboat as SpeedBoat)?.StartSpeedBoatWaterAnimation());
        }

        #endregion
    }
}
