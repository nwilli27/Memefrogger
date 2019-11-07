using FroggerStarter.Enums;

namespace FroggerStarter.Model.Game_Objects.Moving_Object.WaterObstacle
{
    abstract class Log : WaterObstacle
    {
        protected Log(Direction direction) : base(direction)
        {
        }
    }
}
