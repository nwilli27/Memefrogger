using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    internal class Vehicle : GameObject
    {

        public Vehicle(VehicleType vehicleType, int speed)
        {
            this.createVehicleFromType(vehicleType);
            SetSpeed(speed, 0);
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
