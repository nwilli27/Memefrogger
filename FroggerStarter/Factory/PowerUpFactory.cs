using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FroggerStarter.Enums;
using FroggerStarter.Model;
using FroggerStarter.Model.Game_Objects.Power_Ups;

namespace FroggerStarter.Factory
{
    /// <summary>
    ///     Handles the creation of a power up sprite
    /// </summary>
    internal class PowerUpFactory
    {

        /// <summary>
        ///     Creates the power up based on the passed in type
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        /// <param name="powerUpType">Type of the power up.</param>
        /// <returns>
        ///     The correlating power up object based on the type
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">powerUpType - null</exception>
        public static PowerUp CreatePowerUp(PowerUpType powerUpType)
        {
            switch (powerUpType)
            {
                case PowerUpType.TimeIncrease:
                    return new TimePowerUp();

                default:
                    throw new ArgumentOutOfRangeException(nameof(powerUpType), powerUpType, null);
            }
        }
    }
}
