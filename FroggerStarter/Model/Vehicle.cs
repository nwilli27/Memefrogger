using System;
using Windows.Foundation;
using FroggerStarter.View.Sprites;
using Windows.UI.Xaml.Media;

namespace FroggerStarter.Model
{
    internal class Vehicle : GameObject
    {

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

        //TODO make comment
        public void FlipSpriteHorizontally()
        {
            this.Sprite.RenderTransformOrigin = new Point(0.5, 0.5);
            this.Sprite.RenderTransform = new ScaleTransform() { ScaleX = -1 };
        }
    }
}
