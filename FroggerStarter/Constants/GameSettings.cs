using System.Collections.Generic;
using FroggerStarter.Enums;

namespace FroggerStarter.Constants
{
    /// <summary>
    ///     Class holds unique data needed for game operation and setup.
    /// </summary>
    public class GameSettings
    {
        /// <summary>
        ///     The total number of lives
        /// </summary>
        public const int TotalNumberOfLives = 4;

        /// <summary>
        ///     The total score time
        /// </summary>
        public const double ScoreTime = 20.0;

        /// <summary>
        ///     The 5th Lane settings
        /// </summary>
        public static IList<object> RoadLane5 = new List<object> {
            Direction.Right,
            2.0,
            ObstacleType.Car,
            5
        };

        /// <summary>
        ///     The 4th Lane settings
        /// </summary>
        public static IList<object> RoadLane4 = new List<object> {
            Direction.Left,
            1.75,
            ObstacleType.SemiTruck,
            3
        };

        /// <summary>
        ///     The 3rd Lane settings
        /// </summary>
        public static IList<object> RoadLane3 = new List<object> {
            Direction.Left,
            1.5,
            ObstacleType.Car,
            4
        };

        /// <summary>
        ///     The 2nd Lane settings
        /// </summary>
        public static IList<object> RoadLane2 = new List<object> {
            Direction.Right,
            1.25,
            ObstacleType.SemiTruck,
            2
        };

        /// <summary>
        ///     The 1st Lane settings
        /// </summary>
        public static IList<object> RoadLane1 = new List<object> {
            Direction.Left,
            1.0,
            ObstacleType.ToadTruck,
            3
        };

        /// <summary>
        ///     The 1st water Lane settings
        /// </summary>
        public static IList<object> WaterLane1 = new List<object> {
            Direction.Left,
            1.5,
            ObstacleType.LargeLog,
            2
        };

        /// <summary>
        ///     The 2nd water Lane settings
        /// </summary>
        public static IList<object> WaterLane2 = new List<object> {
            Direction.Right,
            2.0,
            ObstacleType.SpeedBoat,
            3
        };

        /// <summary>
        ///     The 1st water Lane settings
        /// </summary>
        public static IList<object> WaterLane3 = new List<object> {
            Direction.Left,
            1.75,
            ObstacleType.MediumLog,
            3
        };

        /// <summary>
        ///     The 1st water Lane settings
        /// </summary>
        public static IList<object> WaterLane4 = new List<object> {
            Direction.Right,
            3.5,
            ObstacleType.SpeedBoat,
            3
        };

        /// <summary>
        ///     The 1st water Lane settings
        /// </summary>
        public static IList<object> WaterLane5 = new List<object> {
            Direction.Left,
            2.0,
            ObstacleType.SmallLog,
            4
        };
    }
}
