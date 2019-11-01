using System.Collections.Generic;
using FroggerStarter.Enums;

namespace FroggerStarter.Constants
{
    /// <summary>
    ///     Class holds unique data needed for game operation and setup.
    /// </summary>
    internal static class GameSettings
    {
        public const int TotalNumberOfLives = 4;
        public const double ScoreTime = 20.0;

        /// <summary>
        ///     The 5th Lane settings
        /// </summary>
        public static IList<object> Lane5 = new List<object> {
            Direction.Right,
            2.0,
            ObstacleType.Car,
            5
        };

        /// <summary>
        ///     The 4th Lane settings
        /// </summary>
        public static IList<object> Lane4 = new List<object> {
            Direction.Left,
            1.75,
            ObstacleType.SemiTruck,
            3
        };

        /// <summary>
        ///     The 3rd Lane settings
        /// </summary>
        public static IList<object> Lane3 = new List<object> {
            Direction.Left,
            1.5,
            ObstacleType.Car,
            4
        };

        /// <summary>
        ///     The 2nd Lane settings
        /// </summary>
        public static IList<object> Lane2 = new List<object> {
            Direction.Right,
            1.25,
            ObstacleType.SemiTruck,
            2
        };

        /// <summary>
        ///     The 1st Lane settings
        /// </summary>
        public static IList<object> Lane1 = new List<object> {
            Direction.Left,
            1.0,
            ObstacleType.ToadTruck,
            3
        };
    }
}
