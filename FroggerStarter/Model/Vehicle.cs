using System;
using Windows.Foundation;
using FroggerStarter.View.Sprites;
using Windows.UI.Xaml.Media;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     A Vehicle sprite object of type GameObject.
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.GameObject" />
    internal class Vehicle : GameObject
    {

        /// <summary>
        ///     Initializes a new instance of the <see cref="Vehicle"/> class.
        ///     Creates the vehicle sprite according to the type of vehicle passed in.
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        /// <param name="vehicleType">Type of the vehicle to create.</param>
        public Vehicle(VehicleType vehicleType)
        {
            this.createVehicleFromType(vehicleType);
        }

        private void createVehicleFromType(VehicleType vehicleType)
        {
            switch (vehicleType)
            {
                case VehicleType.Car:
                    Sprite = new CarSprite();
                    break;

                case VehicleType.SemiTruck:
                    Sprite = new SemiTruckSprite();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(vehicleType), vehicleType, null);
            }
        }
    }
}
