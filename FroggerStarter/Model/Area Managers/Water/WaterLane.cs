using FroggerStarter.Enums;

namespace FroggerStarter.Model.Area_Managers.Water
{
    class WaterLane : Lane
    {
        public WaterLane(double defaultSpeed, Direction direction) : base(defaultSpeed, direction)
        {
        }

        //TODO: There will likely be methods needed to become abstract in the Lane class when functionality
        //TODO: between the Road lane and Water lane becomes very different, just a heads up.
    }
}
