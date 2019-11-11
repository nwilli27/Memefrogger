using System.Collections.Generic;
using FroggerStarter.Enums;

namespace FroggerStarter.Model.Levels.Levels
{
    /// <summary>
    ///     Class responsible for the settings for level 1.
    /// </summary>
    internal class LevelOne : Level
    {

        public override IList<object> RoadLane1 =>
            new List<object>() {
                Direction.Left,
                1.0,
                ObstacleType.ToadTruck,
                3
            };

        public override IList<object> RoadLane2 =>
            new List<object>() {
                Direction.Right,
                1.25,
                ObstacleType.SemiTruck,
                2
            };

        public override IList<object> RoadLane3 =>
            new List<object>() {
                Direction.Left,
                1.5,
                ObstacleType.Car,
                3
            };

        public override IList<object> RoadLane4 =>
            new List<object>() {
                Direction.Left,
                1.75,
                ObstacleType.SemiTruck,
                2
            };

        public override IList<object> RoadLane5 =>
            new List<object>() {
                Direction.Right,
                2.0,
                ObstacleType.Car,
                4
            };

        public override IList<object> WaterLane1 =>
            new List<object>() {
                Direction.Left,
                1.0,
                ObstacleType.LargeLog,
                2
            };

        public override IList<object> WaterLane2 =>
            new List<object>() {
                Direction.Left,
                2.0,
                ObstacleType.SpeedBoat,
                3
            };

        public override IList<object> WaterLane3 =>
            new List<object>() {
                Direction.Left,
                1.5,
                ObstacleType.MediumLog,
                3
            };

        public override IList<object> WaterLane4 =>
            new List<object>() {
                Direction.Right,
                3.0,
                ObstacleType.SpeedBoat,
                3
            };

        public override IList<object> WaterLane5 =>
            new List<object>() {
                Direction.Left,
                2.25,
                ObstacleType.SmallLog,
                4
            };
    }
}
