using System;
using FroggerStarter.Enums;
using FroggerStarter.Model.Game_Objects.Moving_Object;
using FroggerStarter.Model.Game_Objects.Moving_Object.RoadObstacle;
using FroggerStarter.Model.Game_Objects.Moving_Object.WaterObstacle;
using FroggerStarter.Model.Game_Objects.Moving_Object.WaterObstacle.Logs;

namespace FroggerStarter.Factory
{
    /// <summary>
    ///     Factory class the creates a obstacle based on the type (Enum: ObstacleType)
    /// </summary>
    internal class ObstacleFactory
    {
        #region Methods

        /// <summary>
        ///     Creates the correlating type of Obstacle [obstacleType] and returns that Obstacle.
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        /// <param name="obstacleType">Type of the Obstacle.</param>
        /// <returns>
        ///     Returns the Obstacle of the correlating [obstacleType]
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">obstacleType - null</exception>
        public static Obstacle CreateObstacle(ObstacleType obstacleType)
        {
            switch (obstacleType)
            {
                case ObstacleType.Car:
                    return new Car();

                case ObstacleType.SemiTruck:
                    return new SemiTruck();

                case ObstacleType.ToadTruck:
                    return new ToadTruck();

                case ObstacleType.SpeedBoat:
                    return new SpeedBoat();

                case ObstacleType.SmallLog:
                    return new SmallLog();

                case ObstacleType.MediumLog:
                    return new MediumLog();

                case ObstacleType.LargeLog:
                    return new LargeLog();

                default:
                    throw new ArgumentOutOfRangeException(nameof(obstacleType), obstacleType, null);
            }
        }

        #endregion
    }
}