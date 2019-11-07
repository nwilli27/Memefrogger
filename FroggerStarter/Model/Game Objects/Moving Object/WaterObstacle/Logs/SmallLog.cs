using FroggerStarter.Enums;
using FroggerStarter.Factory;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model.Game_Objects.Moving_Object.WaterObstacle.Logs
{
    class SmallLog : Log
    {

        public SmallLog(Direction direction) : base(direction)
        {
            Sprite = new SmallLogSprite();
        }
    }
}
