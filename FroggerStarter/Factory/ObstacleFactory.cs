using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FroggerStarter.Model;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Factory
{
    /// <summary>
    ///     Factory class the creates a obstacle based on the type (Enum: ObstacleType)
    /// </summary>
    internal class ObstacleFactory
    {

        /// <summary>
        ///     Creates the correlating sprite of type [obstacleType] and returns that sprite.
        /// </summary>
        /// <param name="obstacleType">Type of the obstacle sprite.</param>
        /// <returns>
        ///     Returns the sprite of the correlating [obstacleType]
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">obstacleType - null</exception>
        public static BaseSprite CreateObstacleSprite(ObstacleType obstacleType)
        {
            switch (obstacleType)
            {
                case ObstacleType.Car:
                    return new CarSprite();

                case ObstacleType.SemiTruck:
                    return new SemiTruckSprite();

                case ObstacleType.ToadTruck:
                    return new ToadTruckSprite();

                default:
                    throw new ArgumentOutOfRangeException(nameof(obstacleType), obstacleType, null);
            }
        }
    }
}
