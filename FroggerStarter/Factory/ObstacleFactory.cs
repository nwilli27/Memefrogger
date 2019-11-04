﻿using System;
using FroggerStarter.Enums;
using FroggerStarter.Model.Game_Objects.Moving_Object;

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
        /// <param name="direction">The direction of the Obstacle.</param>
        /// <returns>
        ///     Returns the Obstacle of the correlating [obstacleType]
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">obstacleType - null</exception>
        public static Obstacle CreateObstacle(ObstacleType obstacleType, Direction direction)
        {
            switch (obstacleType)
            {
                case ObstacleType.Car:
                    return new Car(direction);

                case ObstacleType.SemiTruck:
                    return new SemiTruck(direction);

                case ObstacleType.ToadTruck:
                    return new ToadTruck(direction);

                default:
                    throw new ArgumentOutOfRangeException(nameof(obstacleType), obstacleType, null);
            }
        }

        #endregion
    }
}