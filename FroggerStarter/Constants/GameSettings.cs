

using System.Collections.Generic;
using FroggerStarter.Model;

namespace FroggerStarter.Controller
{
    /// <summary>
    ///     Class holds unique data needed for game operation and setup.
    /// </summary>
    internal static class GameSettings
    {
        public const int TotalNumberOfLives = 4;
        public const int MaxScore = 3;
        public const double ScoreTime = 20.0;

        public static List<object> Lane5 = new List<object> {
            Direction.Right,
            2.0,
            ObstacleType.Car,
            5
        };

        public static List<object> Lane4 = new List<object> {
            Direction.Left,
            1.75,
            ObstacleType.SemiTruck,
            3
        };

        public static List<object> Lane3 = new List<object> {
            Direction.Left,
            1.5,
            ObstacleType.Car,
            4
        };

        public static List<object> Lane2 = new List<object> {
            Direction.Right,
            1.25,
            ObstacleType.SemiTruck,
            2
        };

        public static List<object> Lane1 = new List<object> {
            Direction.Left,
            1.0,
            ObstacleType.ToadTruck,
            3
        };
    }
}
