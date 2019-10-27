

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

        public static List<object> Lane5 = new List<object> {
            Direction.Right,
            2.0,
            ObstacleType.Car,
            5
        };
    }
}
